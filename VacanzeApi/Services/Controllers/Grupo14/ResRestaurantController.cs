using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo14;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo14;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo14
{
	[Produces("application/json")] 
	[Route("api/[controller]")]
	[EnableCors("MyPolicy")]
	[ApiController]
	public class ResRestaurantController : ControllerBase
	{
      
		[HttpPost]
		public ActionResult<string> Post([FromBody] reservationRestaurant resAux)
		{            
			try{		 
				ResRestaurantRepository conec= new ResRestaurantRepository();
				Restaurant_res reserva= new Restaurant_res(resAux.fecha_res, resAux.cant_people, resAux.date, resAux.user_id, resAux.rest_id);
				Console.WriteLine("Antes de llamar al repository");
                conec.addReservation(reserva);
				return Ok("Agregado correctamente");
			}catch (DatabaseException)
			{            
                Console.WriteLine("Estoy en el databaseException");
				return StatusCode(500);
			}
			catch (InvalidStoredProcedureSignatureException )
			{
                Console.WriteLine("Estoy en el InvalidStoredProcedureSignatureException");
				return StatusCode(500);
			}

		}

        [HttpGet("{id}")]
		public ActionResult<IEnumerable<Restaurant_res>> Get(int userid){
			try{
				ResRestaurantRepository conec= new ResRestaurantRepository();
				List<Restaurant_res> resRestaurantList = conec.getResRestaurant(userid);
                Console.WriteLine("Fuera del SP");
                Console.WriteLine(resRestaurantList.ToArray());
				return resRestaurantList; 
			}
            catch (DatabaseException ){            
				return StatusCode(500);
			}
			catch (InvalidStoredProcedureSignatureException ){
				return StatusCode(500);
			}
		}

		//api/ResRestaurant/id
		[HttpDelete("{id}")]
		public ActionResult<string> Delete(int resRestid)
		{
			try{
				ResRestaurantRepository conec= new ResRestaurantRepository();  
				int id_eliminado = conec.deleteResRestaurant(resRestid); 
				return Ok("Eliminado exitosamente");
			}catch (DatabaseException )
			{            
				return StatusCode(500);
			}
			catch (InvalidStoredProcedureSignatureException){

				return StatusCode(500);
			}
			catch (NullClaimException ){

				ResponseError mensaje = new ResponseError();
				mensaje.error="No existe el elemento que quiere Eliminar";
				return StatusCode(500,mensaje);
			}

		}
		

	}

    public class ResponseError {
	    public string error{get; set;}
	
	}
	public class reservationRestaurant {
		public string fecha_res { get; set; }
       
        public int cant_people{get; set;}

        public string date { get; set;}

        public int user_id { get; set;}

        public int rest_id { get; set;}
	}
}