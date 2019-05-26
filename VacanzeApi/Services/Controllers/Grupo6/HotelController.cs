using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
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
    public class HotelController : ControllerBase
    {

        // GET api/Hotel
        [HttpGet]
        public ActionResult<IEnumerable<Hotel>> GetHotels()
        {
            var con = new HotelConnection();
            var hotels = new List<Hotel>();
            try
            {
                hotels = con.GetHotels();
            }
            catch (DatabaseException e)
            {
                return Ok(e.Message);
            }

            return hotels;
        }

    }
}
