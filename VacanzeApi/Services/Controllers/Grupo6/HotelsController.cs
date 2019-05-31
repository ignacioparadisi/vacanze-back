using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.Repository;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo6;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo6
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Hotel>> Get([FromQuery] int location = -1)
        {
            return location == -1
                ? HotelRepository.GetHotels()
                : HotelRepository.GetHotelsByCity(location);
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
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Hotel> Create([FromBody] Hotel hotel)
        {
            try
            {
                LocationRepository.GetLocationById(hotel.Location.Id);
            }
            catch (EntityNotFoundException)
            {
                ModelState.AddModelError("Location",
                    $"Invalid location ID {hotel.Location.Id} (Not found)");
                return new BadRequestObjectResult(ModelState);
            }

            var idFromDatabase = HotelRepository.AddHotel(hotel);
            return CreatedAtAction("Get", "hotels",
                HotelRepository.GetHotelById(idFromDatabase));
        }

        [HttpDelete("{id}", Name = "DeleteHotel")]
        public ActionResult Delete([FromRoute] int id)
        {
            HotelRepository.DeleteHotel(id);
            return Ok();
        }

        [HttpPut("{hotelId}", Name = "UpdateHotel")]
        public ActionResult<Hotel> Update([FromRoute] int hotelId, [FromBody] Hotel dataToUpdate)
        {
            // TODO: Buscar por id primero a ver si existe
            var updated = HotelRepository.UpdateHotel(hotelId, dataToUpdate);
            return Ok(updated);
        }
    }
}