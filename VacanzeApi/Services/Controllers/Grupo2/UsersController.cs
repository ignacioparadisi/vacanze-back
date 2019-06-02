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
        /// <summary>
        /// Obtiene solo los usuarios empleados
        /// </summary>
        /// <returns>Devuelve una lista de usuarios que son empleados</returns>
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
                return BadRequest("Error obteniendo los usuarios");
            }
            return users;
        }

        // GET api/users/5
        /// <summary>
        /// Busca un usuario con el id especificado
        /// </summary>
        /// <param name="id">Id del usuario que se desea buscar</param>
        /// <returns>Un usuario que corresponde al id especificado</returns>
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
        /// <summary>
        /// Manda a guardar el usuario en la base de datos
        /// </summary>
        /// <param name="user">Usuario que se desea guardar</param>
        /// <returns>Retorna el usuario almacenado</returns>
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            try
            {
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
                return BadRequest("Error agregando al usuario");
            }
            return user;
        }

        // PUT api/users/5
        /// <summary>
        /// Manda a actualizar el usuario en la base de datos
        /// </summary>
        /// <param name="id">Id del usuario que se desea actualizar</param>
        /// <param name="user">Información del usuario actualizada</param>
        /// <returns>Retorna el usuario actualizado</returns>
        [HttpPut("{id}")]
        // TODO: Retornar el usuario actualizado
        public ActionResult<int> Put(int id, [FromBody] User user)
        {
            int user_id;
            try
            {
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
            catch (Exception)
            {
                return BadRequest("Error actualizando al usuario");
            }
        }

        // DELETE api/users/1
        /// <summary>
        /// Manda a eliminar un usario por su id de la base de datos
        /// </summary>
        /// <param name="id">Id del usuario que se desea eliminar</param>
        /// <returns>Retorna el id del usuario que fue eliminado</returns>
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
                return BadRequest("Error eliminando al usuario");
            }
        }
    }
}