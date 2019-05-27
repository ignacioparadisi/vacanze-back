using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.Connection;

namespace vacanze_back.VacanzeApi.Services.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class LocationsControllers : ControllerBase
    {
        // GET api/location/
        [HttpGet]
        public ActionResult<IEnumerable<Location>> Get()
        {
            var dbConnection = new LocationConnection();
            try
            {
                return dbConnection.GetLocations();
            }
            catch (DatabaseException e)
            {
                // TODO: No se deberia mandar un Ok cuando falla la base de datos, se deberia mandar
                //       un error 500 o algo
                return Ok(e.Message);
            }
        }

    }
}