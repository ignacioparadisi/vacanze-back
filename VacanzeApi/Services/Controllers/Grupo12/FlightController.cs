using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo12;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo12
{
    [Produces("application/json")]  
    [Route("api/Todo")]  
    public class FlightController : ControllerBase
    {
        [Route("~/api/flight-reservation")] 
        // GET api/flight-reservation
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get(){
            return Ok("69");
        }

      
    }
}
