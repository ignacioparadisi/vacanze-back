using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.Connection.Grupo2;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo2
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class RolesController : ControllerBase
    {   
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Role>> GetRoles()
        {
            var con = new RoleConnection();
            var roles = new List<Role>();
            try
            {
                roles = con.GetRoles();
            }
            catch (DatabaseException e)
            {
                return Ok(e.Message);
            }

            return roles;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }
    }
}