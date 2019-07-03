using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.Command.Locations;
using vacanze_back.VacanzeApi.LogicLayer.DTO;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;

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
        public ActionResult<IEnumerable<LocationDTO>> Get()
        {
            LocationMapper locationMapper = MapperFactory.createLocationMapper();
            GetLocationsCommand commandGetLocations =  CommandFactory.GetLocationsCommand();
            commandGetLocations.Execute ();
            return(locationMapper.CreateDTOList( commandGetLocations.GetResult()));               

            //return LocationRepository.GetLocations();
        }

        /// <summary>
        ///     Metodo para obtener los paises
        /// </summary>
        /// <returns>Objeto tipo json de los paises</returns>
        [HttpGet("countries")]
        public ActionResult<IEnumerable<LocationDTO>> GetCountries()
        {
            LocationMapper locationMapper = MapperFactory.createLocationMapper();
            GetCountriesCommand commandGetCountries =  CommandFactory.GetCountriesCommand();
            commandGetCountries.Execute ();
            return (locationMapper.CreateDTOList(commandGetCountries.GetResult()));               

            //return LocationRepository.GetCountries();
        }

        /// <summary>
        ///     Metodo para buscar los ciudades por pais
        /// </summary>
        /// <param name="countryId">ID del pais</param>
        /// <returns>Objeto tipo json de las ciudades de un pais en especifico</returns>
        /// <exception cref="LocationNotFoundException">Lanzada si no existe el location</exception>
        [HttpGet("countries/{countryId}/cities")]
        public ActionResult<IEnumerable<LocationDTO>> GetCitiesByCountry([FromRoute] int countryId)
        {
            try
            {
                GetLocationByIdCommand commandId =  CommandFactory.GetLocationByIdCommand(countryId);
                commandId.Execute ();
                commandId.GetResult(); 
                // LocationRepository.GetLocationById(countryId);
            }
            catch (LocationNotFoundException)
            {
                return NotFound($"Location with id {countryId} not found");
            }
                LocationMapper locationMapper = MapperFactory.createLocationMapper();
                GetCitiesByCountryCommand commandIByCountry =  CommandFactory.GetCitiesByCountryCommand(countryId);
                commandIByCountry.Execute ();
                return( locationMapper.CreateDTOList(commandIByCountry.GetResult()));               

            //return LocationRepository.GetCitiesByCountry(countryId);
        }
    }
}