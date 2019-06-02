using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo6;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo6
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        /// <summary>
        ///     Metodo para obtener los hoteles por ubicacion o sin ella
        /// </summary>
        /// <param name="location">id de la ubicacion donde se encuentran los hoteles</param>
        /// <returns>Objeto tipo json de los hoteles</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Hotel>> Get([FromQuery] int location = -1)
        {
            return location == -1
                ? HotelRepository.GetHotels()
                : HotelRepository.GetHotelsByCity(location);
        }

        /// <summary>
        ///     Metodo para buscar los hoteles por id
        /// </summary>
        /// <param name="id">ID del hotel a buscar</param>
        /// <returns>Objeto tipo json del hotel encontrado</returns>
        /// <exception cref="HotelNotFoundException">Lanzada si no existe el hotel</exception>
        [HttpGet("{hotelId}", Name = "GetHotelById")]
        public ActionResult<Hotel> GetById([FromRoute] int hotelId)
        {
            try
            {
                return Ok(HotelRepository.GetHotelById(hotelId));
            }
            catch (HotelNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        ///     Metodo para crear un hotel
        /// </summary>
        /// <param name="hotel">Objeto Hotel a crear</param>
        /// <returns>Objeto tipo json del hotel creado</returns>
        /// <exception cref="RequiredAttributeException">Algun atributo requerido estaba como null</exception>
        /// <exception cref="InvalidAttributeException">Algun atributo tenia un valor invalido</exception>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Hotel> Create([FromBody] Hotel hotel)
        {
            try
            {
                var idFromDatabase = HotelRepository.AddHotel(hotel);
                return CreatedAtAction("Get", "hotels",
                    HotelRepository.GetHotelById(idFromDatabase));
            }
            catch (RequiredAttributeException e)
            {
                return new BadRequestObjectResult(new ErrorMessage(e.Message));
            }
            catch (InvalidAttributeException e)
            {
                return new BadRequestObjectResult(new ErrorMessage(e.Message));
            }
        }

        /// <summary>
        ///     Metodo para eliminar un hotel
        /// </summary>
        /// <param name="id">ID del hotel a eliminar</param>
        /// <returns>una respuesta 200 vacia</returns>
        [HttpDelete("{id}", Name = "DeleteHotel")]
        public ActionResult Delete([FromRoute] int id)
        {
            HotelRepository.DeleteHotel(id);
            return Ok();
        }

        /// <summary>
        ///     Metodo para modifcar un hotel
        /// </summary>
        /// <param name="hotelId">ID del hotel a modificar</param>
        /// <param name="dataToUpdate">Objeto hotel con la data para el hotel a modificar</param>
        /// <returns>Objeto tipo json del hotel modificado</returns>
        /// <exception cref="HotelNotFoundException">El hotel a modifcar no existe</exception>
        /// <exception cref="RequiredAttributeException">Algun atributo requerido estaba como null</exception>
        /// <exception cref="InvalidAttributeException">Algun atributo tenia un valor invalido</exception>
        [HttpPut("{hotelId}", Name = "UpdateHotel")]
        public ActionResult<Hotel> Update([FromRoute] int hotelId, [FromBody] Hotel dataToUpdate)
        {
            try
            {
                HotelRepository.GetHotelById(hotelId);
                var updated = HotelRepository.UpdateHotel(hotelId, dataToUpdate);
                return Ok(updated);
            }
            catch (HotelNotFoundException)
            {
                return new NotFoundObjectResult(
                    new ErrorMessage($"Hotel con id {hotelId} no conseguido"));
            }
            catch (RequiredAttributeException e)
            {
                return new BadRequestObjectResult(new ErrorMessage(e.Message));
            }
            catch (InvalidAttributeException e)
            {
                return new BadRequestObjectResult(new ErrorMessage(e.Message));
            }
        }

        /// <summary>
        ///     Metodo para obtener la imagen de un hotel
        /// </summary>
        /// <param name="hotelId">ID del hotel que posee la imagen</param>
        /// <returns>Un string base64 de la imagen del hotel</returns>
        [HttpGet("{hotelId}/image")]
        public string GetHotelImage([FromRoute] int hotelId)
        {
            return HotelRepository.GetHotelImage(hotelId);
        }
    }
}