using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.Entities.Grupo3;
using vacanze_back.Entities;
using vacanze_back.Connection.Grupo3;
using vacanze_back.Exceptions.Grupo3;


namespace vacanze_back.Controllers.Grupo3
{
    [Route("api/[controller]")]
    [ApiController]
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
                return BadRequest(ex.Message);
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
                return BadRequest(ex.Message);
            }
            catch (System.Exception)
            {
                
                return null;
            }
        }
    }
}