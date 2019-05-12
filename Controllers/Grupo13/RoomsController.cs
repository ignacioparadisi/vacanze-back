using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.Entities.Grupo13;
using vacanze_back.Entities;
using vacanze_back.Persistence.Grupo13;

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
                DAORoom daoRooms = new DAORoom();
                List<Entity> result = daoRooms.getHabitaciones();
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
                DAORoom dAOHabitacion = new DAORoom();
                Entity result = dAOHabitacion.findHabitacion(id);
                return Ok(result);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }

}
