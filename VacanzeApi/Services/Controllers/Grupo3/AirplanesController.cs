using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using vacanze_back.VacanzeApi.Common.Entities.Grupo3;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Persistence.Connection.Grupo3;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo3;


namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo3
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class AirplanesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Entity>> Get()
        {
            try
            {
                AirplanesConnection aircon = new AirplanesConnection(); 
                List<Entity> result = aircon.Get();
                return Ok(result.ToList());
            }
            catch (DbErrorException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
            catch (System.Exception)
            {
                
                return null;
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Entity> Get(int id)
        {
            try
            {
                AirplanesConnection aircon = new AirplanesConnection(); 
                Entity result = aircon.Find(id);
                return Ok(result);
            }
            catch (DbErrorException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
            catch (System.Exception)
            {
                
                return null;
            }
        }
    }
}