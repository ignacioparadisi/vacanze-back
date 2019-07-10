
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

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo8
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class CruiserController : Controller
    {
        // GET/Cruisers
        /// <summary>
        ///     Endpint para obtener todos los cruveros guardados.
        /// </summary>
        /// <returns>OKResult en caso de exito con un JsonObject de la lista de cruceros guardada en la base de datos</returns>
        /// <returns>BadRequest en caso de error con JsonObject del error arrojado por alguna de las excepciones </returns>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la base de
        ///     datos
        /// </exception>
        [HttpGet]
        public ActionResult<IEnumerable<CruiserDTO>> Get()
        {
            GetCruisersCommand getCruisersCommand = CommandFactory.CreateGetCruisersCommand();
                getCruisersCommand.Execute();
                List<CruiserDTO> cruisersDtoList = getCruisersCommand.GetResult();
                return cruisersDtoList; 
        }

        // GET/Cruiser/{id}
        /// <summary>
        ///     Endpoint para obtener objeto Crucero correspondiente a los datos guardados para el ID recibido.
        /// </summary>
        /// <param name="id">ID del crucero a obtener</param>
        /// <returns>OkResult en caso de exito con un JsonObject del crucero obtenido</returns>
        /// <returns>BadRequest en caso de error con un JsonObject del error arrojado por alguna de las excepciones </returns>
        /// <exception cref="CruiserNotFoundException">Lanzada si no existe un Crucero para el ID recibido</exception>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la bse de
        ///     datos
        /// </exception>
        [HttpGet("{id}")]
        public ActionResult<CruiserDTO> GetCruiser(int id)
        {
            try
            {
                GetCruiserCommand getCruiserCommand = CommandFactory.CreateGetCruiserCommand(id);
                getCruiserCommand.Execute();
                CruiserDTO cruiser = getCruiserCommand.GetResult();
                return cruiser;
            }
            catch (CruiserNotFoundException e)
            {
                return BadRequest(new ErrorMessage(e.Message));
            }
        }
        // POST/Cruiser/{id}
        /// <summary>
        ///     Endpoint para añadir un Crucero.
        /// </summary>
        /// <param name="cruiser">Datos a ser guardados en tipo crucero</param>
        /// <returns>OkResult en caso exito con un JsonObject del crucero agregado</returns>
        ///<returns>BadRequest en caso de error con un JsonObject del error arrojado por alguna de las excepciones </returns>
        /// <exception cref="InvalidAttributeException">Algun atributo tenia un valor invalido retorna</exception>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la bse de
        ///     datos
        /// </exception>
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
                return savedCruiser;
            }
            catch (InvalidAttributeException e)
            {
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
        /// <exception cref="NullCruiserException">El metodo recibio null como parametro</exception>
        /// <exception cref="CruiserNotFoundException">No se encontro el crucero con el Id sumunistrado en los parametros</exception>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la bse de
        ///     datos
        /// </exception>
        [HttpPut]
         public ActionResult<CruiserDTO> PutCruiser([FromBody] CruiserDTO cruiser)
         {
             try
             {
                 UpdateCruiserCommand updateCruiserCommand = CommandFactory.CreateUpdateCruiserCommand(cruiser);
                 updateCruiserCommand.Execute();
                 CruiserDTO updatedCruiser = updateCruiserCommand.GetResult();
                 return updatedCruiser;
             }
             catch(CruiserNotFoundException e)
             {
                 return BadRequest(new ErrorMessage(e.Message));
             }
             catch (InvalidAttributeException e)
             {
                 return BadRequest(new ErrorMessage(e.Message));
             }
         }
        
        /// <summary>
        ///     Endpoint para eliminar un Crucero.
        /// </summary>
        /// <param name="id">Id del crucero a ser eliminado</param>
        /// <returns>OkResult en caso de exito con un JsonObject del id del crrucero eliminado</returns>
        /// <returns>BadRequest en caso de error con un Json del error arrojado por alguna de las excepciones </returns>
        /// <exception cref="CruiserNotFoundException">Retornado si el id ingresado no corresponde con ningun crucero</exception>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la base de
        ///     datos
        /// </exception>
        [HttpDelete("{id}")]
        public IActionResult DeleteCruiser(int id)
        {
            DeleteCruiserCommand deleteCruiserCommand = CommandFactory.CreateDeleteCruiserCommand(id);
                deleteCruiserCommand.Execute();
                return Ok("Eliminado satisfactoriamente");
        }
        
        /// <summary>
        ///     Endpoint para obtener todos los layovers (escalas) de un crucero.
        /// </summary>
        /// <param name="cruiserId">Id del crucero del cual se desea obtener los escalas</param>
        /// <returns>OkResult en caso de exito con un JsonObject de la lista de layovers (escalas) de un crucero</returns>
        /// <returns>BadRequest en caso de error con un JsonObject del error arrojado por alguna de las excepciones </returns>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la base de
        ///     datos
        /// </exception>
        [HttpGet("{cruiserId}/Layover")]
        public ActionResult<IEnumerable<LayoverDTO>> GetLayovers(int cruiserId)
        {
                GetLayoversCommand getLayoversCommand = CommandFactory.CreateGetLayoversCommand(cruiserId);
                getLayoversCommand.Execute();
                List<LayoverDTO> layoverDtoList = getLayoversCommand.GetResult();
                return layoverDtoList; 
        }
        /// <summary>
        ///     Endpoint para agregar un layover a un crucero.
        /// </summary>
        /// <param name="layover">Id del crucero al que se le agregara la escala</param>
        /// <returns>Okresult en caso de exito con un JsonObject de la lista de layovers (escalas) de un crucero</returns>
        /// <returns>BadRequest en caso de error con un JsonObject del error arrojado por alguna de las excepciones </returns>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la base de
        ///     datos
        /// </exception>
        /// <exception cref="CruiserNotFoundException">Lanzada si el id del crucero en el layover no corresponde con ningun crucero guardado</exception>
        /// <exception cref="InvalidAttributeException">Algun atributo tenia un valor invalido</exception>
        
        [HttpPost("{cruiserId}/Layover")]
        public ActionResult<LayoverDTO> PostLayover([FromBody] LayoverDTO layover)
        {
            try
            {
                AddLayoverCommand addLayoverCommand = CommandFactory.CreateAddLayoverCommand(layover);
                addLayoverCommand.Execute();
                LayoverDTO savedLayover = addLayoverCommand.GetResult();
                return savedLayover;
            }
            catch (InvalidAttributeException e)
            {
                return BadRequest(new ErrorMessage(e.Message));
            }
        }
        /// <summary>
        ///     Endpoint para eliminar una escala.
        /// </summary>
        /// <param name="layoverId">Id del layover a ser eliminado</param>
        /// <returns>OkResult en caso de exito con un JsonObeject del id de la escala eliminada</returns>
        /// <returns>BadRequest en caso de error con un JsonObject del error arrojado por alguna de las excepciones </returns>
        /// <exception cref="LayoverNotFoundException">Retornado si el id ingresado no corresponde con ningun layover</exception>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la base de
        ///     datos
        /// </exception>
        
        [HttpDelete("Layover/{layoverId}")]
        public IActionResult DeleteLayover(int id)
        {
            DeleteLayoverCommand deleteLayoverCommand = CommandFactory.CreateDeleteLayoverCommand(id);
                deleteLayoverCommand.Execute();
                return Ok("Eliminado satisfactoriamente");
        }
        
        /// <summary>
        ///     Endpoint para obtener todos los layovers (escalas) de un crucero.
        /// </summary>
        /// <param name="departure">Id del la locacion de salida del crucero</param>
        /// <param name="arrival">Id de la locacion de llegada del crucero</param>
        /// <returns>OkResult en caso de exito con un JsonObject de la lista de layovers (escalas) disponibles para las locaciones ingresadas</returns>
        /// <returns>BadRequest en caso de error con un JsonObject del error arrojado por alguna de las excepciones </returns>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la base de
        ///     datos
        /// </exception>
        /// <exception cref="LayoverNotFoundException">Si no se encontraron rutas para las locaciones ingresadass</exception>
        [HttpGet("{departure}/{arrival}/Layover")]
        public ActionResult<IEnumerable<Layover>> GetLayoverByLoc(int departure, int arrival)
        {
            try
            {
                var layovers = CruiserRepository.GetLayoversForRes(departure,arrival);
                return Ok(layovers);
            }
            catch (LayoverNotFoundException e)
            {
                return NotFound(new ErrorMessage(e.Message));
            }
            catch (DatabaseException e)
            {
                return BadRequest(new ErrorMessage(e.Message));
            }
        }
    }
}