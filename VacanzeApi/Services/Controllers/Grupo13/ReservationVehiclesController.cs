using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using Microsoft.AspNetCore.Http;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo13;
using Microsoft.Extensions.Logging;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo13
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class ReservationVehiclesController : ControllerBase
    {
        private readonly ILogger<ReservationVehiclesController> _logger;
        public ReservationVehiclesController(ILogger<ReservationVehiclesController> logger)
        {
            _logger = logger;
        }
        // GET api/values

        // GET api/reservationautomobiles/?id={user_id}]
        /* https://localhost:5001/api/reservationautomobiles/?user=1 */
        [HttpGet]
        public ActionResult<IEnumerable<ReservationVehicleDTO>> GetAllByUserID([FromQuery] int user)
        {
            try
            {
                CommandResult<List<ReservationVehicle>> command =
                    CommandFactory.CreateGetReservationVehicleByUserCommand(user);
                _logger?.LogInformation("Se ejecuta el Comando para Obtener las Reservaciones de Vehículo del" +
                                       "Usuario " + Convert.ToString(user));
                command.Execute();
                _logger?.LogInformation("Se Obtienen las Reservaciones de Vehículo del" +
                                       "Usuario " + Convert.ToString(user));
                var resvehicMapper = MapperFactory.CreateReservationVehicleMapper();
                return Ok(resvehicMapper.CreateDTOList(command.GetResult()));
            }
            catch (GeneralException e)
            {
                _logger?.LogWarning(e, e.Message + " al Obtener las Reservas de Vehículo de el Usuario " + 
                                      Convert.ToString(user));
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error al Obtener las Reservas de Vehículo de el Usuario " + 
                                    Convert.ToString(user));
                return StatusCode(500, "Error en el Servidor");
            }
        }


        [HttpGet("{id}")]
        public ActionResult<ReservationVehicleDTO> Get(int id)
        {
            try
            {
                CommandResult<ReservationVehicle> command = CommandFactory.CreateFindReservationVehicleCommand(id);
                _logger?.LogInformation("Se Ejecuta el Comando para Obtener la Reservación de Vehículo " +
                              Convert.ToString(id));
                command.Execute();
                _logger?.LogInformation("Se Obtiene la Reservación de Vehículo " +
                                       Convert.ToString(id));
                var resvehicMapper = MapperFactory.CreateReservationVehicleMapper();
                return Ok(resvehicMapper.CreateDTO(command.GetResult()));
            }
            catch (GeneralException e)
            {
                _logger?.LogWarning(e, e.Message + " al Obtener la Reservación de Vehículo " + 
                                      Convert.ToString(id));
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger?.LogError(e,  "Error en el Servidor al Obtener la Reservación de Vehículo " + 
                                      Convert.ToString(id));
                return StatusCode(500, "Error en el Servidor");
            }
        }

        // POST api/values //CREAR UN RECURSO
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ReservationVehicleDTO> Post([FromBody] ReservationVehicleDTO reservationDto)
        {
            try
            {
                var resroomMapper = MapperFactory.CreateReservationVehicleMapper();
                var reservation = resroomMapper.CreateEntity(reservationDto);
                CommandResult<ReservationVehicle> command =
                    CommandFactory.CreateAddReservationVehicleCommand(reservation);
                _logger?.LogInformation("Se Ejecuta el Comando para Agregar la Reserva de Vehículo");
                command.Execute();
                _logger?.LogInformation("Se Creó la Reserva de Vehículo");
                return Ok(resroomMapper.CreateDTO(command.GetResult()));
            }
            catch (GeneralException e)
            {
                _logger?.LogWarning(e, e.Message + " al Agregar Reserva de Vehículo");
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger?.LogError(e,"Error en el Servidor al Agregar Reserva de Habitación");
                return StatusCode(500, "Error en el Servidor");
            }
        }


        // PUT api/values/5    //ACTUALIZAR UN RECURSO
        [HttpPut]
        public ActionResult<Entity> Put([FromBody] ReservationVehicleDTO reservationDto)
        {
            try
            {
                var resroomMapper = MapperFactory.CreateReservationVehicleMapper();
                var reservation = resroomMapper.CreateEntity(reservationDto);
                CommandResult<ReservationVehicle> command = CommandFactory.CreateUpdateReservationVehicleCommand(reservation);
                _logger?.LogInformation("Se Ejecuta el Comando para Modificar la Reservar de Vehícuñp " +
                                        Convert.ToString(reservationDto.Id));
                command.Execute();
                _logger?.LogInformation("Se Actualizó la Reserva de Habitación " +
                                        Convert.ToString(reservationDto.Id));
                return Ok(resroomMapper.CreateDTO(command.GetResult()));
            }
            
            catch (GeneralException e)
            {
                _logger?.LogWarning(e, e.Message + "al Actualizar la Reserva de Vehículo "  +
                                       Convert.ToString(reservationDto.Id));
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger?.LogError(e,"Error en el Servidor al Actualizar la Reserva de Vehículo "  +
                                    Convert.ToString(reservationDto.Id));
                return StatusCode(500, "Error en el Servidor");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            try
            {
                CommandResult<int> command = CommandFactory.CreateDeleteReservationVehicleCommand(id);
                _logger?.LogInformation("Se Ejecuta el Comando para Eliminar la Reservación de Vehículo " +
                                       Convert.ToString(id));
                command.Execute();
                _logger?.LogInformation("Se Elimina la Reservación de Vehículo " +
                                       Convert.ToString(id));
                return Ok(command.GetResult());
            }
            catch (GeneralException e)
            {
                _logger?.LogWarning(e, e.Message + " al Eliminar la Reservación de Vehículo " + 
                                      Convert.ToString(id));
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger?.LogError(e,  "Error en el Servidor al Eliminar la Reservación de Vehículo " + 
                                     Convert.ToString(id));
                return StatusCode(500, "Error en el Servidor");
            }
        }
    }
}
