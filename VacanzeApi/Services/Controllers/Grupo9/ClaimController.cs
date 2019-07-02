using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo9;
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
        public ActionResult<Claim> GetById(int id)
        {
            var getByIdCommand = CommandFactory.CreateGetClaimByIdCommand(id);

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
            var getByDocumentCommand = CommandFactory.CreateGetClaimsByDocumentCommand(document);
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
        ///     Status posibles: ABIERTO, CERRADO [Existe estado Extraviado pero no se puede postear]
        /// </summary>
        [HttpGet("admin/{status}")]
        public ActionResult<IEnumerable<Claim>> GetByStatus(string status)
        {
            var getByStatusCommand = CommandFactory.CreateGetClaimsByStatusCommand(status);

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
        ///     Post api/claim/{baggage_id}
        ///     utilizado para crear un reclamo con una id del equipaje
        /// </summary>
        [HttpPost]
        public ActionResult<Claim> Post([FromBody] Claim claim)
        {
            try
            {
                // TODO: Esta validacion no esta funcionando, le puedes pasar un id que no exista
                // TODO: Refactorizar esto relacionado a baggage
                // TODO: Meter esta validacion dentro del ClaimValidator
                // var bag = new BaggageRepository();
                // var a = bag.GetBaggage(claim.BaggageId);
                // if (a.Count == 0) throw new NullBaggageException("No existe el Equipaje");

                var addClaimCommand = CommandFactory.CreateAddClaimCommand(claim);
                addClaimCommand.Execute();
                claim.Id = addClaimCommand.GetResult();
                return CreatedAtAction("Get", "claim", claim);
            }
            catch (RequiredAttributeException ex)
            {
                return new BadRequestObjectResult(new ErrorMessage(ex.Message));
            }
            catch (AttributeSizeException ex)
            {
                return new BadRequestObjectResult(new ErrorMessage(ex.Message));
            }
            catch (AttributeValueException ex)
            {
                return new BadRequestObjectResult(new ErrorMessage(ex.Message));
            }
            catch (BaggageNotFoundException ex)
            {
                return new BadRequestObjectResult(new ErrorMessage(ex.Message));
            }
            catch (DatabaseException ex)
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
            // TODO: Refacotrizar con comandos/dao y quitar ese ClaimSecundary
            try
            {
                var conec = new ClaimRepository();
                var claim = ClaimBuilder.Create()
                    .WithTitle(ClaimAux.title)
                    .WithDescription(ClaimAux.description)
                    .WithStatus(ClaimAux.status)
                    .Build();

                ClaimValidator.Validate(claim, HttpMethod.Put);
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