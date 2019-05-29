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
                var cruiserList = CruiserRepository.GetCruisers();
                return Ok(cruiserList);
            }
            catch (CruiserNotFoundException e)
            {
                ErrorMessage errorMessage = new ErrorMessage(400,e.Message);
                return BadRequest(errorMessage);
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
            catch (CruiserNotFoundException e)
            {
                ErrorMessage errorMessage = new ErrorMessage(400,e.Message);
                return BadRequest(errorMessage);
            }
        }


        [HttpPost]
        public ActionResult<Cruiser> PostCruiser([FromBody] Cruiser cruiser)
        {
            try
            {
                cruiser.Validate();
                var id = CruiserRepository.AddCruiser(cruiser);
                var savedCruiser = new Cruiser(id, cruiser.Name, cruiser.Status, cruiser.Capacity,
                    cruiser.LoadingShipCap, cruiser.Model, cruiser.Line, cruiser.Picture);
                return StatusCode(200, savedCruiser);
            }
            catch (NotValidAttributeException e)
            {
                var errorMsg = new ErrorMessage(400,e.Message);
                return BadRequest(errorMsg);
            }
            catch (DatabaseException e)
            {
                ErrorMessage errorMsg = new ErrorMessage(400,e.Message);
                return BadRequest(errorMsg);
            }
        }
        
        
        [HttpPut]
         public ActionResult<Cruiser> PutCruiser([FromBody] Cruiser cruiser)
         {
             try
             {
                 cruiser.Validate();
                 var updatedCruiser = CruiserRepository.UpdateCruiser(cruiser);
                 return StatusCode(200, cruiser);
             }
             catch (NotValidAttributeException e)
             {
                 ErrorMessage errorMsg = new ErrorMessage(400, e.Message);
                 return BadRequest(errorMsg);
             }
             catch (CruiserNotFoundException e)
             {
                 ErrorMessage errorMsg = new ErrorMessage(400, e.Message);
                 return BadRequest(errorMsg);
             }
         }

        [HttpDelete("{id}")]
        public ActionResult<int> DeleteCruiser(int id)
        {
            try
            {
                var deletedId = CruiserRepository.DeleteCruiser(id);
                return StatusCode(200, deletedId);
            }
            catch (CruiserNotFoundException e)
            {
                ErrorMessage errorMessage = new ErrorMessage(400,e.Message);
                return BadRequest(errorMessage);
            }
        }
    }
}