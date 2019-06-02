using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using vacanze_back.VacanzeApi.Common.Entities.Grupo14;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo14;
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
			catch(AvailabilityException e){
				ErrorMessage errorMessage = new ErrorMessage(e.Message);
                return BadRequest(errorMessage);
			}

		}

		[HttpGet("{userid}")]
		public ActionResult<IEnumerable<Restaurant_res>> Get(int userid){
			try{
				Console.WriteLine(userid);
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
		[HttpDelete("{resRestId}")]
		public ActionResult<string> Delete(int resRestId){

			Console.WriteLine(resRestId);
			ResRestaurantRepository conec= new ResRestaurantRepository();
			var deletedid = conec.deleteResRestaurant(resRestId);

			if(deletedid.Equals(-1)){
				ResponseError mensaje = new ResponseError();
				mensaje.error="No existe el elemento que quiere Eliminar";
				return StatusCode(500,mensaje);
			}
			Console.WriteLine(deletedid);
			return StatusCode(200, "Eliminado satisfactoriamente");

		}

		//api/Clain/status/5
		[HttpPut("{resRestId}")]
		public ActionResult<string> Put(int resRestId, reservationRestaurant resAux)
		{
			try{
				ResRestaurantRepository conec = new ResRestaurantRepository();
				Restaurant_res reserva = new Restaurant_res(resAux.pay_id);

				Console.WriteLine(resAux.pay_id);

				if (resAux.pay_id == 0){
					ResponseError mensaje= new ResponseError();
					mensaje.error="Can't modify a null value.";
					return StatusCode(500,mensaje);
				}						
				else{
					int deletedid = conec.updateResRestaurant(resAux.pay_id, resRestId);
					if (deletedid == -1){
						ResponseError mensaje= new ResponseError();
						mensaje.error="Can't modify your reservation.";
						return StatusCode(500,mensaje);
					}
					return Ok("Your payment have been made succesfully.");
				}
				
			}catch (DatabaseException )
			{            
				ResponseError mensaje= new ResponseError();
				mensaje.error="DataBase error.";
				return StatusCode(500, mensaje);
			}
			catch (InvalidStoredProcedureSignatureException e)
			{
				ResponseError mensaje= new ResponseError();
				mensaje.error="Inside error at modify.";
				return StatusCode(500);
			}catch (NullClaimException )
			{
				ResponseError mensaje= new ResponseError();
				mensaje.error="Your reservation doesn't exits.";
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

		public int pay_id {get; set;}
	}
}