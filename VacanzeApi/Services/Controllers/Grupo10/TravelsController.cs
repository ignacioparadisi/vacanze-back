using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
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
    [EnableCors("MyPolicy")]
    [ApiController]
    public class TravelsController : ControllerBase {
        
        [HttpGet("~/api/users/{userId:int}/[controller]")]
        public ActionResult<IEnumerable<Travel>> GetTravels(int userId){
            List<Travel> travels = new List<Travel>();
            try{
                travels = TravelRepository.GetTravels(userId);
                return Ok(travels);
            }catch(WithoutExistenceOfTravelsException ex){
                return StatusCode(404,ex.Message);
            }catch(UserNotFoundException ex){
                return StatusCode(400,ex.Message);
            }catch(InternalServerErrorException ex){
                return StatusCode(500, ex.Message);
            }catch(Exception){
                return StatusCode(400);
            }
        }

        [HttpGet("{travelId}")]
        public ActionResult<IEnumerable<object>> GetReservationsByTravelAndLocation(
            int travelId, [FromQuery] int locationId, [FromQuery] string type){
            try{
                type = type.ToUpper();
                List<object> reservations = 
                        TravelRepository.GetReservationsByTravelAndLocation<object>(travelId, locationId, type.ToUpper());
                return Ok(reservations);
            }catch(InvalidReservationTypeException ex){
                return StatusCode(400, ex.Message);
            }catch(WithoutTravelReservationsException ex){
                return StatusCode(404, ex.Message);
            }catch(InternalServerErrorException ex){
                return StatusCode(500, ex.Message);
            }catch(Exception){
                return StatusCode(400);
            }
        }

        [HttpGet("{travelId:int}/locations")]
        public ActionResult<IEnumerable<Location>> GetLocationsByTravel(int travelId){
            List<Location> locationsByTravel = new List<Location>();
            try{
                locationsByTravel = TravelRepository.GetLocationsByTravel(travelId);
                return Ok(locationsByTravel);
            }catch(WithoutTravelLocationsException ex){
                return StatusCode(404,ex.Message);
            }catch(InternalServerErrorException ex){
                return StatusCode(500, ex.Message);
            }catch(Exception){
                return StatusCode(400);
            }
        }

        [Consumes("application/json")]
        [HttpPost]
        public IActionResult AddTravel([FromBody] Travel travel){
            try{
                int id = TravelRepository.AddTravel(travel);
                return Ok(travel);
            }catch(RequiredAttributeException ex){
                return StatusCode(400,ex.Message);
            }catch(UserNotFoundException ex){
                return StatusCode(404,ex.Message);
            }catch(InternalServerErrorException ex){
                return StatusCode(500, ex.Message);
            }catch(Exception){
                return StatusCode(400);
            }
        }

        [Consumes("application/json")]
        [HttpPost("{travelId:int}/locations")]
        public IActionResult AddLocationsToTravel(int travelId, [FromBody] List<Location> locations){
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

        [Consumes("application/json")]
        [HttpPut]
        public IActionResult UpdateTravel([FromBody] Travel travel){
            try{
                if(TravelRepository.UpdateTravel(travel))
                    return Ok("Las modificaciones fueron realizadas exitosamente");
                else
                    return StatusCode(400);
            }catch(InternalServerErrorException ex){
                return StatusCode(500, ex.Message);
            }   
        }
    }
}
