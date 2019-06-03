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

        /// <summary> POST api/flight-reservation</summary>
        /// <returns>Devuelve el id de la reserva que se agrego</returns>

        [Route("~/api/flight-reservation")] 
        // POST api/flight-reservation
        [HttpPost]
        public ActionResult<IEnumerable<string>> Post(FlightRes flight){

            try{
                DateTime dateLocal = DateTime.Now;
                FlightResConnection con=new FlightResConnection();
                string seat=con.conSeatNum(flight._numPas,flight._id_fli);
                FlightRes f=new FlightRes(seat,flight._timestamp,
                flight._numPas,flight._id_user,flight._id_fli);
                int i = con.AddReservationFlight(f);
                
                return StatusCode(200, "Se agrego satisfactoriamente id:"+i);

            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
       

       
        /// <summary> GET api/list-reservation-flight/id_user</summary>
        /// <param name="id_user">Id del usuario</param>
        /// <returns>Devuelve una lista de todas las reservas de realizo</returns>

        [Route("~/api/list-reservation-flight/{id_user}")] 
        // GET api/list-reservation-flight
        [HttpGet("{id_user}")]
        public ActionResult<IEnumerable<FlightRes>> Get(int id_user){

            try{
                FlightResConnection con=new FlightResConnection();
                List<FlightRes> listFlight = con.GetReservationFlight(id_user);
                return listFlight;

            }catch(Exception ex ){

                 return BadRequest(ex.Message);
            }

        }
        
        
        /// <summary> GET api/id-return-city/name_city_i/name_city_v</summary>
        /// <param name="name_city_i">Nombre de la ciudad de origen</param>
        /// <param name="name_city_v">Nombre de la ciudad de destino</param>
        /// <returns>Devuelve el id de los paises de origen y destino</returns>

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


        /// <summary> GET api/reservation-flight/id_res</summary>
        /// <param name="id_res">ID de la reserva</param>
        /// <returns>Devuelve un mensaje de satisfactorio si elimina la reserva</returns>
        
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

       

        /// <summary> GET api/reservation-flight/departure/arrival/departuredate</summary>
        /// <param name="departure">Ciudad de salida del vuelor</param>
        /// <param name="arrival">Ciudad de llegada del vuelo</param>
        /// <param name="departuredate">Fecha de salida del vuelo</param>
        /// <returns>Devuelve todas las reservas en una fecha indicada</returns>
        
        [Route("~/api/reservation-flight/{departure}/{arrival}/{departuredate}/{numpas}")] 
        [HttpGet("{departure}/{arrival}/{departuredate}/{numpas}")]
        public ActionResult<IEnumerable<ListRes>> Get(int departure, int arrival, string departuredate,int numpas)
        {
            try
            {
                FlightResConnection con=new FlightResConnection();
                List<ListRes> listFlight=con.GetFlightValidateI(departure, arrival, departuredate,numpas);
                return listFlight;
            }
            catch (DatabaseException ex)
            {
                return BadRequest(ex.Message);
            }
           
        }



        /// <summary> GET api/reservation-flight/departure/arrival/departuredate/arrivaldate/numpas</summary>
        /// <param name="departure">Ciudad de salida del vuelor</param>
        /// <param name="arrival">Ciudad de llegada del vuelo</param>
        /// <param name="departuredate">Fecha de salida del vuelo</param>
        /// <param name="arrivaldate">Fecha de Regreso del vuelo</param>
        /// <param name="numpas">Cantidad de Pasajeros que se quiere reservar </param>
        /// <returns>Devuelve todas las reservas en una fecha indicada</returns>

        [Route("~/api/reservation-flight/{departure}/{arrival}/{departuredate}/{arrivaldate}/{numpas}")] 
        [HttpGet("{departure}/{arrival}/{departuredate}/{arrivaldate}/{numpas}")]
        public ActionResult<IEnumerable<ListRes>> Get(int departure, int arrival, string departuredate,string arrivaldate,int numpas)
        {
            try
            {
                FlightResConnection con=new FlightResConnection();
                List<ListRes> listFlight=con.GetReservationFlightIV(departure, arrival, departuredate,arrivaldate,numpas);
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
