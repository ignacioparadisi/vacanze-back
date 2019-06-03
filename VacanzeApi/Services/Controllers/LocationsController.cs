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
        /// <summary>
        ///     Metodo para obtener las ubicacions
        /// </summary>
        /// <returns>Objeto tipo json de los locations</returns>
        // GET api/location/[?countries={}]
        [HttpGet]
        public ActionResult<IEnumerable<Location>> Get()
        {
            return LocationRepository.GetLocations();
        }

        /// <summary>
        ///     Metodo para obtener los paises
        /// </summary>
        /// <returns>Objeto tipo json de los paises</returns>
        [HttpGet("countries")]
        public ActionResult<IEnumerable<Location>> GetCountries()
        {
            return LocationRepository.GetCountries();
        }

        /// <summary>
        ///     Metodo para buscar los ciudades por pais
        /// </summary>
        /// <param name="countryId">ID del pais</param>
        /// <returns>Objeto tipo json de las ciudades de un pais en especifico</returns>
        /// <exception cref="LocationNotFoundException">Lanzada si no existe el location</exception>
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