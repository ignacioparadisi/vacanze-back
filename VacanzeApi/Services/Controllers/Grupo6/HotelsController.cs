using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.Connection.Grupo6;

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
        public ActionResult<IEnumerable<Hotel>> Get([FromQuery] long location = -1)
        {
            var dbConnection = new HotelConnection();
            try
            {
                if (location == -1)
                    return dbConnection.GetHotels();
                return Ok($"No implementado todavia. Recibi location: {location}");
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
            var savedHotel = new Hotel(receivedId, hotel.Name, hotel.AmountOfRooms, hotel.IsActive,
                hotel.Phone,
                hotel.Website);
            return CreatedAtAction("Get", "hotels", savedHotel);
        }
    }
}