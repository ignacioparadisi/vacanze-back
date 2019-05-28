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
                var result = FlightRepository.Get();
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

        [HttpGet("{id}")]
        public ActionResult<Entity> Find(int id)
        {
            try
            {
                var result = FlightRepository.Find(id);
                return Ok(result);
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
                var result = FlightRepository.GetByDate(begin, end);
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

                FlightRepository.Add(flight);
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

        [HttpPut]
        public ActionResult<Entity> Put([FromBody] Flight flight)
        {
           try
            {
                
                Flight f = (Flight) FlightRepository.Find( (int)flight.Id );

                if(f == null){
                    throw new ValidationErrorException("El vuelo a editar no existe");
                }
 
                FlightValidator validator = new FlightValidator(f);


                validator.Validate();
                FlightRepository.Update(flight);

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
            try
            {

                Flight f = (Flight)FlightRepository.Find((int)id);

                if (f == null)
                {
                    throw new ValidationErrorException("El vuelo a eliminar no existe");
                }

                FlightValidator validator = new FlightValidator(f);

                FlightRepository.Delete(f);

                return Ok(new { Message = "¡Vuelo elimindado con éxito!" });
            }
            catch (ValidationErrorException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (DbErrorException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            //return Ok(id);
        }

    }
}