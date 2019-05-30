using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo10;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo10;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo10;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo10
{
    [Produces("application/json")]
    [Route("api/[controller]")]
     
    [ApiController]
    public class TravelsController : ControllerBase {
        
        [HttpGet("~/api/users/{userId:int}/[controller]")]
        public ActionResult<IEnumerable<Travel>> GetTravels(int userId){
            List<Travel> travels = new List<Travel>();
            try{
                travels = TravelRepository.GetTravels(userId);
            }catch(WithoutExistenceOfTravelsException ex){
                return BadRequest(ex.Message);
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
            return Ok(travels);
        }

        [Consumes("application/json")]
        [HttpPost]
        public IActionResult AddTravel([FromBody] Travel travel){
            try{
                long id = TravelRepository.AddTravel(travel);
                if(id == 0)
                    return BadRequest("Lo sentimos, el vieje no pudo ser creado");
                return Ok(travel);
            }catch(NameRequiredException ex){
                return BadRequest(ex.Message);
            }
        }
    }
}