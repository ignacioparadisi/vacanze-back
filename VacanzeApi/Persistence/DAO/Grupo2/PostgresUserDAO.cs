using System;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo2;
using Microsoft.Extensions.Logging;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo2
{
    public class PostgresUserDAO : UserDAO
    {
        private const string SP_GETEMPLOYEES = "getEmployees()";
        private const string SP_GETUSERBYEMAIL = "GetUserByEmail(@email_id, @user_id)";
        private const string SP_GETUSERBYID = "GetUserById(@user_id)";
        private const string SP_ADD = "AddUser(@doc_id, @email, @lastname, @name, @password)";
        private const string SP_ADDUSER_ROLE = "AddUser_Role(@use_id, @rol_id)";
        private const string SP_DELETEUSER = "DeleteUserById(@user_id)";
        private const string SP_DELETEUSER_ROLE = "DeleteUser_Role(@user_id)";
        private const string SP_UPDATE = "ModifyUser(@id, @doc_id, @name, @lastname, @email)";
       // private readonly ILogger _logger;


        // GETS
        /// <summary>
        ///  Se conecta con la base de datos para seleccionar todos los usuarios que no sean clientes
        /// </summary>
        /// <returns>Una lista de usuarios que no tienen como rol cliente</returns>
        public List<User> GetEmployees()
          {
               try
               {
               // _logger.LogInformation("Entrando en GetEmployees");
                    var users = new List<User>();
                    var table = PgConnection.Instance.ExecuteFunction(SP_GETEMPLOYEES);
                    DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                    RoleDAO roles = factory.GetRoleDAO();
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                         var id = Convert.ToInt32(table.Rows[i][0]);
                         var documentId = Convert.ToInt64(table.Rows[i][1]);
                         var name = table.Rows[i][2].ToString();
                         var lastname = table.Rows[i][3].ToString();
                         var email = table.Rows[i][4].ToString();
                         var user = new User(id, documentId, name, lastname, email);

                         //Role roles = new List<Entity>();

                         user.Roles = roles.GetRolesForUser(id);
                         users.Add(user);
                    }
            //    _logger.LogDebug("Users:", users);
                return users;
               }
               catch(Exception e){
              //  _logger.LogError("Error", e);
                    e.ToString();
                    throw;
               }
          }
          

          /// <summary>
          /// Verifica en la base de datos que no exista un usuario con el email
          /// </summary>
          /// <param name="email">Email que se quiere verificar en la base de datos</param>
          /// <param name="id">El id del usuario (en caso de que sea actualizar) para que no actualice su email
          /// por uno ya existente pero que lo deje guardar su propio correo</param>
          /// <exception cref="RepeatedEmailException">Si el correo existe, se devuelve esta exception
          /// porque no pueden haber correos repetidos</exception>
          public void VerifyEmail(string email, int id = 0)
          {
        //    _logger.LogInformation("Entrando en VerifyEmail(email,id)",email,id);
            var table = PgConnection.Instance.ExecuteFunction(SP_GETUSERBYEMAIL,
                   email, id);
               if (table.Rows.Count > 0)
                    throw new RepeatedEmailException("El Email Ingresado ya Existe");
          //  _logger.LogInformation("Saliendo de GetEmployees");
        }

          /// <summary>
          /// Obtiene un usuario de la base de datos por su id
          /// </summary>
          /// <param name="id">Id del usuario que se desea buscar</param>
          /// <returns>El usuario que corresponde con el id</returns>
          /// <exception cref="UserNotFoundException">En caso de no encontrar ningun usuario se devuelve
          /// una excepcion para avisar que no se encontró ningún usuario con ese id</exception>
          public User GetUserById(int id)
          {
               try
               {
            //    _logger.LogInformation("Entrando en GetUserById(id)",id);
                User user;
                DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                RoleDAO roles = factory.GetRoleDAO();
                var table = PgConnection.Instance.ExecuteFunction(SP_GETUSERBYID, id);
                    if (table.Rows.Count == 0)
                         throw new UserNotFoundException("No se Encontró Usuario");

                    var user_id = Convert.ToInt32(table.Rows[0][0]);
                    var documentId = Convert.ToInt64(table.Rows[0][1]);
                    var name = table.Rows[0][2].ToString();
                    var lastname = table.Rows[0][3].ToString();
                    var email = table.Rows[0][4].ToString();
                    user = new User(user_id, documentId, name, lastname, email);

                    user.Roles = roles.GetRolesForUser(id);
            //    _logger.LogDebug("User:", user);
                return user;
               }
               catch(Exception e){
             //   _logger.LogError("Error", e);
                e.ToString();
                    throw;
               }
          }

          // - MARK: CREATES
          /// <summary>
          /// Agrega un nuevo usuario a la base de datos y encripta su contraseña
          /// </summary>
          /// <param name="user">Usuario que será almacenado en la base de datos</param>
          /// <returns>El usuario agregado en la base de datos con el ID</returns>
          public User AddUser(User entity)
          {
               try
               {
           //     _logger.LogInformation("Entrando en AddUser");
                User user = (User)entity;
                    user.Validate();
                    VerifyEmail(user.Email);
                    user.EncryptOrCreatePassword();
                    var table = PgConnection.Instance.
                    ExecuteFunction(SP_ADD,
                         user.DocumentId.ToString(), user.Email, user.Lastname, user.Name, user.Password);

                    var id = Convert.ToInt32(table.Rows[0][0]);
                    user.Id = id;
                    user.Password = null;

             //   _logger.LogDebug("User:", user);
                return user;
               }
               catch(Exception e){
            //    _logger.LogError("Error", e);
                e.ToString();
                    throw;
               }
          }

          /// <summary>
          /// Agrega los elementos necesarios a la tabla N a N entre Usuarios y Roles almacenar qué roles tiene
          /// cada usuario
          /// </summary>
          /// <param name="userid">Id del usuario</param>
          /// <param name="roleid">Id del rol</param>
          public void AddUser_Role(int userid, int roleid)
          {
               try
               {
             //   _logger.LogInformation("Entrando en AddUser_Role(userid,roleid)",userid,roleid);
                var table = PgConnection.Instance.ExecuteFunction(SP_ADDUSER_ROLE,
                    userid, roleid);
               }
               catch(DatabaseException e){
                    Console.WriteLine(e.ToString());
                    throw new Exception();
               }
          //  _logger.LogInformation("Saliendo de AddUser_Role(userid,roleid)", userid, roleid);
        }

          /// <summary>
          /// Elimina un usario de la base de datos por su id
          /// </summary>
          /// <param name="id">id del usuario que se desea eliminar</param>
          /// <returns>Si el usuario es eliminado satisfactoriamente, se devuelve su id</returns>
          /// <exception cref="UserNotFoundException">Si el id es inválido se devuelve la excepción
          /// diciendo que el usuario no existe</exception>
          public int DeleteUserById(int id)
          {
        //    _logger.LogInformation("Entrando en DeleteUserById(int id)",id);
            var table = PgConnection.Instance.ExecuteFunction(SP_DELETEUSER, id);
               var userId = table.Rows[0][0];
               if (userId == DBNull.Value)
               {
                    throw new UserNotFoundException("El usuario no se encuentra registrado.");
               }
          //  _logger.LogDebug("ID : ", id);
               return Convert.ToInt32(userId);
          }

          /// <summary>
          /// Elimina todos los roles del N a N entre roles y usuarios de un usuario especificado
          /// </summary>
          /// <param name="id">Id del usuario al que se le quieren eliminar los roles</param>
          public void DeleteUser_Role(int id)
          {
       //     _logger.LogInformation("Entrando en DeleteUserById(int id)", id);
            var table = PgConnection.Instance.ExecuteFunction(SP_DELETEUSER_ROLE, id);
        //    _logger.LogInformation("Saliendo de DeleteUserById(int id)", id);
        }

          // - MARK: UPDATE
          /// <summary>
          /// Actualiza en usuario en la base de datos con la nueva información
          /// </summary>
          /// <param name="user">Información del usuario actualizada</param>
          /// <param name="id">Id del usuario que se va a actualizar</param>
          /// <returns>El usuario actualizado</returns>
          /// <exception cref="UserNotFoundException">Excepción que se devuelve cuando
          /// no se encuentra ningun usuario con el id especificado en la base de datos</exception>
          // TODO: Devolver el usuario y no el id nada más
          public int UpdateUser(User entity, int id)
          {
       //     _logger.LogInformation("Entrando en UpdateUser(User entity, int id)",entity,id);
            User user = (User)entity;
               user.Validate();
               VerifyEmail(user.Email, id); //Hay que cambiar ese Id por user.Id en controller tambien
               var table = PgConnection.Instance
                   .ExecuteFunction(SP_UPDATE,
                   id, user.DocumentId.ToString(), user.Name, user.Lastname, user.Email);
               var userId = table.Rows[0][0];
               
            if (userId == DBNull.Value)
               {
                    throw new UserNotFoundException("El usuario no se encuentra registrado.");
               }
        //    _logger.LogDebug("UserID: ", userId);
            return Convert.ToInt32(userId);
          }

    }
}