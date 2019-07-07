using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.LogicLayer.Command;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo9
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private readonly ILogger<ClaimController> _logger;

        public ClaimController(ILogger<ClaimController> logger)
        {
            _logger = logger;
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
                return Ok(getByIdCommand.GetResult());
            }
            catch (ClaimNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (DatabaseException ex)
            {
                _logger?.LogError(ex, "Database exception when trying to get a claim by id");
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
                return Ok(getByStatusCommand.GetResult());
            }
            catch (DatabaseException ex)
            {
                // TODO: Log
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        ///     Post api/claim/
        ///     utilizado para crear un reclamo con una id del equipaje
        /// </summary>
        [HttpPost]
        public ActionResult<Claim> Post([FromBody] Claim claim)
        {
            try
            {
                var addClaimCommand = CommandFactory.CreateAddClaimCommand(claim);
                addClaimCommand.Execute();
                var registeredClaimId = addClaimCommand.GetResult();
                var getClaimByIdCommand = CommandFactory.CreateGetClaimByIdCommand(registeredClaimId);
                getClaimByIdCommand.Execute();
                return StatusCode(201, getClaimByIdCommand.GetResult());
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

        /// <summary>
        ///     DELETE api/claim/{id}
        ///     Endpoint para eliminar reclamos por id
        /// </summary>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                CommandFactory.CreateDeleteClaimByIdCommand(id).Execute();
                return Ok();
            }
            catch (DatabaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        ///     PUT /api/claim/{id}
        ///     Endpoint para modificar un reclamo.
        /// </summary>
        [HttpPut("{id}")]
        public ActionResult<string> Put(int id, [FromBody] Claim fieldsToUpdate)
        {
            try
            {
                CommandFactory.CreateUpdateClaimCommand(id, fieldsToUpdate).Execute();
                return Ok();
            }
            catch (ClaimNotFoundException ex)
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
            catch (DatabaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}