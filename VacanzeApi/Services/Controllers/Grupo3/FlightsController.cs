using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo3;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo3;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo3;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo3
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class FlightsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Entity>> Get()
        {
            try
            {
                var flighscon = new FlightsConnection();
                var result = flighscon.Get();
                return Ok(result.ToList());
            }
            catch (DbErrorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet("{begin}/{end}")]
        public ActionResult<IEnumerable<Entity>> GetByDate(string begin, string end)
        {
            try
            {
                var flighscon = new FlightsConnection();
                var result = flighscon.GetByDate(begin, end);
                return Ok(result.ToList());
            }
            catch (DbErrorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult<Entity> Post([FromBody] Flight flight)
        {
            try
            {
                var validator = new FlightValidator(flight);
                validator.Validate();
                var flighscon = new FlightsConnection();
                flighscon.Add(flight);
                return Ok(new {Message = "¡Vuelo agregado con éxito!"});
            }
            catch (ValidationErrorException ex)
            {
                return BadRequest(new {ex.Message});
            }
            catch (DbErrorException ex)
            {
                return BadRequest(new {ex.Message});
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}