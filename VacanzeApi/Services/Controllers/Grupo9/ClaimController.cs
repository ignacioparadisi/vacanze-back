using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo9;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo9
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        /// <summary>
        ///     GET api/Claim
        ///     se usara para consultar la cantidad de reclamos en la base de datos
        /// </summary>
        [HttpGet]
        public int Get()
        {
            try
            {
                var conec = new ClaimRepository();
                var rows = conec.GetClaim();
                return rows;
            }
            catch (DatabaseException)
            {
                return -1;
            }
            catch (InvalidStoredProcedureSignatureException)
            {
                return -1;
            }
        }

        /// <summary>
        ///     GET api/claim/{id}
        ///     Obtener Claim segun su Id
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<Claim> Get(int id)
        {
            var commandFactory = new CommandFactory();
            var getByIdCommand = commandFactory.CreateGetClaimByIdCommand(id);

            try
            {
                getByIdCommand.Execute();
                return getByIdCommand.GetResult();
            }
            catch (ClaimNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (DatabaseException ex)
            {
                // TODO: Log
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        ///     Get para la tabla equipaje segun su documento de identidad
        /// </summary>
        [HttpGet("document/{document}")]
        public ActionResult<IEnumerable<Claim>> GetByDocument(string document)
        {
            var commandFactory = new CommandFactory();
            var getByDocumentCommand = commandFactory.CreateGetClaimsByDocumentCommand(document);
            try
            {
                getByDocumentCommand.Execute();
                return getByDocumentCommand.GetResult();
            }
            catch (DatabaseException ex)
            {
                // TODO: Log
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        ///     GET api/claim/admin/status
        ///     usado para que el administrador consulte los reclamos por estatus.
        ///     Status posibles: ABIERTO, CERRADO, EXTRAVIADO ... [TODO: Conseguir lista completa]
        /// </summary>
        [HttpGet("admin/{status}")]
        public ActionResult<IEnumerable<Claim>> GetStatus(string status)
        {
            var commandFactory = new CommandFactory();
            var getByStatusCommand = commandFactory.CreateGetClaimsByStatusCommand(status);

            try
            {
                getByStatusCommand.Execute();
                return getByStatusCommand.GetResult();
            }
            catch (DatabaseException ex)
            {
                // TODO: Log
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        ///     Post api/Claim/id
        ///     utilizado para crear un reclamo con una id del equipaje
        /// </summary>
        [HttpPost("{id}")]
        public ActionResult<string> Post(int id, [FromBody] ClaimSecundary ClaimAux)
        {
            try
            {
                var bag = new BaggageRepository();
                var a = bag.GetBaggage(id);
                if (a.Count == 0) throw new NullBaggageException("No existe el Equipaje");

                var conec = new ClaimRepository();
                var claim = new Claim(ClaimAux.title, ClaimAux.description);
                claim.Validate();
                claim.ValidatePost();
                conec.AddClaim(claim, id);
                return Ok("Agregado correctamente");
            }
            catch (DatabaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (InvalidStoredProcedureSignatureException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (AttributeSizeException exc)
            {
                return StatusCode(500, exc.Message);
            }
            catch (AttributeValueException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (NullBaggageException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/Claim/5
        /// <summary>
        ///     DELETE api/Claim/id
        ///     eliminar un reclamo
        /// </summary>
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                var conec = new ClaimRepository();
                var rows = conec.DeleteClaim(id);
                return Ok("eliminado exitosamente");
            }
            catch (DatabaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (InvalidStoredProcedureSignatureException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (ClaimNotFoundException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        ///     api/Clain/status/id
        ///     modificar un reclamo , tanto por status o por titulo y descripcion
        /// </summary>
        [HttpPut("{id}")]
        public ActionResult<string> Put(int id, [FromBody] ClaimSecundary ClaimAux)
        {
            try
            {
                var conec = new ClaimRepository();
                var claim = new Claim(ClaimAux.title, ClaimAux.description, ClaimAux.status);
                claim.Validate();
                claim.ValidatePut();
                var rows = 0;
                if (ClaimAux.status != null)
                    rows = conec.ModifyClaimStatus(id, claim);
                else if (ClaimAux.title != null && ClaimAux.description != null)
                    rows = conec.ModifyClaimTitle(id, claim);
                else throw new ClaimNotFoundException("claim vacio");


                return Ok("Modificado exitosamente");
            }
            catch (DatabaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (InvalidStoredProcedureSignatureException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (ClaimNotFoundException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (AttributeSizeException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (AttributeValueException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }


    public class ClaimSecundary
    {
        public string title { get; set; }
        public string description { get; set; }
        public string status { get; set; }

        public string getStatus()
        {
            return status;
        }
    }
}