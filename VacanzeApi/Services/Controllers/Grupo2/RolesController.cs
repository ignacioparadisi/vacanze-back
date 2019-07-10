using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo2;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo2;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo2;
using Microsoft.Extensions.Logging;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo2
{
    [Produces("application/json")] 
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ILogger _logger;

        // GET api/values
        /// <summary>
        /// Obtienes los roles disponibles
        /// </summary>
        /// <returns>Una lista de roles</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Role>> GetRoles()
        {
            var roles = new List<Role>();
            var rolesDTO = new List<RoleDTO>();
            try
            {
                _logger.LogInformation("Ejecutando el Comando GetRolesCommand()");
                GetRolesCommand command = CommandFactory.CreateGetRolesCommand();
                command.Execute();
                _logger.LogInformation("Ejecutado el Comando GetRolesCommand()");
                roles = command.GetResult();
                RoleMapper mapper = new RoleMapper();
                rolesDTO = mapper.CreateDTOList(roles);
            }
            catch (DatabaseException e)
            {
                _logger.LogError("BadRequest: ", e);
                return BadRequest("Error al Obtener Roles");
            }

            return Ok(rolesDTO);
        }
    }
}