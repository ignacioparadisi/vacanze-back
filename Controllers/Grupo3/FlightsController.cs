using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.Entities.Grupo3;
using vacanze_back.Persistence.Grupo3;
using Newtonsoft.Json;

namespace vacanze_back.Controllers.Grupo3
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        [HttpPost]
        public Flight Post([FromBody] Flight flight)
        {
            try
            {;
                DAOFlight daof= new DAOFlight();
                daof.Add(flight);
                return flight;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}