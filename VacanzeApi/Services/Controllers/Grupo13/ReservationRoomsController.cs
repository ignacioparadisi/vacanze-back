using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo13;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo13;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using Command = vacanze_back.VacanzeApi.Common.Command;
using Microsoft.Extensions.Logging;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo13
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class ReservationRoomsController : ControllerBase
    {
        private readonly ILogger<ReservationRoomsController> _logger;
        public ReservationRoomsController(ILogger<ReservationRoomsController> logger)
        {
            _logger = logger;
        }
        // GET api/reservationautomobiles/?id={user_id}]
        /* https://localhost:5001/api/reservationrooms/?user=1 */
        [HttpGet]
        public ActionResult<IEnumerable<ReservationRoomDTO>> GetReservationRoomsForUser([FromQuery] int user)
        {
            try
            {
                CommandResult<List<ReservationRoom>> command =
                    CommandFactory.CreateGetReservationRoomsForUserCommand(user);
                _logger?.LogInformation("Se Ejecuta el Comando para Obtener las Reservas de Habitación de un "+
                                       "Usuario");
                command.Execute();
                _logger?.LogInformation("Se Obtuvieron las Reservas de Habitación de un Usuario");
                var resroomMapper = MapperFactory.CreateReservationRoomMapper();
                return Ok(resroomMapper.CreateDTOList(command.GetResult()));
            }
            catch (GeneralException e)
            {
                _logger?.LogError(e, e.Message + "al Obtener las Reservas de Habitación de un Usuario");
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error en el Servidor al Obtener las Reservas de Habitación de un " +
                                    "Usuario" );
                return StatusCode(500, "Error en el Servidor");
            }
        }
        
        [HttpGet("{id}")]
        public ActionResult<ReservationRoomDTO> Find(int id)
        {
            try
            {
                CommandResult<ReservationRoom> command = CommandFactory.CreateGetReservationRoomCommand(id);
                _logger?.LogInformation("Se Ejecuta el Comando para Consultar una Reserva de Habitación");
                command.Execute();
                var resroomMapper = MapperFactory.CreateReservationRoomMapper();
                _logger?.LogInformation("Se Consultó la Reserva de Habitación");
                return Ok(resroomMapper.CreateDTO(command.GetResult()));
            }
            catch (GeneralException e)
            {
                _logger?.LogError(e, e.Message + "al Consultar una Reserva de Habitación");
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger?.LogError(e,"Error en el Servidor al Consultar una Reserva de Habitación");
                return StatusCode(500, "Error en el Servidor");
            }
        }

        /// <summary>
        /// POST api/reservationrooms
        /// </summary>
        /// <param name="reservation">El DTO de la Reserva a Agregar</param>
        /// <returns> El DTO de la Reserva de Habitación Agregada</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ReservationRoomDTO> Post([FromBody] ReservationRoomDTO reservationDto)
        {
            try
            {
                var resroomMapper = MapperFactory.CreateReservationRoomMapper();
                var reservation = resroomMapper.CreateEntity(reservationDto);
                CommandResult<ReservationRoom> command = CommandFactory.CreateAddReservationRoomCommand(reservation);
                _logger?.LogInformation("Se Ejecuta el Comando para Agregar Habitación");
                command.Execute();
                _logger?.LogInformation("Se Creó la Reserva de Habitación");
                return Ok(resroomMapper.CreateDTO(command.GetResult()));
            }
            catch (GeneralException e)
            {
                _logger?.LogError(e, e.Message + "al Agregar Reserva de Hotel");
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger?.LogError(e,"Error en el Servidor al Agregar Reserva de Hotel");
                return StatusCode(500, "Error en el Servidor");
            }
        }

        /// <summary>
        /// PUT api/reservationrooms
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns> El DTO de la Reserva de Habitación Modificada</returns>
        [HttpPut]
        public ActionResult<ReservationRoomDTO> Put([FromBody] ReservationRoomDTO reservationDto)
        {
            try
            {
                var resroomMapper = MapperFactory.CreateReservationRoomMapper();
                var reservation = resroomMapper.CreateEntity(reservationDto);
                CommandResult<ReservationRoom> command = CommandFactory.CreateUpdateReservationRoomCommand(reservation);
                _logger?.LogInformation("Se Ejecuta el Comando para Reservar Habitación");
                command.Execute();
                _logger?.LogInformation("Se Actualizó la Reserva de Habitación");
                return Ok(resroomMapper.CreateDTO(command.GetResult()));
            }
            
            catch (GeneralException e)
            {
                _logger?.LogError(e, e.Message + "al Actualizar Reserva de Habitación");
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger?.LogError(e,"Error en el Servidor al Actualizar Reserva de Habitación");
                return StatusCode(500, "Error en el Servidor");
            }
        }

        /// <summary>
        /// DELETE api/reservationrooms
        /// </summary>
        /// <param name="id"> Id de la Reserva de Habitación a Eliminar</param>
        /// <returns>El Id de la Reserva de Habitación luego de Eliminarla en Base de Datos</returns>
        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            try
            {
                CommandResult<int> command = CommandFactory.CreateDeleteReservationRoomCommand(id);
                _logger?.LogInformation("Se Ejecuta el Comando para Eliminar una Reserva de Habitación");
                command.Execute();
                _logger?.LogInformation("Se Eliminó la Reserva de Habitación");
                return Ok(command.GetResult());
            }
            catch (GeneralException e)
            {
                _logger?.LogError(e, e.Message + "al Agregar Reserva de Hotel");
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger?.LogError(e,"Error en el Servidor al Eliminar una Reserva de Habitación");
                return StatusCode(500, "Error en el Servidor");
            }
        }
    }
}

