using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo6;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo6
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        // GET api/hotels/[?location={location_id}]
        [HttpGet]
        public ActionResult<IEnumerable<Hotel>> Get([FromQuery] int location = -1)
        {
            try
            {
                if (location == -1)
                {
                    return HotelRepository.GetHotels();
                }
                return HotelRepository.GetHotelsByCity(location);
            }
            catch (DatabaseException e)
            {
                // TODO: No se deberia mandar un Ok cuando falla la base de datos, se deberia mandar
                //       un error 500 o algo
                return Ok(e.Message);
            }
        }

        [HttpGet("{hotelId}", Name = "GetHotelById")]
        public ActionResult<Hotel> GetById([FromRoute] int hotelId)
        {
            try
            {
                return HotelRepository.GetHotelById(hotelId);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (DatabaseException e)
            {
                // TODO: No se deberia mandar un Ok cuando falla la base de datos, se deberia mandar
                //       un error 500 o algo
                return Ok(e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Hotel> Create([FromBody] Hotel hotel)
        {
            var receivedId = HotelRepository.AddHotel(hotel);
            // TODO: Si le pasas un ID de un location que no existe, explota. Arreglar
            return CreatedAtAction("Get", "hotels",
                HotelRepository.GetHotelById(Convert.ToInt32(receivedId)));
        }
    }
}