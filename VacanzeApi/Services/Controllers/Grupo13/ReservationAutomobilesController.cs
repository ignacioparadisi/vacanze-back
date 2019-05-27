using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo13
{
    [Produces("application/json")]
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

        // POST api/values //CREAR UN RECURSO
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5    //ACTUALIZAR UN RECURSO
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /*
    [HttpDelete("{id}")]
    public ActionResult<string> Delete(int id)
    {
        try
        {
            ReservationAutomobileConnection connection = new ReservationAutomobileConnection();
            connection.DeleteClaim(id);
            return Ok("Eliminado exitosamente");
        }
        catch (System.Exception)
        {
            throw;
        }

    }
    */
    }

}
