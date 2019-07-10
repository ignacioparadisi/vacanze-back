
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo7;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo7
{
    /// <summary>  
    ///  Clase para manejar las peticiones HTTP relacionadas a los restaurantes  
    /// </summary> 
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class RestaurantsController : Controller
    {
        private readonly ILogger<RestaurantsController> _logger;
        /// <summary>
        ///     Get api/Restaurants
        ///     Metodo para obtener todos los restaurantes
        /// </summary>
        /// <returns>Objeto tipo JSON con los restaurantes obtenidos</returns>
        public RestaurantsController(ILogger<RestaurantsController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> Get()
        {
            try
            {
                GetRestaurantsCommand getRestaurantsCommand = CommandFactory.CreateGetRestaurantsCommand();
                getRestaurantsCommand.Execute();
                List<RestaurantDto> restaurantsDtoList = getRestaurantsCommand.GetResult();
                _logger?.LogInformation("Los restaurantes fueron obtenidos exitosamente");
                return restaurantsDtoList; 
            }
            catch (DatabaseException ex)
            {
                _logger?.LogError(ex, "Database exception when trying to get a restaurant by id");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        ///     Get api/Restaurants/location/id
        ///     Metodo para obtener todos los restaurantes de una ciudad dada
        /// </summary>
        /// <param name="id">Identificador unico de la ciudad a la que pertenecen los restaurantes</param>
        /// <returns>Objeto tipo JSON con los restaurantes obtenidos</returns>
        [HttpGet("location/{id}")]
        public ActionResult<IEnumerable<RestaurantDto>> GetRestaurantByLocation(int id)
        {
            try
            {
                GetRestaurantsByCityCommand getRestaurantsByCityCommand = CommandFactory.CreateGetRestaurantsByCityCommand(id);
                getRestaurantsByCityCommand.Execute();
                List<RestaurantDto> restaurantsDtoList = getRestaurantsByCityCommand.GetResult();
                _logger?.LogInformation($"Los restaurantes por la locacion de ID {id} fueron obtenidos exitosamente");
                return restaurantsDtoList;
            }
            catch (DatabaseException ex)
            {
                _logger?.LogError(ex, "Database exception when trying to get a restaurant by location id");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        ///     Get api/Restaurants/id
        ///     Metodo para obtener un unico restaurant
        /// </summary>
        /// <param name="id">Identificador unico del restaurant a obtener</param>
        /// <returns>Objeto tipo JSON con el restaurant obtenido</returns>
        /// <exception cref="DatabaseException">Ocurrio una excepcion en la ejecución de la función</exception>
        /// <exception cref="RestaurantNotFoundExeption">El restaurant buscado no existe</exception>
        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> GetRestaurant(int id)
        {
            try
            {
                GetRestaurantCommand getRestaurantCommand = CommandFactory.CreateGetRestaurantCommand(id);
                getRestaurantCommand.Execute();
                RestaurantDto restaurant = getRestaurantCommand.GetResult();
                _logger?.LogInformation($"El restaurante de ID {id} fue obtenido exitosamente");
                return restaurant;
            }
            catch (RestaurantNotFoundExeption e)
            {
                _logger.LogWarning($"Restaurant con ID {id} no conseguido");
                return BadRequest(new ErrorMessage(e.Message));
            } catch (DatabaseException ex)
            {
                _logger?.LogError(ex, "Database exception when trying to get a restaurant by id");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        ///     Post /api/Restaurants
        ///     Endpoint para crear un restaurant
        /// </summary>
        /// <param name="restaurant">Objeto Restaurant a crear</param>
        /// <returns>Objeto tipo JSON del restaurant creado</returns>
        /// <exception cref="InvalidAttributeException">Alguno de los campos ingresados en el dto no es valido</exception>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<RestaurantDto> PostRestaurant([FromBody] RestaurantDto restaurant)
        {
            try
            {
                AddRestaurantCommand addRestaurantCommand = CommandFactory.CreateAddRestaurantCommand(restaurant);
                addRestaurantCommand.Execute();
                RestaurantDto savedRestaurant = addRestaurantCommand.GetResult();
                _logger?.LogInformation($"Restaurante creado exitosamente con el ID {savedRestaurant.Id}");
                return savedRestaurant;
            }
            catch (InvalidAttributeException e)
            {
                _logger?.LogError(e, "Error en campos de la entidad restaurante");
                return BadRequest(new ErrorMessage(e.Message));
            }catch (DatabaseException ex)
            {
                _logger?.LogError(ex, "Database exception when trying to add a restaurant");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        ///    Put api/Restaurants
        ///     Endpoint para modifcar un restaurant
        /// </summary>
        /// <param name="restaurant">Objeto restaurant con la data para el restaurant a modificar</param>
        /// <returns>Objeto tipo JSON del restaurant modificado</returns>
        /// <exception cref="RestaurantNotFoundExeption">Retorna BadRequest en caso de no encontrar el restaurante a actualizar</exception>
        /// <exception cref="InvalidAttributeException">Alguno de los campos ingresados en el ResturantDto no es valido</exception>
        [HttpPut]
         public ActionResult<RestaurantDto> PutRestaurant([FromBody] RestaurantDto restaurant)
         {
             try
             {
                 UpdateRestaurantCommand updateRestaurantCommand = CommandFactory.CreateUpdateRestaurantCommand(restaurant);
                 updateRestaurantCommand.Execute();
                 RestaurantDto updatedRestaurant = updateRestaurantCommand.GetResult();
                 _logger?.LogInformation($"Restaurante con ID {updatedRestaurant.Id} actualizado correctamente");
                 return updatedRestaurant;
             }
             catch(RestaurantNotFoundExeption e)
             {
                 _logger.LogWarning($"Restaurant con ID {restaurant.Id} no conseguido");
                 return BadRequest(new ErrorMessage(e.Message));
             }
             catch (InvalidAttributeException e)
             {
                 _logger?.LogError(e, "Error en campos de la entidad restaurante");
                 return BadRequest(new ErrorMessage(e.Message));
             }catch (DatabaseException ex)
             {
                 _logger?.LogError(ex, "Database exception when trying to update a restaurant");
                 return StatusCode(500, ex.Message);
             }
         }
        
        /// <summary>
        ///     Delete api/Restaurants/{id}
        ///     Metodo para eliminar un restaurante 
        /// </summary>
        /// <param name="id">Identificador unico del restaurant a eliminar</param>
        /// <returns>Ok Result</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteRestaurant(int id)
        {
            try
            {
                DeleteRestaurantCommand deleteRestaurantCommand = CommandFactory.CreateDeleteRestaurantCommand(id);
                deleteRestaurantCommand.Execute();
                _logger?.LogInformation($"Restaurante con ID {id} eliminado satisfactoriamete");
                return Ok("Eliminado satisfactoriamente");
            }
            catch (RestaurantNotFoundExeption e)
            {
                _logger.LogWarning($"Restaurant con ID {id} no conseguido");
                return BadRequest(new ErrorMessage(e.Message));
            }
            catch (DatabaseException ex) 
            {
                _logger?.LogError(ex, "Database exception when trying to update a restaurant");
                return StatusCode(500, ex.Message);
            }
        }
    }
}