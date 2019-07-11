
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo8;
using Microsoft.AspNetCore.Http;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo8;
using Microsoft.Extensions.Logging;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo8
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class CruiserController : Controller
    {
        private readonly ILogger<CruiserController> _logger;

        public CruiserController(ILogger<CruiserController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///     Endpoint para obtener todos los cruceros guardados.
        /// </summary>
        /// <returns>OKResult en caso de exito con un JsonObject de la lista de cruceros guardada en la base de datos</returns>

        [HttpGet]
        public ActionResult<IEnumerable<CruiserDTO>> Get()
        {
            GetCruisersCommand getCruisersCommand = CommandFactory.CreateGetCruisersCommand();
                getCruisersCommand.Execute();
                List<CruiserDTO> cruisersDtoList = getCruisersCommand.GetResult();
                _logger?.LogError("Get method for acquiring cruisers executed");
                return cruisersDtoList; 
        }

        /// <summary>
        ///     Endpoint para obtener objeto Crucero correspondiente a los datos guardados para el ID recibido.
        /// </summary>
        /// <param name="id">ID del crucero a obtener</param>
        /// <returns>OkResult en caso de exito con un JsonObject del crucero obtenido</returns>
        /// <returns>BadRequest en caso de error con un JsonObject del error arrojado por alguna de las excepciones </returns>
        /// <exception cref="CruiserNotFoundException">Lanzada si no existe un Crucero para el ID recibido</exception>
        [HttpGet("{id}")]
        public ActionResult<CruiserDTO> GetCruiser(int id)
        {
            try
            {
                GetCruiserCommand getCruiserCommand = CommandFactory.CreateGetCruiserCommand(id);
                getCruiserCommand.Execute();
                CruiserDTO cruiser = getCruiserCommand.GetResult();
                _logger?.LogError("Get method for acquiring a single cruiser using it's ID executed");
                return cruiser;
            }
            catch (CruiserNotFoundException e)
            {
                _logger?.LogError(e, "Database exception when trying to get a cruiser by ID, cruiser doesn't existe");
                return BadRequest(new ErrorMessage(e.Message));
            }
        }

        /// <summary>
        ///     Endpoint para añadir un Crucero.
        /// </summary>
        /// <param name="cruiser">Datos a ser guardados en tipo crucero</param>
        /// <returns>OkResult en caso exito con un JsonObject del crucero agregado</returns>
        ///<returns>BadRequest en caso de error con un JsonObject del error arrojado por alguna de las excepciones </returns>
        /// <exception cref="InvalidAttributeException">Algun atributo tenia un valor invalido retorna</exception>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CruiserDTO> PostCruiser([FromBody] CruiserDTO cruiser)
        {
            try
            {
                AddCruiserCommand addCruiserCommand = CommandFactory.CreateAddCruiserCommand(cruiser);
                addCruiserCommand.Execute();
                CruiserDTO savedCruiser = addCruiserCommand.GetResult();
                _logger?.LogError("Post method for cruiser executed succesfully");
                return savedCruiser;
            }
            catch (InvalidAttributeException e)
            {
                _logger?.LogError(e, "Database exception when trying to get create a new cruiser, invalid attribute");
                return BadRequest(new ErrorMessage(e.Message));
            }
        }
        // PUT/Cruiser/{id}
        /// <summary>
        ///     Endpoint para actualizar los datos de un Crucero.
        /// </summary>
        /// <param name="cruiser">Datos a ser actualizados en tipo crucero guardados</param>
        /// <returns>OkResult en caso de exito con un JsonObject con los datos actualizados del crucero</returns>
        /// <returns>BadRequest en caso de error con un JsonObject del error arrojado por alguna de las excepciones </returns>
        /// <exception cref="InvalidAttributeException">Algun atributo tenia un valor invalido</exception>
        /// <exception cref="CruiserNotFoundException">No se encontro el crucero con el Id sumunistrado en los parametros</exception>
        [HttpPut]
         public ActionResult<CruiserDTO> PutCruiser([FromBody] CruiserDTO cruiser)
         {
             try
             {
                 UpdateCruiserCommand updateCruiserCommand = CommandFactory.CreateUpdateCruiserCommand(cruiser);
                 updateCruiserCommand.Execute();
                 CruiserDTO updatedCruiser = updateCruiserCommand.GetResult();
                 _logger?.LogError("Put method for a given cruiser executed succesfully");
                 return updatedCruiser;
             }
             catch(CruiserNotFoundException e)
             {
                 _logger?.LogError(e, "Database exception when trying to update a cruiser, cruiser not found");
                 return BadRequest(new ErrorMessage(e.Message));
             }
             catch (InvalidAttributeException e)
             {
                 _logger?.LogError(e, "Database exception when trying to update a cruiser, invalid attribute");
                 return BadRequest(new ErrorMessage(e.Message));
             }
         }
        
        /// <summary>
        ///     Endpoint para eliminar un Crucero.
        /// </summary>
        /// <param name="id">Id del crucero a ser eliminado</param>
        /// <returns>OkResult en caso de exito con un JsonObject del id del crrucero eliminado</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteCruiser(int id)
        {
            DeleteCruiserCommand deleteCruiserCommand = CommandFactory.CreateDeleteCruiserCommand(id);
                deleteCruiserCommand.Execute();
                _logger?.LogError("Cruiser deletion by ID executed succesfully");
                return Ok("Eliminado satisfactoriamente");
        }
        
        /// <summary>
        ///     Endpoint para obtener todos los layovers (escalas) de un crucero.
        /// </summary>
        /// <param name="cruiserId">Id del crucero del cual se desea obtener los escalas</param>
        /// <returns>OkResult en caso de exito con un JsonObject de la lista de layovers (escalas) de un crucero</returns>
        [HttpGet("{cruiserId}/Layover")]
        public ActionResult<IEnumerable<LayoverDTO>> GetLayovers(int cruiserId)
        {
                GetLayoversCommand getLayoversCommand = CommandFactory.CreateGetLayoversCommand(cruiserId);
                getLayoversCommand.Execute();
                List<LayoverDTO> layoverDtoList = getLayoversCommand.GetResult();
                _logger?.LogError("Get method for all layovers belonging to a single cruiser returned succesfull");
                return layoverDtoList; 
        }

        /// <summary>
        ///     Endpoint para agregar un layover a un crucero.
        /// </summary>
        /// <param name="layover">Id del crucero al que se le agregara la escala</param>
        /// <returns>Okresult en caso de exito con un JsonObject de la lista de layovers (escalas) de un crucero</returns>
        /// <returns>BadRequest en caso de error con un JsonObject del error arrojado por alguna de las excepciones </returns>
        /// <exception cref="CruiserNotFoundException">Lanzada si el id del crucero en el layover no corresponde con ningun crucero guardado</exception>   
        [HttpPost("{cruiserId}/Layover")]
        public ActionResult<LayoverDTO> PostLayover([FromBody] LayoverDTO layover)
        {
            try
            {
                AddLayoverCommand addLayoverCommand = CommandFactory.CreateAddLayoverCommand(layover);
                addLayoverCommand.Execute();
                LayoverDTO savedLayover = addLayoverCommand.GetResult();
                _logger?.LogError("Post method for layover executed succesfully");
                return savedLayover;
            }
            catch (CruiserNotFoundException e)
            {
                _logger?.LogError(e, "Database exception when trying to create a new layover for a given cruiser");
                return BadRequest(new ErrorMessage(e.Message));
            }
        }
        /// <summary>
        ///     Endpoint para eliminar una escala.
        /// </summary>
        /// <param name="layoverId">Id del layover a ser eliminado</param>
        /// <returns>OkResult con un mensaje de exito o no, segun el resultado</returns>
        
        [HttpDelete("Layover/{layoverId}")]
        public IActionResult DeleteLayover(int layoverId)
        {
            DeleteLayoverCommand deleteLayoverCommand = CommandFactory.CreateDeleteLayoverCommand(layoverId);
            deleteLayoverCommand.Execute();
            _logger?.LogError("Delete method for a given layover returned succesfully");
            return Ok("Eliminado satisfactoriamente");
        }
    }
}