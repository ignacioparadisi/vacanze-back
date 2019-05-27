using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo3;
using vacanze_back.VacanzeApi.Persistence.Connection.Grupo3;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo3;
using Newtonsoft.Json;

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

        [HttpGet("{begin}/{end}")]
        public ActionResult<IEnumerable<Entity>> GetByDate(string begin, String end)
        {
            try
            {
                FlightsConnection flighscon = new FlightsConnection(); 
                List<Entity> result = flighscon.GetByDate(begin,end);
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
                return Ok( new {Message = "¡Vuelo agregado con éxito!"});
            }
            catch (ValidationErrorException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
            catch (DbErrorException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        [HttpPut]
        public ActionResult<Entity> Put([FromBody] Flight flight)
        {
           try
            {
                //return Ok( new {Message = flight.id});
                FlightsConnection flightcon = new FlightsConnection();
                Flight f = (Flight) flightcon.Find(flight.id);

                if(f == null){
                    throw new ValidationErrorException("El vuelo a editar no existe");
                }
 
                FlightValidator validator = new FlightValidator(f);


                validator.Validate();
                flightcon.Update(flight);
                
                return Ok( new {Message = "¡Vuelo editado con éxito!"});
            }
            catch (ValidationErrorException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
            catch (DbErrorException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }


        [HttpDelete("{id}")]    
        public ActionResult<Entity> Delete(int id)    
        {    
            return Ok(id);
        }

    }
}