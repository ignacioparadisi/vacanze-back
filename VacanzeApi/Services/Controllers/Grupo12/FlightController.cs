using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Entities.Grupo12;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo12;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo12;
using vacanze_back.VacanzeApi.Common.Entities;

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
            
            }catch(Exception e){
                Console.WriteLine(e);
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

            }catch(Exception ex ){

                 return BadRequest(ex.Message);
            }

        }


        [Route("~/api/id-return-city/{name_city_i}/{name_city_v}")] 
        // GET api/list-reservation-flight
        [HttpGet("{name_city_i}/{name_city_v}")]
        public ActionResult<IEnumerable<int>> Get(string name_city_i,string name_city_v){
            try{
                List<int> lis_id=new List<int>();
                FlightResConnection con=new FlightResConnection();
                int id_locationI = con.GetIDLocation(name_city_i);
                int id_locationV = con.GetIDLocation(name_city_v);
                lis_id.Add(id_locationI);
                lis_id.Add(id_locationV);
                return lis_id;
            }catch(Exception ex ){

                 return BadRequest(ex.Message);
            }

        }

        

         [Route("~/api/delete-reservation-flight/{id_res}")] 
        // GET api/list-reservation
        [HttpDelete("{id_res}")]
        public ActionResult<IEnumerable<String>> Delete(int id_res){

            try{
                Console.Write(id_res);
                FlightResConnection con=new FlightResConnection();
                con.DeleteReservationFlight(id_res);
                return StatusCode(200, "Se elimino satisfactoriamente");

            }catch(Exception ex ){

                return BadRequest(ex.Message);
            }

        }


          /// <summary> GET api/outbounds/departure/arrival/departuredate</summary>
        /// <param name="departure">Ciudad de salida del vuelor</param>
        /// <param name="arrival">Ciudad de llegada del vuelo</param>
        /// <param name="departuredate">Fecha de salida del vuelo</param>
        /// <returns>ActionResult con resultado de la query</returns>
        

        [Route("~/api/reservation-flight/{departure}/{arrival}/{departuredate}/{numpas}")] 
        [HttpGet("{departure}/{arrival}/{departuredate}/{numpas}")]
        public ActionResult<IEnumerable<ListRes>> Get(int departure, int arrival, string departuredate,int numpas)
        {
            try
            {
                Console.WriteLine("Estoy Aqui");
                FlightResConnection con=new FlightResConnection();
                List<ListRes> listFlight=con.GetFlightValidate(departure, arrival, departuredate,numpas);
                return listFlight;
            }
            catch (DatabaseException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return null;
            }
        }

        

        
       
	}


    
}