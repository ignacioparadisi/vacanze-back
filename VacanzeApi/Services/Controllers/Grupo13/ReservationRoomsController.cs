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
        public ActionResult<IEnumerable<Entity>> GetAllByUserID([FromQuery] int user = -1)
        {
            try
            {
                ReservationRoomRepository connection = new ReservationRoomRepository();
                return user == -1
                    ? connection.GetRoomReservations()
                    : connection.GetAllByUserId(user);
            }
            catch (SystemException)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Entity> Find(int id)
        {
            try
            {
                ReservationRoomRepository reservationRoomConnection = new ReservationRoomRepository();
                Entity result = reservationRoomConnection.Find(id);
                return Ok(result);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        // POST api/values //CREAR UN RECURSO
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Entity> Post([FromBody] ReservationRoom reservation)
        {
            try
            {
                ReservationRoomRepository repository = new ReservationRoomRepository();
                 repository.Add(reservation);
                return Ok(new { Message = "Reservacion Agregada" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        // DELETE api/values/5 //BORRAR
        [HttpDelete("{id}")]
        public ActionResult<Entity> Delete(int id)
        {
            try
            {
                ReservationRoomRepository repository = new ReservationRoomRepository();
                ReservationRoom reservation = (ReservationRoom) repository.Find(id);

                repository.Delete(reservation);

                return Ok(new { Message = "Reservacion eliminada" });
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        //PUT api/values/{id}
        [HttpPut("{resRooId}")]
        public ActionResult<string> Put(int resRestId, ReservationRoom res)
        {
            try
            {
                ReservationRoomRepository repository = new ReservationRoomRepository();
                ReservationRoom reservation = new ReservationRoom(res.Id);
                reservation = res;

                if (res.Fk_pay > 0) {
                    int id = repository.AddPayment(reservation, resRestId);
                    if (id == -1)
                    {
                        return null;
                    }
                    return Ok("Pago modificado");
                }
                return Ok("Pago Modificado");
            }
            catch (NpgsqlException e)
            {
                e.ToString();
                throw;
            }
            catch (Exception e)
            {
                e.ToString();
                throw;
            }
        }


    }
}

