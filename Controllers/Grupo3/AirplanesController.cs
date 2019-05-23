using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.Entities.Grupo3;
using vacanze_back.Entities;
using vacanze_back.Persistence.Grupo3;


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
                DAOAirplanes adao = new DAOAirplanes(); 
                List<Entity> result = adao.Get();
                return Ok(result.ToList());
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Entity> Get(int id)
        {
            try
            {
                DAOAirplanes adao = new DAOAirplanes(); 
                Entity result = adao.Find(id);
                return Ok(result);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}