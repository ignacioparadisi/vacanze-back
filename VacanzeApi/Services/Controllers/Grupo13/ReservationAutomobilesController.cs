using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Persistence.Connection.Grupo13;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo13
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationAutomobilesController : ControllerBase
    {
        /*
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Entity>> Get()
        {
            try
            {
                DAOReservationAutomobile daoReservationAutomobiles = new DAOReservationAutomobile();
                List<Entity> result = daoReservationAutomobiles.getRoomReservations();
                return Ok(result.ToList());
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        */

        /*
        [HttpGet("{id}")]
        public ActionResult<Entity> Get(int id)
        {
            try
            {
                DAOReservationAutomobile daoReservationAutomobiles = new DAOReservationAutomobile();
                Entity result = daoReservationAutomobiles.Find(id);
                return Ok(result);
            }
            catch (System.Exception)
            {

                throw;
            }
        }*/
    }

}
