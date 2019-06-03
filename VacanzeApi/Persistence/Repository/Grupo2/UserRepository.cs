using System;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo2
{
     public class UserRepository
     {

          // GETS
          /// <summary>
          ///  Se conecta con la base de datos para seleccionar todos los usuarios que no sean clientes
          /// </summary>
          /// <returns>Una lista de usuarios que no tienen como rol cliente</returns>
          public static List<User> GetEmployees()
          {
               var users = new List<User>();
               var table = PgConnection.Instance.ExecuteFunction("getEmployees()");

               for (int i = 0; i < table.Rows.Count; i++)
               {
                    var id = Convert.ToInt32(table.Rows[i][0]);
                    var documentId = Convert.ToInt64(table.Rows[i][1]);
                    var name = table.Rows[i][2].ToString();
                    var lastname = table.Rows[i][3].ToString();
                    var email = table.Rows[i][4].ToString();
                    var user = new User(id, documentId, name, lastname, email);

                    var roles = RoleRepository.GetRolesForUser(id);
                    user.Roles = roles;
                    users.Add(user);
               }

               return users;
          }

          /// <summary>
          /// Verifica en la base de datos que no exista un usuario con el email
          /// </summary>
          /// <param name="email">Email que se quiere verificar en la base de datos</param>
          /// <param name="id">El id del usuario (en caso de que sea actualizar) para que no actualice su email
          /// por uno ya existente pero que lo deje guardar su propio correo</param>
          /// <exception cref="RepeatedEmailException">Si el correo existe, se devuelve esta exception
          /// porque no pueden haber correos repetidos</exception>
          public static void VerifyEmail(string email, int id = 0)
          {
               var table = PgConnection.Instance.ExecuteFunction("GetUserByEmail(@email_id, @user_id)",
                   email, id);
               if (table.Rows.Count > 0)
                    throw new RepeatedEmailException("El Email Ingresado ya Existe");
          }

          /// <summary>
          /// Obtiene un usuario de la base de datos por su id
          /// </summary>
          /// <param name="id">Id del usuario que se desea buscar</param>
          /// <returns>El usuario que corresponde con el id</returns>
          /// <exception cref="UserNotFoundException">En caso de no encontrar ningun usuario se devuelve
          /// una excepcion para avisar que no se encontró ningún usuario con ese id</exception>
          public static User GetUserById(int id)
          {
               User user;
               var table = PgConnection.Instance.ExecuteFunction("GetUserById(@user_id)", id);
               if (table.Rows.Count == 0)
                    throw new UserNotFoundException("No se Encontró Usuario");

               var user_id = Convert.ToInt32(table.Rows[0][0]);
               var documentId = Convert.ToInt64(table.Rows[0][1]);
               var name = table.Rows[0][2].ToString();
               var lastname = table.Rows[0][3].ToString();
               var email = table.Rows[0][4].ToString();
               user = new User(user_id, documentId, name, lastname, email);

               var roles = RoleRepository.GetRolesForUser(id);
               user.Roles = roles;

               return user;
          }


          // - MARK: CREATES
          /// <summary>
          /// Agrega un nuevo usuario a la base de datos y encripta su contraseña
          /// </summary>
          /// <param name="user">Usuario que será almacenado en la base de datos</param>
          /// <returns>El usuario agregado en la base de datos con el ID</returns>
          public static User AddUser(User user)
          {
               user.Validate();
               VerifyEmail(user.Email);
               user.EncryptOrCreatePassword();
               var table = PgConnection.Instance.
                   ExecuteFunction("AddUser(@doc_id, @email, @lastname, @name, @password)",
                       user.DocumentId.ToString(), user.Email, user.Lastname, user.Name, user.Password);

               var id = Convert.ToInt32(table.Rows[0][0]);
               user.Id = id;
               user.Password = null;

               return user;
          }

          /// <summary>
          /// Agrega los elementos necesarios a la tabla N a N entre Usuarios y Roles almacenar qué roles tiene
          /// cada usuario
          /// </summary>
          /// <param name="userid">Id del usuario</param>
          /// <param name="roleid">Id del rol</param>
          public static void AddUser_Role(int userid, int roleid)
          {
               var table = PgConnection.Instance.ExecuteFunction("AddUser_Role(@use_id, @rol_id)",
                   userid, roleid);
          }


          // - MARK: DELETE
          // TODO: Ver si eliminar este método o no
          public static void DeleteUserByEmail(string email)
          {
               var table = PgConnection.Instance.ExecuteFunction("DeleteUserByEmail(@email_id)",
                   email);
          }

          /// <summary>
          /// Elimina un usario de la base de datos por su id
          /// </summary>
          /// <param name="id">id del usuario que se desea eliminar</param>
          /// <returns>Si el usuario es eliminado satisfactoriamente, se devuelve su id</returns>
          /// <exception cref="UserNotFoundException">Si el id es inválido se devuelve la excepción
          /// diciendo que el usuario no existe</exception>
          public static int DeleteUserById(int id)
          {
               var table = PgConnection.Instance.ExecuteFunction("DeleteUserById(@user_id)", id);
               var userId = table.Rows[0][0];
               if (userId == DBNull.Value)
               {
                    throw new UserNotFoundException("El usuario no se encuentra registrado.");
               }

               return Convert.ToInt32(userId);
          }

          /// <summary>
          /// Elimina todos los roles del N a N entre roles y usuarios de un usuario especificado
          /// </summary>
          /// <param name="id">Id del usuario al que se le quieren eliminar los roles</param>
          public static void DeleteUser_Role(int id)
          {
               var table = PgConnection.Instance.ExecuteFunction("DeleteUser_Role(@user_id)", id);
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
          public static int UpdateUser(User user, int id)
          {
               user.Validate();
               VerifyEmail(user.Email, id);
               var table = PgConnection.Instance
                   .ExecuteFunction("ModifyUser(@id, @doc_id, @name, @lastname, @email)",
                   id, user.DocumentId.ToString(), user.Name, user.Lastname, user.Email);
               var userId = table.Rows[0][0];
               if (userId == DBNull.Value)
               {
                    throw new UserNotFoundException("El usuario no se encuentra registrado.");
               }
               return Convert.ToInt32(userId);
          }
     }
}