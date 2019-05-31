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
                ErrorMessage errorMessage = new ErrorMessage(e.Message);
                return BadRequest(errorMessage);
            }
            catch (DatabaseException e)
            {
                ErrorMessage errorMessage = new ErrorMessage(e.Message);
                return BadRequest(errorMessage);
            }
        }

        // GET/Cruiser/{id}
        [HttpGet("{id}")]
        public ActionResult<Cruiser> GetCruiser(int id)
        {
            try
            {
                Cruiser cruiser = CruiserRepository.GetCruiser(id);

                return Ok(cruiser);
            }
            catch (CruiserNotFoundException e)
            {
                ErrorMessage errorMessage = new ErrorMessage(e.Message);
                return BadRequest(errorMessage);
            }
            catch (DatabaseException e)
            {
                ErrorMessage errorMessage = new ErrorMessage(e.Message);
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
                return Ok(savedCruiser);
            }
            catch (InvalidAttributeException e)
            {
                var errorMsg = new ErrorMessage(e.Message);
                return BadRequest(errorMsg);
            }
            catch (DatabaseException e)
            {
                ErrorMessage errorMsg = new ErrorMessage(e.Message);
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
                return Ok(updatedCruiser);
            }
            catch (InvalidAttributeException e)
            {
                ErrorMessage errorMsg = new ErrorMessage(e.Message);
                return BadRequest(errorMsg);
            }
            catch (CruiserNotFoundException e)
            {
                ErrorMessage errorMessage = new ErrorMessage(e.Message);
                return BadRequest(errorMessage);
            }
            catch (NullCruiserException e)
            {
                ErrorMessage errorMessage = new ErrorMessage(e.Message);
                return BadRequest(errorMessage);
            }
            catch (DatabaseException e)
            {
                ErrorMessage errorMessage = new ErrorMessage(e.Message);
                return BadRequest(errorMessage);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<int> DeleteCruiser(int id)
        {
            try
            {
                var deletedId = CruiserRepository.DeleteCruiser(id);
                return Ok(new {id = deletedId});
            }
            catch (CruiserNotFoundException e)
            {
                ErrorMessage errorMessage = new ErrorMessage(e.Message);
                return BadRequest(errorMessage);
            }
            catch (DatabaseException e)
            {
                ErrorMessage errorMessage = new ErrorMessage(e.Message);
                return BadRequest(errorMessage);
            }
        }

        [HttpGet("{cruiserId}/Layover")]
        public ActionResult<IEnumerable<Layover>> GetLayovers(int cruiserId)
        {
            try
            {
                var layovers = CruiserRepository.GetLayovers(cruiserId);
                return Ok(layovers);
            }
            catch (LayoverNotFoundException e)
            {
                ErrorMessage errorMessage = new ErrorMessage(e.Message);
                return BadRequest(errorMessage);
            }
            catch (DatabaseException e)
            {
                ErrorMessage errorMessage = new ErrorMessage(e.Message);
                return BadRequest(errorMessage);
            }
        }

        [HttpDelete("Layover/{layover_id}")]
        public ActionResult<int> DeleteLayover(int layoverid)
        {
            try
            {
                var deletedId = CruiserRepository.DeleteLayover(layoverid);
                return Ok(deletedId);
            }
            catch (LayoverNotFoundException e)
            {
                ErrorMessage errorMessage = new ErrorMessage(e.Message);
                return BadRequest(errorMessage);
            }
            catch (DatabaseException e)
            {
                ErrorMessage errorMessage = new ErrorMessage(e.Message);
                return BadRequest(errorMessage);
            }
        }
    }
}