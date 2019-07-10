using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo9;
using Microsoft.Extensions.Logging;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo9
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class BaggageController : ControllerBase
    {


        private readonly ILogger<ClaimController> _logger;


        public BaggageController(ILogger<ClaimController> logger)
        {
            _logger = logger;
        }


        /// <summary>
        ///     GET api/baggage/serial/{id}
        ///     Metodo para obtener el equipaje segun su serial
        /// </summary>
        /// <returns>Objeto tipo json con el equipaje deseado</returns>
        [HttpGet("serial/{id}")]
        public ActionResult<BaggageDTO> Get(int id)
        {
            var getByIdCommand = CommandFactory.CreateGetBaggageByIdCommand(id);
            try
            {
                getByIdCommand.Execute();
                _logger?.LogInformation($"Obtenido Equipaje por numero serial {id} exitosamente");
                return getByIdCommand.GetResult();
            }
            catch (BaggageNotFoundException)
            {
                _logger.LogWarning($"Equipaje con serial {id} no encontrado");
                return new NotFoundResult();
            }
            catch (DatabaseException ex)
            {
                _logger?.LogError(ex,"Error en la base de datos al tratar de obtener un equipaje por serial");
                return StatusCode(500, ex.Message);
            }
        }



        /// <summary>
        ///     GET api/baggage/documentPasaport/{id}
        ///     Metodo para obtener todos los equipajes de un usuario en particular por medio de su pasaporte
        /// </summary>
        /// <returns>Lista de Objetos de tipo json de cada equipaje</returns>
        [HttpGet("documentPasaport/{id}")]
        public ActionResult<IEnumerable<BaggageDTO>> GetDocument(string id)
        {
            var getByPassportCommand = CommandFactory.CreateGetBaggageByPassportCommand(id);
            try
            {
                getByPassportCommand.Execute();
                _logger?.LogInformation($"Obtenido Equipaje por numero de pasaporte {id} exitosamente");
                return getByPassportCommand.GetResult();
            }
            catch (BaggageNotFoundException)
            {
                _logger.LogWarning($"Equipajes no encontrados pertenecientes al usuario de pasaporte {id}");
                return new NotFoundResult();
            }
            catch (DatabaseException ex)
            {
                _logger?.LogError(ex, "Error en la base de datos al obtener un equipaje por numero de pasaporte");
                return StatusCode(500, ex.Message);
            }
        }



        /// <summary>
        ///     GET api/baggage/admin/getStatus/{id}
        ///     Metodo para obtener todos los equipajes segun un estatus determinado
        ///     Estatus posibles: EXTRAVIADO, RECLAMADO, ENTREGADO
        /// </summary>
        /// <returns>Lista de Objetos de tipo json de cada equipaje</returns>
        [HttpGet("admin/getStatus/{status}")]
        public ActionResult<IEnumerable<BaggageDTO>> GetStatus(string status)
        {
            var getByStatusCommand = CommandFactory.CreateGetBaggageByStatusCommand(status);
            try
            {
                getByStatusCommand.Execute();
                _logger?.LogInformation($"Obtenidos todos los equipajes de {status} exitosamente");
                return getByStatusCommand.GetResult();
            }
            catch (BaggageNotFoundException)
            {
                _logger.LogWarning($"Equipajes no encontrados pertenecientes al estatus {status}");
                return new NotFoundResult();
            }
            catch (DatabaseException ex)
            {
                _logger?.LogError(ex, "Error en la base de datos al obtener el listado de equipajes por estatus");
                return StatusCode(500, ex.Message);
            }
        }



        /// <summary>
        ///     PUT api/baggage/{id}
        ///     Metodo para modificar el equipaje
        /// </summary>
        /// <returns>id del equipaje modificado</returns>
        [HttpPut("{id}")]
        public ActionResult<BaggageDTO> Put(int id, [FromBody] BaggageDTO Baggage)
        {
            var updateBaggageCommand = CommandFactory.CreateUpdateBaggageCommand(id, Baggage);
            try
            {
                updateBaggageCommand.Execute();
                _logger?.LogInformation($"Modificado el equipaje de id {id} exitosamente");
                return updateBaggageCommand.GetResult();
            }
            catch (DatabaseException ex)
            {
                _logger?.LogError(ex, $"Error en la base de datos al tratar de modificar el equipaje de id {id}");
                return StatusCode(500, ex.Message);
            }
            catch (BaggageNotFoundException ex)
            {
                _logger?.LogError(ex, "Equipaje que se deseaba modificar no fue encontrado");
                return StatusCode(500, ex.Message);
            }
            catch (AttributeValueException ex)
            {
                _logger?.LogError(ex, "Al modificar un baggage, ocurrio un error en los valores de los atributos");
                return new BadRequestObjectResult(new ErrorMessage(ex.Message));
            }
        }
    }
}