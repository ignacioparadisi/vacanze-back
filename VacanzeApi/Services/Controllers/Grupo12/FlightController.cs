using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo12;
using vacanze_back.VacanzeApi.Common.Entities.Grupo12;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo12;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo12
{
    [Produces("application/json")]  
    [Route("api/Todo")]  
    [ApiController]
    public class FlightController : ControllerBase
    {
        [Route("~/api/flight-reservation")] 
        // POST api/flight-reservation
        [HttpPost]
        public ActionResult<IEnumerable<string>> Post(ReservationFlight flight){
            try{
                
                FlightResConnection con=new FlightResConnection();
                FlightRes f=new FlightRes(flight._seatNum,flight._timestamp,
                flight._numPas,flight._id_user,flight._id_pay,flight._id_fli);
                int i=con.AddReservationFlight(f);
                return Ok(i);
          }catch (DatabaseException e)
			{            
                Console.Write(e);
				return StatusCode(501);
			}
			catch (InvalidStoredProcedureSignatureException )
			{
				return StatusCode(500);
			}
        }

        public class ReservationFlight {
        public string _seatNum{ get; set;}
        public string _timestamp{ get; set;}
        public int _numPas{get; set;}
        public int _id_user{get;set;}
        public int _id_pay{get;set;}
        public int _id_fli{get;set;}
		}
	}

      
    }
}
