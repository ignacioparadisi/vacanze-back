
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
                var cruiser = CruiserRepository.GetCruiser(id);
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
        [HttpPost("{cruiserId}/Layover")]
        public ActionResult<Layover> PostLayover([FromBody] Layover layover)
        {
            try
            {
                layover.Validate();
                var id = CruiserRepository.AddLayover(layover);
                var savedLayover = new Layover(id, layover.CruiserId, layover.DepartureDate, layover.ArrivalDate,
                    layover.Price,
                    layover.LocDeparture, layover.LocArrival);
                return Ok(savedLayover);
            }
            catch (InvalidAttributeException e)
            {
                var errorMsg = new ErrorMessage(e.Message);
                return BadRequest(errorMsg);
            }
            catch (CruiserNotFoundException e)
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
        [HttpDelete("Layover/{layoverId}")]
        public ActionResult<int> DeleteLayover(int layoverId)
        {
            try
            {
                var deletedId = CruiserRepository.DeleteLayover(layoverId);
                return Ok(new {id = deletedId});
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
        [HttpGet("{departure}/{arrival}/Layover")]
        public ActionResult<IEnumerable<Layover>> GetLayoverByLoc(int departure, int arrival)
        {
            try
            {
                var layovers = CruiserRepository.GetLayoversForRes(departure,arrival);
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
    }
}