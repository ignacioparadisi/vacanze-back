using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo9;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo9
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class BaggageController : ControllerBase
    {

        /// <summary>
        ///     GET api/baggage/serial/{id}
        ///     Metodo para obtener el equipaje segun su serial
        /// </summary>
        /// <returns>Objeto tipo json con el equipaje deseado</returns>
        [HttpGet("serial/{id}")]
        public ActionResult<BaggageDTO> Get(int id)
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
        ///     GET api/baggage/documentPasaport/{id}
        ///     Metodo para obtener todos los equipajes de un usuario en particular por medio de su pasaporte
        /// </summary>
        /// <returns>Lista de Objetos de tipo json de cada equipaje</returns>
        [HttpGet("documentPasaport/{id}")]
        public ActionResult<IEnumerable<BaggageDTO>> GetDocument(string id)
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
        ///     GET api/baggage/admin/getStatus/{id}
        ///     Metodo para obtener todos los equipajes segun un estatus determinado
        ///     Estatus posibles: EXTRAVIADO, RECLAMADO, ENTREGADO
        /// </summary>
        /// <returns>Lista de Objetos de tipo json de cada equipaje</returns>
        [HttpGet("admin/getStatus/{status}")]
        public ActionResult<IEnumerable<BaggageDTO>> GetStatus(string status)
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
        ///     PUT api/baggage/{id}
        ///     Metodo para modificar el equipaje
        /// </summary>
        /// <returns>id del equipaje modificado</returns>
        [HttpPut("{id}")]
        public ActionResult<BaggageDTO> Put(int id, [FromBody] BaggageDTO Baggage)
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