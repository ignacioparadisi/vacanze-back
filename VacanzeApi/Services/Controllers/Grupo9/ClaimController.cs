using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo9;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo9
{
    /// <summary>  
    ///  Clase para manejar las peticiones HTTP relacionadas a los reclamos  
    /// </summary> 
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
        ///     Metodo para obtener un reclamo segun su id
        /// </summary>
        /// <returns>Objeto tipo Json con el reclamo encontrado</returns>
        [HttpGet("{id}")]
        public ActionResult<ClaimDto> GetById(int id)
        {
            var getByIdCommand = CommandFactory.CreateGetClaimByIdCommand(id);

            try
            {
                getByIdCommand.Execute();
                var result = getByIdCommand.GetResult();
                _logger?.LogInformation($"Obtenido Claim por ID {id} exitosamente");
                return Ok(result);
            }
            catch (ClaimNotFoundException)
            {
                _logger.LogWarning($"Claim con ID {id} no conseguido");
                return new NotFoundResult();
            }
            catch (DatabaseException ex)
            {
                _logger?.LogError(ex, "Database exception when trying to get a claim by id");
                return StatusCode(500, ex.Message);
            }
        }



        /// <summary>
        ///     GET api/claim/document/{id}
        ///     Metodo para obtener todos los reclamos segun su documento de identidad
        /// </summary>
        /// <returns>Objeto tipo Json con los reclamos encontrados</returns>
        [HttpGet("document/{document}")]
        public ActionResult<IEnumerable<ClaimDto>> GetByDocument(string document)
        {
            var getByDocumentCommand = CommandFactory.CreateGetClaimsByDocumentCommand(document);
            try
            {
                getByDocumentCommand.Execute();
                var result = getByDocumentCommand.GetResult();
                _logger?.LogInformation($"Listados claims por documento {document} correctamente");
                return Ok(result);
            }
            catch (DatabaseException ex)
            {
                _logger?.LogError(ex, "Database exception when trying to get a claim by document");
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        ///     GET api/claim/admin/{status}
        ///     Metodo para obtener todos los reclamos segun un estatus determinado
        ///     Status posibles: ABIERTO, CERRADO [Existe estado Extraviado pero no se puede postear]
        /// </summary>
        /// <returns>Objeto tipo Json con los reclamos encontrados con el dicho estatus</returns>
        [HttpGet("admin/{status}")]
        public ActionResult<IEnumerable<ClaimDto>> GetByStatus(string status)
        {
            var getByStatusCommand = CommandFactory.CreateGetClaimsByStatusCommand(status);

            try
            {
                getByStatusCommand.Execute();
                var result = getByStatusCommand.GetResult();
                _logger?.LogInformation($"Listados claims por estado {status} correctamente");
                return Ok(result);
            }
            catch (DatabaseException ex)
            {
                _logger?.LogError(ex, "Database exception when trying to get a claim by status");
                return StatusCode(500, ex.Message);
            }
        }

      
        /// <summary>
        ///     POST api/claim/
        ///     Utilizado para crear un reclamo
        /// </summary>
        [HttpPost]
        public ActionResult<ClaimDto> Post([FromBody] ClaimDto claimDto)
        {
            try
            {
                var addClaimCommand = CommandFactory.CreateAddClaimCommand(claimDto);
                addClaimCommand.Execute();
                var registeredClaimId = addClaimCommand.GetResult();
                var getClaimByIdCommand = CommandFactory.CreateGetClaimByIdCommand(registeredClaimId);
                getClaimByIdCommand.Execute();
                var result = getClaimByIdCommand.GetResult();
                _logger?.LogInformation($"Claim creado correctamente con ID {result.Id}");
                return StatusCode(201, result);
            }
            catch (RequiredAttributeException ex)
            {
                _logger?.LogWarning($"Atributo requerido faltante al crear Claim: {ex.Message}");
                return new BadRequestObjectResult(new ErrorMessage(ex.Message));
            }
            catch (AttributeSizeException ex)
            {
                _logger?.LogWarning($"Tamaño invalido de atributo al crear Claim: {ex.Message}");
                return new BadRequestObjectResult(new ErrorMessage(ex.Message));
            }
            catch (AttributeValueException ex)
            {
                _logger?.LogWarning($"Valor invalido de atributo al crear Claim: {ex.Message}");
                return new BadRequestObjectResult(new ErrorMessage(ex.Message));
            }
            catch (BaggageNotFoundException ex)
            {
                _logger?.LogWarning($"Baggage invalido ({claimDto.BaggageId}) al crear Claim: {ex.Message}");
                return new BadRequestObjectResult(new ErrorMessage(ex.Message));
            }
            catch (DatabaseException ex)
            {
                _logger?.LogError(ex, "Error de base de datos al tratar de crear Claim");
                return StatusCode(500, ex.Message);
            }
        }



        /// <summary>
        ///     DELETE api/claim/{id}
        ///     Endpoint para eliminar un reclamo segun id
        /// </summary>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                CommandFactory.CreateDeleteClaimByIdCommand(id).Execute();
                _logger?.LogInformation($"Claim {id} eliminado correctamente");
                return Ok();
            }
            catch (DatabaseException ex)
            {
                _logger?.LogError(ex, "Error de base de datos al tratar de eliminar Claim");
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        ///     PUT /api/claim/{id}
        ///     Endpoint para modificar un reclamo
        /// </summary>
        [HttpPut("{id}")]
        public ActionResult<string> Put(int id, [FromBody] ClaimDto fieldsToUpdate)
        {
            try
            {
                CommandFactory.CreateUpdateClaimCommand(id, fieldsToUpdate).Execute();
                _logger?.LogInformation($"Claim {id} actualizado correctamente");
                return Ok();
            }
            catch (ClaimNotFoundException ex)
            {
                _logger?.LogWarning(ex.Message);
                return new BadRequestObjectResult(new ErrorMessage(ex.Message));
            }
            catch (AttributeSizeException ex)
            {
                _logger?.LogWarning($"Tamaño invalido de atributo al actualizar Claim {id}: {ex.Message}");
                return new BadRequestObjectResult(new ErrorMessage(ex.Message));
            }
            catch (AttributeValueException ex)
            {
                _logger?.LogWarning($"Valor invalido de atributo al actualizar Claim {id}: {ex.Message}");
                return new BadRequestObjectResult(new ErrorMessage(ex.Message));
            }
            catch (DatabaseException ex)
            {
                _logger?.LogError(ex, "Error de base de datos al tratar de actualizar Claim");
                return StatusCode(500, ex.Message);
            }
        }
    }
}