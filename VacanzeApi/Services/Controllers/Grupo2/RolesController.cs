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

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo2
{
    [Produces("application/json")] 
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        /*
        // GET api/values
        /// <summary>
        /// Obtienes los roles disponibles
        /// </summary>
        /// <returns>Una lista de roles</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Entity>> GetRoles()
        {
            var roles = new List<Entity>();
            try
            {
                roles = RoleRepository.GetRoles();
            }
            catch (DatabaseException e)
            {
                return BadRequest("Error al Obtener Roles");
            }

            return Ok(roles);
        }
        */

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
                GetRolesCommand command = CommandFactory.CreateGetRolesCommand();
                command.Execute();
                roles = command.GetResult();
                RoleMapper mapper = new RoleMapper();
                rolesDTO = mapper.CreateDTOList(roles);
            }
            catch (DatabaseException e)
            {
                return BadRequest("Error al Obtener Roles");
            }

            return Ok(rolesDTO);
        }
    }
}