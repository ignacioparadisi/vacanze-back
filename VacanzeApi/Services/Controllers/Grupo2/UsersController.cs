using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo2;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo2
{
    [Produces("application/json")] 
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetEmployees()
        {
            var users = new List<User>();
            try
            {
                users = UserRepository.GetEmployees();
            }
            catch (DatabaseException e)
            {
                return BadRequest(e.Message);
            }
            return users;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            User user;
            try
            {
                user = UserRepository.GetUserById(id);
                user.Roles = RoleRepository.GetRolesForUser(id);
            }
            catch (GeneralException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return BadRequest("Error de servidor");
            }
            return user;
        }

        // POST api/users
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            try
            {
                user.Validate();
                UserRepository.VerifyEmail(user.Email);
                user = UserRepository.AddUser(user);
                foreach (var roles in user.Roles)
                {
                    UserRepository.AddUser_Role(user.Id, roles.Id);
                }
            }
            catch (GeneralException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return BadRequest("Error del servidor");
            }
            return user;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<int> Put(int id, [FromBody] User user)
        {
            int user_id;
            try
            {
                user.Validate(true);
                user_id = UserRepository.UpdateUser(user, id);
                UserRepository.DeleteUser_Role(id);
                foreach (var role in user.Roles)
                {
                    UserRepository.AddUser_Role(id, role.Id);
                }

                return user_id;

            }
            catch (GeneralException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/users/1
        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            try
            {
                return UserRepository.DeleteUserById(id);
            }
            catch (GeneralException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return BadRequest("Error del servidor");
            }
        }
    }
}