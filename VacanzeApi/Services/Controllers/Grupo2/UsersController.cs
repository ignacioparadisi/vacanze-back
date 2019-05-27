using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo2;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo2
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class UsersController : ControllerBase
    {
        // GET api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetEmployees()
        {
            var connection = new UserConnection();
            var users = new List<User>();
            try
            {
                users = connection.GetEmployees();
            }
            catch (DatabaseException)
            {
                return BadRequest("Error obteniendo los empleados");
            }

            return Ok(users);
        }

        // GET api/values/5
//        [HttpGet("{id}")]
//        public ActionResult<string> Get(int id)
//        {
//            return "value";
//        }

        // POST api/users
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            return user;
        }

        // PUT api/values/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody] string value)
//        {
//        }
//
//        // DELETE api/values/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }
    }
}