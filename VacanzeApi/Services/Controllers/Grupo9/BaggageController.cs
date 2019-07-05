using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo9;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo9
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class BaggageController : ControllerBase
    {
        /// <summary>
        // GET api/values/id
        // consultar los equipajes segun su id
        /// </summary>
        [HttpGet("serial/{id}")]
        public ActionResult<Baggage> Get(int id)
        {
            var getByIdCommand = CommandFactory.CreateGetBaggageByIdCommand(id);
            try
            {
                getByIdCommand.Execute();
                return getByIdCommand.GetResult();
            }
            catch (BaggageNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (DatabaseException ex)
            {
                // TODO: Log
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        // GET api/values/5
        // Get para la tabla equipaje segun su documento de identidad
        /// </summary>
        [HttpGet("documentPasaport/{id}")]
        public ActionResult<IEnumerable<Baggage>> GetDocument(string id)
        {
            var getByPassportCommand = CommandFactory.CreateGetBaggageByPassportCommand(id);
            try
            {
                getByPassportCommand.Execute();
                return getByPassportCommand.GetResult();
            }
            catch (BaggageNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (DatabaseException ex)
            {
                // TODO: Log
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        //consultar los equipajes segun su estatus
        /// </summary>
        [HttpGet("admin/getStatus/{status}")]
        public ActionResult<IEnumerable<Baggage>> GetStatus(string status)
        {
            var getByStatusCommand = CommandFactory.CreateGetBaggageByStatusCommand(status);
            try
            {
                getByStatusCommand.Execute();
                return getByStatusCommand.GetResult();
            }
            catch (BaggageNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (DatabaseException ex)
            {
                // TODO: Log
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        //api/Clain/status/5
        // modificar el estatus del equipaje
        /// </summary>
        [HttpPut("{id}")]
        public ActionResult<Baggage> Put(int id, [FromBody] Baggage Baggage)
        {
            var updateBaggageCommand = CommandFactory.CreateUpdateBaggageCommand(id, Baggage);
            try
            {
                updateBaggageCommand.Execute();
                return updateBaggageCommand.GetResult();
            }
            catch (DatabaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (BaggageNotFoundException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (AttributeValueException ex)
            {
                return new BadRequestObjectResult(new ErrorMessage(ex.Message));
            }
        }
    }
}