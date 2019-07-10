using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo3;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo3;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo3;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo3;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo3;

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
                GetAirplaneCommand f=CommandFactory.GetAirplaneCommand();
                f.Execute();
                return Ok( f.GetResult().ToList());
                
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
                GetAirplaneByIdCommand command = CommandFactory.GetFindPlaneIdCommand(id);
                command.Execute();
               // var result = AirplanesRepository.Find(id);
                return Ok(command.GetResult());
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