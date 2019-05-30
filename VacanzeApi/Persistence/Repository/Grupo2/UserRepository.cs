using System;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo2
{
     public class UserRepository
     {
          public static List<User> GetEmployees()
          {
               var users = new List<User>();
               var table = PgConnection.Instance.ExecuteFunction("getEmployees()");

               for (int i = 0; i < table.Rows.Count; i++)
               {
                    var id = Convert.ToInt64(table.Rows[i][0]);
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

          public static User AddUser(User user)
          {
               var table = PgConnection.Instance.
                   ExecuteFunction("AddUser(@doc_id, @email, @lastname, @name, @password)",
                       user.DocumentId.ToString(), user.Email, user.Lastname, user.Name, user.Password);

               var id = Convert.ToInt64(table.Rows[0][0]);
               user.Id = id;
               user.Password = null;

               return user;
          }

          public static void AddUser_Role(long userid, long roleid)
          {
               var table = PgConnection.Instance.ExecuteFunction("AddUser_Role(@rol_id, @use_id)",
                   roleid, userid);
          }

          public static void VerifyEmail(string email)
          {
               var table = PgConnection.Instance.ExecuteFunction("GetUserByEmail(@email_id)",
                   email);
               if (table.Rows.Count > 0)
                    throw new RepeatedEmailException("El Email Ingresado ya Existe");
          }

          public static void DeleteUserByEmail(string email)
          {
               var table = PgConnection.Instance.ExecuteFunction("DeleteUserByEmail(@email_id)",
                   email);
          }
     }
}