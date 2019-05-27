using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using vacanze_back.VacanzeApi.Persistence.Connection.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo13
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class ReservationAutomobilesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Entity>> Get()
        {
            try
            {
                ReservationAutomobileConnection reservationAutomobileConnection = new ReservationAutomobileConnection();
                List<Entity> result = reservationAutomobileConnection.GetAutomobileReservations();
                return Ok(result.ToList());
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        
        /*
        [HttpGet("{id}")]
        public ActionResult<Entity> Get(int id)
        {
            try
            {
                ReservationAutomobileConnection reservationAutomobileConnection = new ReservationAutomobileConnection();
                Entity result = reservationAutomobileConnection.Find(id);
                return Ok(result);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        */
    }

}
