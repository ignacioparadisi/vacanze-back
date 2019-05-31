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
    [EnableCors("MyPolicy")]
	[ApiController]
    
    public class FlightController : ControllerBase
    {
        [Route("~/api/flight-reservation")] 
        // POST api/flight-reservation
        [HttpPost]
        public ActionResult<IEnumerable<string>> Post(FlightRes flight){

            try{
                
                FlightResConnection con=new FlightResConnection();
                FlightRes f=new FlightRes(flight._seatNum,flight._timestamp,
                flight._numPas,flight._id_user,flight._id_pay,flight._id_fli);
                con.AddReservationFlight(f);
                return StatusCode(200, "Se agrego satisfactoriamente");
                
            }catch(Exception ){

                 return StatusCode(404, "Error");
            
            }

        }

        [Route("~/api/list-reservation-flight/{id_user}")] 
        // GET api/list-reservation-flight
        [HttpGet("{id_user}")]
        public ActionResult<IEnumerable<FlightRes>> Get(int id_user){

            try{
                
                Console.WriteLine(id_user);
                FlightResConnection con=new FlightResConnection();
                List<FlightRes> listFlight = con.GetReservationFlight(id_user);
                return listFlight;

            }catch(Exception ){

                 return StatusCode(404, "Error");
            }

        }

         [Route("~/api/delete-reservation-flight/{id_res}")] 
        // GET api/list-reservation
        [HttpDelete("{id_res}")]
        public ActionResult<IEnumerable<String>> Delete(int id_res){

            try{
                
                FlightResConnection con=new FlightResConnection();
                con.DeleteReservationFlight(id_res);
                return StatusCode(200, "Se elimino satisfactoriamente");

            }catch(Exception ){

                return StatusCode(404, "Error");
            }

        }

        
       
	}


    
}
