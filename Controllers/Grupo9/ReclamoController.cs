using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.Entities.Grupo9;
using Newtonsoft.Json;

namespace vacanze_back.Controllers.Grupo9
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ReclamoController : ControllerBase
    {
        
        private Reclamo[] Reclamo =
        {
        };
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Reclamo>> Get()
        {
            Reclamo product= new Reclamo(1,2,"aja","sss","ss","aass"); 
            Console.WriteLine("Reclamo");
            return Ok(JsonConvert.SerializeObject(product)); 
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }
    }
}