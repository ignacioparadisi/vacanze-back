using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities.Grupo4;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo4;
using vacanze_back.VacanzeApi.LogicLayer.Command;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo4
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class SaleFlightController : ControllerBase
    {
  
        [HttpGet("")]
        public ActionResult<List<SaleFlight>> Get(int origin, int destination, DateTime dateArrival, DateTime dateDeparute)
        {
            var getByIdCommand = CommandFactory.GetSaleFlightCommand(origin,destination,dateArrival,dateDeparute);
            try
            {
                getByIdCommand.Execute();
                return getByIdCommand.GetResult();
            }
            catch (FlightNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (DatabaseException ex)
            {
                // TODO: Log
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("")]
        public ActionResult<int> Post([FromBody] List<PostSaleFlight> postSaleFlight)
        {
            var getByIdCommand = CommandFactory.PostSaleFlightCommand(postSaleFlight);
            try
            {
                getByIdCommand.Execute();
                return getByIdCommand.GetResult();
            }
            catch (ErrorInSaleFlightException)
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
