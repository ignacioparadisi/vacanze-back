using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace vacanze_back.Controllers.Grupo2
{   
    [Route("api/[controller]")]
    [ApiController]
    public class TravelController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetTravels()
        {
            return new string[] { "Travel1", "Travel2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> GetTravel(int id)
        {
            return "Travel";
        }
            
    }
}
