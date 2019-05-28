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
        public ActionResult<IEnumerable<Cruiser>> GetCruisers(int id)
        {
            try
            {
                var CruiserList=  CruiserConnection.GetCruisers();
                return Ok(JsonConvert.SerializeObject(CruiserList));
            }
            catch (IndexOutOfRangeException)
            {
                return StatusCode(500,"No hay cruceros registrados");
            }
        }
        // GET/Cruiser/{id}
        [HttpGet("{id}")]
        public IActionResult GetCruiser(int id)
        {
            try
            {
                 Cruiser cruiser=  CruiserConnection.GetCruiser(id);
                 return Ok(JsonConvert.SerializeObject(cruiser));
            }
            catch (IndexOutOfRangeException)
            {
                return StatusCode(500,"El Crusero no fue encontrado");
            }
        }


        [HttpPost]
        public ActionResult<Cruiser> PostCruiser([FromBody] Cruiser cruiser)
        {
            Console.WriteLine(cruiser);
            var id = CruiserConnection.AddCruiser(cruiser);
            var savedCruiser = new Cruiser(id,cruiser.Name,cruiser.Status,cruiser.Capacity,cruiser.LoadingShipCap,cruiser.Model,cruiser.Line);
            return StatusCode(200,savedCruiser);
        }
        
//    
//        }
//        [HttpPut]
//        public Cruiser PutCruiser(Cruiser cruiser)
//        {
//         
////      }
/// 
        [HttpDelete("{id}")]
        public ActionResult<int> DeleteCruiser(int id)
        {
            var deletedid = CruiserConnection.DeleteCruiser(id);
            if (deletedid.Equals(-1))
            {
                return StatusCode(500, "El crucero no existe");
            }
            return StatusCode(200, "Eliminado satisfactoriamente");
        }
    }
}