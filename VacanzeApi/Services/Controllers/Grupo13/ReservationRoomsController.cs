using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo13;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo13
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class ReservationRoomsController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Entity>> Get()
        {
            try
            {
                ReservationRoomRepository reservationRoomConnection = new ReservationRoomRepository();
                List<Entity> result = reservationRoomConnection.GetRoomReservations();
                return Ok(result.ToList());
            }
            catch (System.Exception)
            {
                return null;
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
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
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
    }
}
