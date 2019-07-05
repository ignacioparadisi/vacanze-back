using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Newtonsoft.Json;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo7;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo7;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo7
{
    /// <summary>  
    ///  Clase para manejar las peticiones HTTP relacionadas a los restaurantes  
    /// </summary> 
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        /// <summary>
        ///     Metodo para obtener todos los restaurantes
        /// </summary>
        /// <returns>Objeto tipo JSON con los restaurantes obtenidos</returns>
        /// <exception cref="GetRestaurantExcepcion">Ocurrio una excepcion en la ejecución del repository</exception>
        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>> Get()
        {
            try
            {
                return RestaurantRepository.GetRestaurants();
            }
            catch (GetRestaurantExcepcion e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        ///     Metodo para obtener todos los restaurantes de una ciudad dada
        /// </summary>
        /// <param name="id">Identificador unico de la ciudad a la que pertenecen los restaurantes</param>
        /// <returns>Objeto tipo JSON con los restaurantes obtenidos</returns>
        /// <exception cref="GetRestaurantExcepcion">Ocurrio una excepcion en la ejecución del repository</exception>
        [HttpGet("location/{id}")]
        public ActionResult<IEnumerable<Restaurant>> GetRestaurantByLocation(int id)
        {
            try
            {
                return RestaurantRepository.GetRestaurantsByCity(id);
            }
            catch(GetRestaurantExcepcion e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        ///     Metodo para obtener un unico restaurant
        /// </summary>
        /// <param name="id">Identificador unico del restaurant a obtener</param>
        /// <returns>Objeto tipo JSON con el restaurant obtenido</returns>
        /// <exception cref="DatabaseException">Ocurrio una excepcion en la ejecución de la función</exception>
        /// <exception cref="IndexOutOfRangeException">El restaurant buscado no existe</exception>
        [HttpGet("{id}")]
        public IActionResult GetRestaurant(int id)
        {
            try
            {
                GetRestaurantCommand getRestaurantCommand = CommandFactory.CreateGetRestaurantCommand(id);
                getRestaurantCommand.Execute();
                RestaurantDTO restaurant = getRestaurantCommand.GetResult();
                return Ok(JsonConvert.SerializeObject(restaurant));
            }
            catch (IndexOutOfRangeException)
            {
                return StatusCode(500,"El Restaurant no fue encontrado");
            }
            catch(GetRestaurantExcepcion e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        ///     Metodo para crear un restaurant
        /// </summary>
        /// <param name="restaurant">Objeto Restaurant a crear</param>
        /// <returns>Objeto tipo JSON del restaurant creado</returns>
        /// <exception cref="AddRestaurantException">Ocurrio una excepcion en la ejecución del repository</exception>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Restaurant> PostRestaurant([FromBody] RestaurantDTO restaurant)
        {
            try
            {
                AddRestaurantCommand addRestaurantCommand = CommandFactory.CreateAddRestaurantCommand(restaurant);
                addRestaurantCommand.Execute();
                RestaurantDTO savedRestaurant = addRestaurantCommand.GetResult();
                return Ok(savedRestaurant);
            }
            catch(AddRestaurantException e)
            {
                return StatusCode(500, e.Message);
            }
            
        }

        /// <summary>
        ///     Metodo para modifcar un restaurant
        /// </summary>
        /// <param name="restaurant">Objeto restaurant con la data para el restaurant a modificar</param>
        /// <returns>Objeto tipo JSON del restaurant modificado</returns>
        /// <exception cref="UpdateRestaurantException">Ocurrio una excepcion en la ejecución del repository</exception>
        [HttpPut]
         public ActionResult<Restaurant> PutRestaurant([FromBody] Restaurant restaurant)
         {
             try
             {
                var UpdatedRestaurant = RestaurantRepository.UpdateRestaurant(restaurant);
                return StatusCode(200, restaurant);
             }
             catch(UpdateRestaurantException e)
             {
                 return StatusCode(500, e.Message);
             }
            
         }
        
        /// <summary>
        ///     Metodo para eliminar un restaurante 
        /// </summary>
        /// <param name="id">Identificador unico del restaurant a eliminar</param>
        /// <returns>Objeto de tipo JSON que contiene el identificador del restaurant eliminado</returns>
        /// <exception cref="DeleteRestaurantException">Ocurrio una excepcion en la ejecución del repository</exception>
        [HttpDelete("{id}")]
        public ActionResult<int> DeleteRestaurant(int id)
        {
            try
            {
                var deletedid = RestaurantRepository.DeleteRestaurant(id);
                return StatusCode(200, "Eliminado satisfactoriamente");
            }
            catch(DeleteRestaurantException e)
            {
                return StatusCode(500, e.Message);
            }
            
        }

    }
}