using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo10;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo10;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo10;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;


namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo10
{
    [Produces("application/json")]
    [Route("api/[controller]")]
     
    [ApiController]
    public class TravelsController : ControllerBase {
        
        [HttpGet("~/api/users/{userId}/[controller]")]
        public ActionResult<IEnumerable<Travel>> GetTravels(long userId){
            List<Travel> travels = new List<Travel>();
            try{
                travels = TravelRepository.GetTravels(userId);
            }catch(UserNotFoundException ex){
                return BadRequest(ex.Message);
            }catch(WithoutExistenceOfTravelsException ex){
                return BadRequest(ex.Message);
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
            return Ok(travels);
        }

        [HttpGet("{travelId}")]
        public ActionResult<IEnumerable<Entity>> GetReservationsByTravelAndLocation(
            long travelId, [FromQuery] int locationId, [FromQuery] string type){
            try{
                type = type.ToUpper();
                List<Entity> reservations = 
                        TravelRepository.GetReservationsByTravelAndLocation(travelId, locationId, type.ToUpper());
                return Ok(reservations);
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{travelId}/locations")]
        public ActionResult<IEnumerable<Location>> GetLocationsByTravel(long travelId){
            List<Location> locationsByTravel = new List<Location>();
            try{
                locationsByTravel = TravelRepository.GetLocationsByTravel(travelId);
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
            return Ok(locationsByTravel);
        }

        [Consumes("application/json")]
        [HttpPost]
        public IActionResult AddTravel([FromBody] Travel travel){
            try{
                long id = TravelRepository.AddTravel(travel);
                if(id == 0)
                    return BadRequest("Lo sentimos, el viaje no pudo ser creado");
                return Ok(travel);
            }catch(NameRequiredException ex){
                return BadRequest(ex.Message);
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [Consumes("application/json")]
        [HttpPost("{travelId}/locations")]
        public IActionResult AddLocationsToTravel(long travelId, [FromBody] List<Location> locations){
            Boolean saved = false;
            try{
                saved = TravelRepository.AddLocationsToTravel(travelId, locations);
                if(saved){
                    return Ok("Las ciudades fueron agregadas satisfactoriamente");
                }
                return Ok(saved);
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}