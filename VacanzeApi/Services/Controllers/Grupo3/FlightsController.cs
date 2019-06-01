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
        /// <summary>GET api/flight</summary>
        /// <returns>Action result con resultado de la query</returns>
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

        /// <summary> GET api/flights/id</summary>
        /// <param name="id">id del vuelo a buscar</param>
        /// <returns>ActionResult con resultado de la query</returns>
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

        /// <summary> GET api/flights/begin/end</summary>
        /// <param name="begin">Rango menor de la fecha a buscar</param>
        /// <param name="end">Rango mayor de la fecha a buscar</param>
        /// <returns>ActionResult con resultado de la query</returns>
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

        /// <summary> GET api/flights/begin/end</summary>
        /// <param name="begin">Rango menor de la fecha a buscar</param>
        /// <param name="end">Rango mayor de la fecha a buscar</param>
        /// <returns>ActionResult con resultado de la query</returns>
        [HttpGet("locations/{departure}/{arrival}")]
        public ActionResult<IEnumerable<Entity>> GetByLocation(int departure, int arrival)
        {
            try
            {
                var result = FlightRepository.GetByLocation(departure, arrival);
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

        /// <summary> GET api/outbounds/departure/arrival/departuredate</summary>
        /// <param name="departure">Ciudad de salida del vuelor</param>
        /// <param name="arrival">Ciudad de llegada del vuelo</param>
        /// <param name="departuredate">Fecha de salida del vuelo</param>
        /// <returns>ActionResult con resultado de la query</returns>
        [HttpGet("outbounds/{departure}/{arrival}/{departuredate}")]
        public ActionResult<IEnumerable<Entity>> GetOutboundFlights(int departure, int arrival, string departuredate)
        {
            try
            {
                var result = FlightRepository.GetOutboundFlights(departure, arrival, departuredate);
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

        /// <summary> GET api/outbounds/departure/arrival/departuredate</summary>
        /// <param name="departure">Ciudad de salida del vuelor</param>
        /// <param name="arrival">Ciudad de llegada del vuelo</param>
        /// <param name="departuredate">Fecha de salida del vuelo</param>
        /// <returns>ActionResult con resultado de la query</returns>
        [HttpGet("roundtripflights/{departure}/{arrival}/{departuredate}/{arrivaldate}")]
        public ActionResult<IEnumerable<Entity>> GetRoundTripFlights(int departure, int arrival, string departuredate, string arrivaldate)
        {
            try
            {
                var result = FlightRepository.GetRoundTripFlights(departure, arrival, departuredate, arrivaldate);
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

        /// <summary>POST api/flights</summary>
        /// <param name="flight">Parametro tipo Flight con el vuelo a agregar</param>
        /// <returns>ActionResult con mensaje de exito si se agrego de manera exitosa</returns>
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

        /// <summary>PUT api/flights</summary>
        /// <param name="flight">Parametro tipo Flight con el vuelo a editar</param>
        /// <returns>ActionResult con mensaje de exito si se edito de manera exitosa</returns>
        [HttpPut]
        public ActionResult<Entity> Put([FromBody] Flight flight)
        {
           try
            {
                
                Flight f = (Flight) FlightRepository.Find( (int)flight.Id );

                if(f == null){
                    throw new ValidationErrorException("El vuelo a editar no existe");
                }
 
                FlightValidator validator = new FlightValidator(flight);


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

        /// <summary>DELETE api/fligts/id</summary>
        /// <param name="id">Id del vuelo a elimnar</param>
        /// <returns>ActionResult con mensaje de exito si se elimino correctamente</returns>
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