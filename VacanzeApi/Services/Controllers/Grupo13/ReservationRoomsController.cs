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
        public ActionResult<IEnumerable<ReservationRoom>> GetReservationRoomsForUser([FromQuery] int user)
        {
            try
            {
                CommandResult<List<ReservationRoom>> command =
                    CommandFactory.CreateGetReservationRoomsForUserCommand(user);
                command.Execute();
                return Ok(command.GetResult());
            }
            catch (GeneralException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error en el Servidor");
            }
        }
        
        [HttpGet("{id}")]
        public ActionResult<Entity> Find(int id)
        {
            try
            {
                DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                IReservationRoomDAO reservationRoomDao = factory.GetReservationRoomDAO();
                ReservationRoom result = reservationRoomDao.Find(id);
                return Ok(result);
            }
            catch (GeneralException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error en el Servidor");
            }
        }

        /// <summary>
        /// POST api/reservationrooms
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns> El DTO de la Resrva de Hotel Agregada</returns>
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

        // PUT api/values/5    //ACTUALIZAR UN RECURSO
        [HttpPut]
        public ActionResult<ReservationRoomDTO> Put([FromBody] ReservationRoomDTO reservationDto)
        {
            try
            {
                var resroomMapper = MapperFactory.CreateReservationRoomMapper();
                var reservation = resroomMapper.CreateEntity(reservationDto);
                CommandResult<ReservationRoom> command = CommandFactory.CreateUpdateReservationRoomCommand(reservation);
                _logger.LogInformation("Se Ejecuta el Comando para Reservar Habitación");
                command.Execute();
                _logger.LogInformation("Se Actualizó la Reserva de Habitación");
                return Ok(resroomMapper.CreateDTO(command.GetResult()));
            }
            
            catch (GeneralException e)
            {
                _logger.LogError(e, e.Message + "al Actualizar Reserva de Hotel");
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Error en el Servidor al Actualizar Reserva de Hotel");
                return StatusCode(500, "Error en el Servidor");
            }
        }

        // DELETE api/values/5 //BORRAR
        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            try
            {
                CommandResult<int> command = CommandFactory.CreateDeleteReservationRoomCommand(id);
                _logger.LogInformation("Se Ejecuta el Comando para Eliminar Habitación");
                command.Execute();
                _logger.LogInformation("Se Eliminó la Reserva de Habitación");
                return Ok(command.GetResult());
            }
            catch (GeneralException e)
            {
                _logger.LogError(e, e.Message + "al Agregar Reserva de Hotel");
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Error en el Servidor al Eliminar una Reserva de Hotel");
                return StatusCode(500, "Error en el Servidor");
            }
        }
    }
}

