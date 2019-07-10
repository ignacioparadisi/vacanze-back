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
using vacanze_back.VacanzeApi.LogicLayer.DTO;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo12;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo12;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo12;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo12
{
    [Produces("application/json")]  
    [Route("api/Todo")]  
    [EnableCors("MyPolicy")]
	[ApiController]
    
    public class FlightReservationController : ControllerBase
    {

        /// <summary> POST api/flight-reservation</summary>
        /// <returns>Devuelve el id de la reserva que se agrego</returns>

        [Route("~/api/flight-reservation")] 
        // POST api/flight-reservation
        [HttpPost]
        public ActionResult<IEnumerable<string>> Post(FlightResDTO flightDTO){

            try{

                //Crea la entidad por medio del mapper devuelvo del factory
                ReservationFlightMapper ResFlightMapper = MapperFactory.CreateReservationFlightMapper();
                Entity entity = ResFlightMapper.CreateEntity(flightDTO);

                //Instancia el comando por medio del factory pasandole la entidad al constructor
                AddReservationFlightCommand command = CommandFactory.CreateAddReservationFlightCommand ((FlightRes)entity);

                //Ejecuta y obtiene el resultado del comando
                command.Execute ();
                int _id = command.GetResult();


                return StatusCode(200, "Se agrego satisfactoriamente id: " + _id);

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
        public ActionResult<IEnumerable<Entity>> Get(int id_user){

            try{
                

                GetReservationFlightByUserCommand command = 
                CommandFactory.CreateGetReservationFlightByUserCommand(id_user);
                command.Execute();
                List<Entity> listFlight = command.GetResult();

                return listFlight;

            }catch(Exception ex){

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
                List<string> city_names = new List<string>();
                city_names.Add(name_city_i);
                city_names.Add(name_city_v);

                GetIdReturnCityCommand command = CommandFactory.CreateGetIdReturnCityCommand(city_names);

                command.Execute();
                List<int> city_ids = command.GetResult();

                return city_ids;
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

                DeleteReservationCommand command = CommandFactory.CreateDeleteReservationCommand(id_res);

                command.Execute();

                return StatusCode(200, "Se elimino satisfactoriamente");

            }catch(Exception ex ){

                return BadRequest(ex.Message);
            }

        }

       
//
        /// <summary> GET api/reservation-flight/departure/arrival/departuredate/numpas</summary>
        /// <param name="departure">Ciudad de salida del vuelor</param>
        /// <param name="arrival">Ciudad de llegada del vuelo</param>
        /// <param name="departuredate">Fecha de salida del vuelo</param>
        /// <param name="numpas"></param>
        /// <returns>Devuelve todas las reservas en una fecha indicada</returns>
        
        [Route("~/api/reservation-flight/{departure}/{arrival}/{departuredate}/{numpas}")] 
        [HttpGet("{departure}/{arrival}/{departuredate}/{numpas}")]
        public ActionResult<IEnumerable<Entity>> Get(int departure, int arrival, string departuredate,int numpas)
        {
            try
            {
                GetReservationsByDateICommand command = 
                    CommandFactory.CreateGetReservationsByDateICommand(departure, arrival, departuredate, numpas);
                command.Execute();
                List<Entity> listFlight = command.GetResult();

                return listFlight;
            }catch(Exception ex ){

                return BadRequest(ex.Message);
            }
           
        }


//
        /// <summary> GET api/reservation-flight/departure/arrival/departuredate/arrivaldate/numpas</summary>
        /// <param name="departure">Ciudad de salida del vuelor</param>
        /// <param name="arrival">Ciudad de llegada del vuelo</param>
        /// <param name="departuredate">Fecha de salida del vuelo</param>
        /// <param name="arrivaldate">Fecha de Regreso del vuelo</param>
        /// <param name="numpas">Cantidad de Pasajeros que se quiere reservar </param>
        /// <returns>Devuelve todas las reservas en una fecha indicada</returns>

        [Route("~/api/reservation-flight/{departure}/{arrival}/{departuredate}/{arrivaldate}/{numpas}")] 
        [HttpGet("{departure}/{arrival}/{departuredate}/{arrivaldate}/{numpas}")]
        public ActionResult<IEnumerable<Entity>> Get(int departure, int arrival, string departuredate,string arrivaldate,int numpas)
        {
            try
            {
                GetReservationsByDateIVCommand command = 
                    CommandFactory.CreateGetReservationsByDateIVCommand(departure, arrival, departuredate, arrivaldate, numpas);
                command.Execute();
                List<Entity> listFlight = command.GetResult();

                return listFlight;
            }
            catch(Exception ex ){

                return BadRequest(ex.Message);
            }
        }
        
    }

}
