using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace vacanze_back.Controllers.Grupo2
{   
    [Route("api/[controller]")]
    [ApiController]
    public class VacationController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetVacations()
        {
            return new string[] { "Vacation1", "Vacation2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> GetVacation(int id)
        {
            return "Vacation";
        }
            
    }
}
