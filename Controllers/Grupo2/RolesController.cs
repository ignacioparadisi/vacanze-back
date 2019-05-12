using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.Entities.Grupo2;

namespace vacanze_back.Controllers.Grupo2
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {   
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Role>> Get()
        {
            List<Role> roles = new List<Role>();
            roles.Add(new Role(0, "Cliente"));
            roles.Add(new Role(1, "Administrador"));
            roles.Add(new Role(2, "Checkin"));
            roles.Add(new Role(3, "Reclamo"));
            roles.Add(new Role(4, "Cargador"));
            return Ok(roles.ToList());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }
    }
}