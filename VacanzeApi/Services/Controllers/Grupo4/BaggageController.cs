using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo4;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo4;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo4;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo4
{
     [Produces("application/json")]
     [Route("api/[controller]")]
     [EnableCors("MyPolicy")]
     [ApiController]
     public class BaggageController : ControllerBase
     {

        [HttpPost("")]
        public ActionResult<int> Post([FromBody] List<CheckinBaggage> checkBag)
        {
            var getByIdCommand = CommandFactory.PostCheckBaggageCommand(checkBag);
            try
            {
                getByIdCommand.Execute();
                return getByIdCommand.GetResult();
            }
            catch (ErrorCheckBaggageException)
            {
                return new NotFoundResult();
            }
            catch (DatabaseException ex)
            {
                // TODO: Log
                return StatusCode(500, ex.Message);
            }
        }


     }

}