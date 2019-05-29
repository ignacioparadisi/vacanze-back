using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;
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
                var CruiserList = CruiserRepository.GetCruisers();
                return Ok(CruiserList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // GET/Cruiser/{id}
        [HttpGet("{id}")]
        public IActionResult GetCruiser(int id)
        {
            try
            {
                Cruiser cruiser=  CruiserRepository.GetCruiser(id);
             
                 return Ok(cruiser);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        public ActionResult<Cruiser> PostCruiser([FromBody] Cruiser cruiser)
        {
            try
            {
                cruiser.Validate();
                var id = CruiserRepository.AddCruiser(cruiser);
                var savedCruiser = new Cruiser(id,cruiser.Name,cruiser.Status,cruiser.Capacity,cruiser.LoadingShipCap,cruiser.Model,cruiser.Line,cruiser.Picture);
                return StatusCode(200,savedCruiser);
            }
            catch (NotValidAttributeException e)
            {
               return BadRequest(e.Message);
            }
        }
        
        
        [HttpPut]
         public ActionResult<Cruiser> PutCruiser([FromBody] Cruiser cruiser)
         {
             var UpdatedCruiser = CruiserRepository.UpdateCruiser(cruiser);
             if (UpdatedCruiser.Id.Equals(-1))
             {
                 return BadRequest("Error actualizando crucero");
             }
             return StatusCode(200, cruiser);
         }

        [HttpDelete("{id}")]
        public ActionResult<int> DeleteCruiser(int id)
        {
            var DeletedId = CruiserRepository.DeleteCruiser(id);
            if (DeletedId.Equals(-1))
            {
                return BadRequest("Error eliminando el crucero");
            }
            return StatusCode(200, "Eliminado satisfactoriamente");
        }
    }
}