using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo3;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo3;

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
                var aircon = new AirplanesConnection();
                var result = aircon.Get();
                return Ok(result.ToList());
            }
            catch (DbErrorException ex)
            {
                return BadRequest(new {ex.Message});
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Entity> Get(int id)
        {
            try
            {
                var aircon = new AirplanesConnection();
                var result = aircon.Find(id);
                return Ok(result);
            }
            catch (DbErrorException ex)
            {
                return BadRequest(new {ex.Message});
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}