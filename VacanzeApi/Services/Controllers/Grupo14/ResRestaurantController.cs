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
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo14;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo14;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo14;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo13;
using Microsoft.Extensions.Logging;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo14
{
	[Produces("application/json")] 
	[Route("api/[controller]")]
	[EnableCors("MyPolicy")]
	[ApiController]
	public class ResRestaurantController : ControllerBase
	{
        private readonly ILogger<ResRestaurantController> _logger;
        private string mensaje;
        public ResRestaurantController(ILogger<ResRestaurantController> logger)
        {
            _logger = logger;
        }

        //POST api/ResRestaurant
        [HttpPost]
		public ActionResult<int> Post([FromBody] reservationRestaurant resAux)
		{
            Restaurant_res reserva = new Restaurant_res(resAux.fecha_res, resAux.cant_people, resAux.date, resAux.user_id, resAux.rest_id);
            try
            {
                
                AddResRestaurantCommand command = CommandFactory.AddResRestaurantCommand(reserva);
                _logger.LogInformation("Se ejecuta el llamado al comando correspondiente para la agregacion de reservas de restaurante" +
                    " para el usuario con el id " +Convert.ToString(reserva.user_id));
                command.Execute();
                _logger.LogInformation("Se ejecuta el comando con las acciones a ejecutar correspondientes para la agregacion de reservas de restaurante" +
                    " para el usuario con el id " + Convert.ToString(reserva.user_id));
                //var id = ResRestaurantRepository.addReservation(reserva);
                //Console.WriteLine(id);
                return Ok();
            }
            catch (DatabaseException)
			{
                _logger.LogError("Se presento un excepción de comunicacion con la base de datos al querer ingresar la reservacion en el restaurante " + Convert.ToString(reserva.user_id));
                mensaje = "Error presentado en la base de datos";
				return StatusCode(500, mensaje);
			}
			catch (InvalidStoredProcedureSignatureException e)
			{
                _logger.LogError(e, "Se presento un excepción de en el proceso de almacenamiento");
                ErrorMessage errorMessage = new ErrorMessage(e.Message);               
				return StatusCode(500, errorMessage);
			}
			catch(AvailabilityException e){
                _logger.LogError(e, "Se presento un excepción de en la que no se encuentra habilitada la realiacion de reserva");
                ErrorMessage errorMessage = new ErrorMessage(e.Message);
                return BadRequest(errorMessage);
			}

		}

		//GET /ResRestaurant/{id}
		[HttpGet("{id}")]
		public ActionResult<IEnumerable<ResRestaurantDTO>> Get(int id){
            
            try {
                var getByIdCommand = CommandFactory.GetResRestaurantByIdCommand(id);
                Console.WriteLine(id);
                //return ResRestaurantRepository.getResRestaurant(id);
                getByIdCommand.Execute();
                _logger.LogInformation("Se ejecuta el comando con las acciones a ejecutar correspondientes para la obtencion de reservas de restaurante" +
                    " para el usuario con el id " + Convert.ToString(id));
                //return getByIdCommand.GetResult();
                ResRestaurantMapper resRestMapper = MapperFactory.createResRestaurantMapper();
                return resRestMapper.CreateDTOList(getByIdCommand.GetResult());
            }
            catch (DatabaseException ){
                _logger.LogError("Se presento un excepción de comunicacion con la base de datos al querer ingresar la reservacion en el restaurante " + Convert.ToString(id));
                mensaje = "Error presentado en la base de datos";
                return StatusCode(500, mensaje);
            }
			catch (InvalidStoredProcedureSignatureException e){
                _logger.LogError(e, "Se presento un excepción de en el proceso de almacenamiento");
                ErrorMessage errorMessage = new ErrorMessage(e.Message);
                return StatusCode(500, errorMessage);
            }
		}

		//GET /ResRestaurant/Payment/{id} el ID es el del usuario
        //Metodo de entrada para obtener la lista de las reservaciones que todavian no se han pagado
		[HttpGet("Payment/{userId}")]
		public ActionResult<IEnumerable<ResRestaurantDTO>> GetReservationNotPay(int userId){ //this method is not used to this preview

            
            try {
                var getResNotPayByIdCommand = CommandFactory.GetResRestaurantNotPayByIdCommand(userId);
                Console.WriteLine(userId);            
                getResNotPayByIdCommand.Execute();
                _logger.LogInformation("Se ejecuta el comando con las acciones a ejecutar correspondientes para la agregacion de reservas de restaurante" +
                    " para el usuario con el id " + Convert.ToString(userId));

                ResRestaurantMapper resRestMapper = MapperFactory.createResRestaurantMapper();
                return resRestMapper.CreateDTOList(getResNotPayByIdCommand.GetResult());
            }
            catch (DatabaseException){
                _logger.LogError("Se presento un excepción de comunicacion con la base de datos al querer ingresar la reservacion en el restaurante " + Convert.ToString(userId));
                mensaje = "Error presentado en la base de datos";
                return StatusCode(500, mensaje);
			}
			catch (InvalidStoredProcedureSignatureException e){
                _logger.LogError(e, "Se presento un excepción de en el proceso de almacenamiento");
                ErrorMessage errorMessage = new ErrorMessage(e.Message);
                return StatusCode(500, errorMessage);
            }
		}

		//DELETE api/ResRestaurant/id el ID es el de la reserva
		[HttpDelete("{id}")]
		public ActionResult<string> Delete(int id){ //PATRONES LISTO
            try
            {
                DeleteResRestaurantCommand command = CommandFactory.DeleteResRestaurantCommand(id);
                command.Execute();
                _logger.LogInformation("Se ejecuta el comando con las acciones a ejecutar correspondientes para la eliminacion de reservas de restaurante" +
                    " para el usuario con el id de reservacion " + Convert.ToString(id));
                return Ok();
            }
            catch (DatabaseException)
            {
                _logger.LogError("Se presento un excepción de comunicacion con la base de datos al querer ingresar la reservacion en el restaurante " + Convert.ToString(id));
                mensaje = "Error presentado en la base de datos";
                return StatusCode(500, mensaje);
            }
        }

		//PUT api/ResRestaurant/id es el id de la reserva
		[HttpPut("{id}")]
		public ActionResult<string> Put(int id, reservationRestaurant resAux)
		{
            var updateResRestaurantCommand = CommandFactory.UpdateResRestaurantCommand(id, resAux);
            try
            {
                updateResRestaurantCommand.Execute();
                return updateResRestaurantCommand.GetResult();

            }
            catch (DatabaseException )
			{            
				ResponseError mensaje= new ResponseError();
				mensaje.error="DataBase error.";
				return StatusCode(500, mensaje);
			}
			catch (InvalidStoredProcedureSignatureException e)
			{
				ResponseError mensaje= new ResponseError();
				mensaje.error="Error interno.";
				return StatusCode(500);
			}
		}


	}

    //Clase para respuestas de error.
    public class ResponseError {
	    public string error{get; set;}
	
	}

    //Clase auxiliar para poder realizar la reservacion en restaurantes.
	public class reservationRestaurant {
		public string fecha_res { get; set; }
       
        public int cant_people{get; set;}

        public string date { get; set;}

        public int user_id { get; set;}

        public int rest_id { get; set;}

		public int pay_id {get; set;}
	}
}