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
        private Role[] roles =
        {
            new Role(0, "Cliente"),
            new Role(1, "Administrador"),
            new Role(2, "Checkin"),
            new Role(3, "Reclamo"),
            new Role(4, "Cargador") 
        };
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Role>> Get()
        {
            Console.WriteLine(roles);
            return roles.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }
    }
}