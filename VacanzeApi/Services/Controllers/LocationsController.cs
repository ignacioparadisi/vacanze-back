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
        public ActionResult<IEnumerable<Location>> Get()
        {
            return LocationRepository.GetLocations();
        }

        [HttpGet("countries")]
        public ActionResult<IEnumerable<Location>> GetCountries()
        {
            return LocationRepository.GetCountries();
        }

        [HttpGet("countries/{countryId}/cities")]
        public ActionResult<IEnumerable<Location>> GetCitiesByCountry([FromRoute] int countryId)
        {
            try
            {
                LocationRepository.GetLocationById(countryId);
            }
            catch (LocationNotFoundException)
            {
                return NotFound($"Location with id {countryId} not found");
            }

            return LocationRepository.GetCitiesByCountry(countryId);
        }
    }
}