using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo8;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo8
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class CruiserController : Controller
    {
        // GET/Cruisers
        [HttpGet]
        public ActionResult<IEnumerable<Cruiser>> GetCruisers()
        {
            try
            {
                var CruiserList = CruiserConnection.GetCruisers();
                return Ok(CruiserList);
            }
//            catch (IndexOutOfRangeException)
//            {
//                return BadRequest("No hay cruceros disponibles")
//            }
            catch (DatabaseException)
            {
                return BadRequest("Error obteniendo los cruceros");
            }
        }
        // GET/Cruiser/{id}
        [HttpGet("{id}")]
        public IActionResult GetCruiser(int id)
        {
            try
            {
                 Cruiser cruiser=  CruiserConnection.GetCruiser(id);
                 return Ok(cruiser);
            }
            catch (IndexOutOfRangeException)
            {
                return BadRequest("Error obteniendo el crucero");
            }
        }


        [HttpPost]
        public ActionResult<Cruiser> PostCruiser([FromBody] Cruiser cruiser)
        {
            var id = CruiserConnection.AddCruiser(cruiser);
            if (id.Equals(-1))
            {
                return BadRequest("Faltan campos en el crucero");
            }
            var savedCruiser = new Cruiser(id,cruiser.Name,cruiser.Status,cruiser.Capacity,cruiser.LoadingShipCap,cruiser.Model,cruiser.Line);
            return StatusCode(200,savedCruiser);
        }
        
        
        [HttpPut]
         public ActionResult<Cruiser> PutCruiser([FromBody] Cruiser cruiser)
         {
             var UpdatedCruiser = CruiserConnection.UpdateCruiser(cruiser);
             if (UpdatedCruiser.Id.Equals(-1))
             {
                 return BadRequest("Error actualizando crucero");
             }
             return StatusCode(200, cruiser);
         }

        [HttpDelete("{id}")]
        public ActionResult<int> DeleteCruiser(int id)
        {
            var DeletedId = CruiserConnection.DeleteCruiser(id);
            if (DeletedId.Equals(-1))
            {
                return BadRequest("Error eliminando el crucero");
            }
            return StatusCode(200, "Eliminado satisfactoriamente");
        }
    }
}