using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.Common.Entities;
using vacanze_back.Common.Entities.Grupo3;
using vacanze_back.Persistence.Connection.Grupo3;
using vacanze_back.Common.Exceptions.Grupo3;
using Newtonsoft.Json;

namespace vacanze_back.Services.Controllers.Grupo3
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
                FlightsConnection flighscon = new FlightsConnection(); 
                List<Entity> result = flighscon.Get();
                return Ok(result.ToList());
            }
            catch (DbErrorException ex)
            {
                return BadRequest(ex.Message);
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
            {
                FlightValidator validator = new FlightValidator(flight);
                validator.Validate();
                FlightsConnection flighscon= new FlightsConnection();
                flighscon.Add(flight);
                return Ok("¡Vuelo agregado con éxito!");
            }
            catch (ValidationErrorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DbErrorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}