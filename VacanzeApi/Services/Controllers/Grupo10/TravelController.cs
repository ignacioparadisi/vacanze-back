using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo10;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo10;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo10;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo10;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo10
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class TravelController: ControllerBase
    {
            [Consumes("application/json")]
            [HttpPost]
            public IActionResult AddTravel([FromBody] Travel travel){
                /*esta funcion se encarga de llamar al metodo addtravel de la clase DAO factory*/
            try{

                DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                ITravel traveldao = factory.GetTravelDAO();
                int id = traveldao.addtravel(travel);
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
        [HttpPut]
        public IActionResult UpdateTravel([FromBody] Travel travel){
            /*esta funcion se encarga de llamar al metodo updatetravel de la clase DAO factory */
            try{
                 DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                ITravel traveldao = factory.GetTravelDAO();

                if(traveldao.Updatetravel(travel))
                    return Ok("Las modificaciones fueron realizadas exitosamente");
                else
                    return StatusCode(400);
            }catch(InternalServerErrorException ex){
                return StatusCode(500, ex.Message);
            }catch(Exception){
                return StatusCode(400);
            }
        }

        [HttpGet("gettravel/{userId}")]
        public ActionResult<IEnumerable<Travel>> GetTravels(int userId)
        {/*----------------------------------------------------------- */
            Console.WriteLine(userId);
            List<Travel> travels = new List<Travel>();
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            ITravel traveldao = factory.GetTravelDAO();
            try{
                
                travels = traveldao.Gettravel(userId);
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
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteTravel(int id)
        {/*/*esta funcion se encarga de llamar al metodo deletetravel de la clase DAO factory */ 
            Console.WriteLine(id);
             DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            ITravel traveldao = factory.GetTravelDAO();
            int i=0;
            try{
                i =  traveldao.Deletetravel(id);
                if (i==0){
                    return Ok ("no se elminno ");
                }
                return Ok("se elimino");
            }catch(WithoutExistenceOfTravelsException ex){
                return StatusCode(404,ex.Message);
            }catch(UserNotFoundException ){
                
                return Ok("se elimino");
            }catch(InternalServerErrorException ex){
                return StatusCode(500, ex.Message);
            }catch(Exception){
                return Ok("se elimino");
            }
        }
        
        [HttpGet("gettravellocation/{travelId}")]
        public ActionResult<IEnumerable<Location>> GetLocationsByTravel(int travelId)
        {/*esta funcion se encarga de llamar al metodo GetLocationsByTravel de la clase DAO factory */
            List<Location> locationsByTravel = new List<Location>();
             DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
             ITravel traveldao = factory.GetTravelDAO();
            try{
                locationsByTravel = traveldao.GetLocationsByTravel(travelId);
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
        [HttpPost("locationtravel/{travelId}")]
        public IActionResult AddLocationsToTravel(int travelId, [FromBody] List<Location> locations)
        {/*esta funcion se encarga de llamar al metodo AddLocationsToTravel de la clase DAO factory */
            Boolean saved = false;
            try{
                 DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                ITravel traveldao = factory.GetTravelDAO();
                saved = traveldao.AddLocationsToTravel(travelId, locations);
                if(saved){
                    return Ok("Las ciudades fueron agregadas satisfactoriamente");
                }
                return Ok(saved);
            }catch(InternalServerErrorException ex){
                return StatusCode(500, ex.Message);
            }catch(Exception){
                return StatusCode(400);
            }
        }
        
        [HttpPost("addreservationtravel/{travelId}/{type}")]
        public IActionResult AddReservationToTravel(int travelId,[FromQuery] int res,  string type)
        {/*esta funcion se encarga de llamar al metodo AddReservationToTravel de la clase DAO factory */
            try{
                DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                ITravel traveldao = factory.GetTravelDAO();
                if(traveldao.AddReservationToTravel(travelId, res, type)) 
                    return Ok("La reserva fue agregada satisfactoriamente");
                else
                    return StatusCode(400,"No se pudo añadir la reserva al viaje");
            }catch(InternalServerErrorException ex){
                return StatusCode(500, ex.Message);
            }catch(Exception){
                return StatusCode(400);
            }

        }


      /*   [HttpGet("reservation/{travelId}/{locationId}/{type}")]
        public ActionResult<IEnumerable<object>> GetReservationsByTravelAndLocation(
            int travelId,  int locationId,  string type){
            try{
               DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                ITravel traveldao = factory.GetTravelDAO();
                List<object> reservations = 
                        traveldao.GetReservationsByTravelAndLocation<object>(travelId, locationId, type);
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
        }*/

    }
}