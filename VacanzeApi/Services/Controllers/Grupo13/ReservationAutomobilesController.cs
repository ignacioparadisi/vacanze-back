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
using Npgsql;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo13
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class ReservationAutomobilesController : ControllerBase
    {
        // GET api/values

        // GET api/reservationautomobiles/?id={user_id}]
        /* https://localhost:5001/api/reservationautomobiles/?user=1 */
        [HttpGet]
        public ActionResult<IEnumerable<Entity>> GetAllByUserID([FromQuery] int user = -1)
        {
            try
            {
                ReservationAutomobileRepository connection = new ReservationAutomobileRepository();
                return user == -1
                    ? connection.GetAutomobileReservations()
                    : connection.GetAllByUserId(user);
            }
            catch (SystemException)
            {
                throw;
            }
        }


        /*[HttpGet("{id}")]
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
        }*/

        // POST api/values //CREAR UN RECURSO
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Entity> Post([FromBody] ReservationVehicle reservation)
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
        [HttpPut]
        public ActionResult<Entity> Put([FromBody] ReservationVehicle res)
        {
            try
            {
                ReservationAutomobileRepository repository = new ReservationAutomobileRepository();
              //  ReservationAutomobile reservation = (ReservationAutomobile) repository.Find((int)res.Id);

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

    /*[HttpDelete("{id}")]
    public ActionResult<string> Delete(int id)
    {
        try
        {
            var connection = new ReservationAutomobileRepository();
               ReservationVehicle reservation = (ReservationVehicle)connection.Find(id);
                connection.Delete(reservation);
            return Ok("Eliminado exitosamente");
        }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }

        }*/

        

    }

 

}
