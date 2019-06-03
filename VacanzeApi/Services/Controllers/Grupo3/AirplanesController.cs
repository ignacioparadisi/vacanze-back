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
        /// <summary>api/airplanes</summary>
        /// <returns>ActionResult con resultado del query</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Entity>> Get()
        {
            try
            {
                var result = AirplanesRepository.Get();
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

        /// <summary>/api/airplanes/id</summary>
        /// <param name="id">Id del avion a busca</param>
        /// <returns>ActionResult con el avion buscado o nulo si no encontro nada</returns>
        [HttpGet("{id}")]
        public ActionResult<Entity> Find(int id)
        {
            try
            {
                var result = AirplanesRepository.Find(id);
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