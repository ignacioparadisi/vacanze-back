using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo13;

namespace vacanze_back.Controllers.Grupo13
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Entity>> Get()
        {
            try
            {
                RoomConnection roomConnection = new RoomConnection();
                List<Entity> result = roomConnection.GetRooms();
                return Ok(result.ToList());
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Entity> Get(int id)
        {
            try
            {
                RoomConnection roomConnection = new RoomConnection();
                Entity result = roomConnection.FindRoom(id);
                return Ok(result);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public Room Post([FromBody] Room room)
        {
            try
            {
                ;
                RoomConnection roomConnection = new RoomConnection();
               // dAORoom.Add(room);
                return room;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }

}
