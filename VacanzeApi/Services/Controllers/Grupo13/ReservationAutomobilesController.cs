using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using Microsoft.AspNetCore.Http;

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
                ReservationAutomobileRepository reservationAutomobileConnection = new ReservationAutomobileRepository();
                List<Entity> result = reservationAutomobileConnection.GetAutomobileReservations();
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
                ReservationAutomobileRepository connection = new ReservationAutomobileRepository();
                Entity result = connection.Find(id);
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
        public ActionResult<Entity> Post([FromBody] ReservationAutomobile reservation)
        {
            try
            {
                var connection = new ReservationAutomobileRepository();
                connection.AddReservation(reservation);
                return Ok(new { Message = "Agregada Reservacion de automovil" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
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
