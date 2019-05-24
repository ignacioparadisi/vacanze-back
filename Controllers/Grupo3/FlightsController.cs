using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.Entities;
using vacanze_back.Entities.Grupo3;
using vacanze_back.Persistence.Grupo3;
using Newtonsoft.Json;

namespace vacanze_back.Controllers.Grupo3
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Entity>> Get()
        {
            try
            {
                DAOFlight daof = new DAOFlight(); 
                List<Entity> result = daof.Get();
                return Ok(result.ToList());
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult<Entity> Post([FromBody] Flight flight)
        {
            try
            {;
                DAOFlight daof= new DAOFlight();
                daof.Add(flight);
                return Ok("¡Vuelo agregado con éxito!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}