using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.Repository;

namespace vacanze_back.VacanzeApi.Services.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        // GET api/location/[?countries={}]
        [HttpGet]
        public ActionResult<IEnumerable<Location>> Get([FromQuery] bool countries = false)
        {
            try
            {
                return countries == false ? LocationRepository.GetLocations() : LocationRepository.GetCountries();
            }
            catch (DatabaseException e)
            {
                // TODO: No se deberia mandar un Ok cuando falla la base de datos, se deberia mandar
                //       un error 500 o algo
                return Ok(e.Message);
            }
        }
        
        // GET api/country/[?city_of={city_id}]
        [HttpGet]
        public ActionResult<IEnumerable<Location>> GetCitiesByCountry([FromQuery] int city_of = -1)
        {
            try
            {   
                // TODO: Chequear cuando el id es -1
                return LocationRepository.GetCitiesByCountry(city_of);
            }
            catch (DatabaseException e)
            {
                // TODO: No se deberia mandar un Ok cuando falla la base de datos, se deberia mandar
                //       un error 500 o algo
                return Ok(e.Message);
            }
        }
        
    }
}