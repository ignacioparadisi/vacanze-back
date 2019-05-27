using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
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
                ReservationRoomConnection reservationRoomConnection = new ReservationRoomConnection();
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
                ReservationRoomConnection reservationRoomConnection = new ReservationRoomConnection();
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
        public ActionResult<Entity> Post([FromBody] Entity entity)
        {
            return entity;
        }

        // PUT api/values/5    //ACTUALIZAR UN RECURSO
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5 //BORRAR
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
