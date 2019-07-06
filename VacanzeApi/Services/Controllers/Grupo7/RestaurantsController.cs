using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        /// <summary>
        ///     Get api/Restaurants
        ///     Metodo para obtener todos los restaurantes
        /// </summary>
        /// <returns>Objeto tipo JSON con los restaurantes obtenidos</returns>
        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> Get()
        {
            GetRestaurantsCommand getRestaurantsCommand = CommandFactory.CreateGetRestaurantsCommand();
                getRestaurantsCommand.Execute();
                List<RestaurantDto> restaurantsDtoList = getRestaurantsCommand.GetResult();
                return restaurantsDtoList; 
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
            GetRestaurantsByCityCommand getRestaurantsByCityCommand = CommandFactory.CreateGetRestaurantsByCityCommand(id);
                getRestaurantsByCityCommand.Execute();
                List<RestaurantDto> restaurantsDtoList = getRestaurantsByCityCommand.GetResult();
                return restaurantsDtoList;
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
                return restaurant;
            }
            catch (RestaurantNotFoundExeption e)
            {
                return BadRequest(new ErrorMessage(e.Message));
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
                return savedRestaurant;
            }
            catch (InvalidAttributeException e)
            {
                return BadRequest(new ErrorMessage(e.Message));
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
                 return updatedRestaurant;
             }
             catch(RestaurantNotFoundExeption e)
             {
                 return BadRequest(new ErrorMessage(e.Message));
             }
             catch (InvalidAttributeException e)
             {
                 return BadRequest(new ErrorMessage(e.Message));
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
            DeleteRestaurantCommand deleteRestaurantCommand = CommandFactory.CreateDeleteRestaurantCommand(id);
                deleteRestaurantCommand.Execute();
                return Ok("Eliminado satisfactoriamente");
        }
    }
}