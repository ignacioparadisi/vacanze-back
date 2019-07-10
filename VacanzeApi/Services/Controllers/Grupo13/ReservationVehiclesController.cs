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
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.LogicLayer.Command;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo13
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class ReservationVehiclesController : ControllerBase
    {
        // GET api/values

        // GET api/reservationautomobiles/?id={user_id}]
        /* https://localhost:5001/api/reservationautomobiles/?user=1 */
        [HttpGet]
        public ActionResult<IEnumerable<Entity>> GetAllByUserID([FromQuery] int user = -1)
        {
            try
            {
                CommandResult<List<ReservationVehicle>> command =
                    CommandFactory.CreateGetReservationVehicleByUserCommand(user);
                command.Execute();
                return Ok(command.GetResult());
            }
            catch (GeneralException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error en el Servidor");
            }
        }


        [HttpGet("{id}")]
        public ActionResult<ReservationVehicle> Get(int id)
        {
            try
            {
                CommandResult<ReservationVehicle> command = CommandFactory.CreateFindReservationVehicleCommand(id);
                command.Execute();
                return Ok(command.GetResult());
            }
            catch (GeneralException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error en el Servidor");
            }
        }

        // POST api/values //CREAR UN RECURSO
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Entity> Post([FromBody] ReservationVehicle reservation)
        {
            try
            {
                CommandResult<ReservationVehicle> command =
                    CommandFactory.CreateAddReservationVehicleCommand(reservation);
                command.Execute();
                return Ok(command.GetResult());
            }
            catch (GeneralException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error en el Servidor");
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

                return Ok(new {Message = "Editado"});
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

        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            try
            {
                CommandResult<int> command = CommandFactory.CreateDeleteReservationVehicleCommand(id);
                command.Execute();
                return Ok(command.GetResult());
            }
            catch (GeneralException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error en el Servidor");
            }
        }
    }
}
