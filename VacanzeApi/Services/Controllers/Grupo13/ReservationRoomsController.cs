using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo13;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;
using Command = vacanze_back.VacanzeApi.Common.Command;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo13
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class ReservationRoomsController : ControllerBase
    {

        // GET api/reservationautomobiles/?id={user_id}]
        /* https://localhost:5001/api/reservationrooms/?user=1 */
        [HttpGet]
        public ActionResult<IEnumerable<IReservationRoomDAO>> GetAllByUserID([FromQuery] int user)
        {
            try
            {
                DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                IReservationRoomDAO reservationRoomDao = factory.GetReservationRoomDAO();
                return Ok(reservationRoomDao.GetAllByUserId(user));
            }
            catch (GeneralException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error en el servidor");
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
                return StatusCode(500, "Error en el servidor");
            }
        }

        // POST api/values //CREAR UN RECURSO
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ReservationRoom> Post([FromBody] ReservationRoom reservation)
        {
            try
            {
                CommandResult<ReservationRoom> command = CommandFactory.CreateAddReservationRoomCommand(reservation);
                command.Execute();
                return Ok(command.GetResult());
            }
            catch (GeneralException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error en el servidor");
            }
        }

        // PUT api/values/5    //ACTUALIZAR UN RECURSO
        [HttpPut]
        public ActionResult<Entity> Put([FromBody] ReservationRoom res)
        {
            try
            {
                ReservationRoomRepository repository = new ReservationRoomRepository();
              //  ReservationRoom reservation = (ReservationRoom)repository.Find((int)entity.Id);

                repository.Update(res);

                return Ok(new { Message = "Editado" });
            }
            /*    catch (DbErrorException ex)
                {
                    return BadRequest(new { Message = ex.Message });
                }*/
            catch (GeneralException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error en el servidor");
            }
        }

        // DELETE api/values/5 //BORRAR
        [HttpDelete("{id}")]
        public ActionResult<Entity> Delete(int id)
        {
            try
            {
                CommandResult<int> command = CommandFactory.CreateDeleteReservationRoomCommand(id);
                command.Execute();
                return Ok(command.GetResult());
            }
            catch (GeneralException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error en el servidor");
            }
        }
    }
}

