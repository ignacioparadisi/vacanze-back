using System;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo2
{
    public class UserRepository
    {
        
        // GETS
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
        
        public static void VerifyEmail(string email)
        {
            var table = PgConnection.Instance.ExecuteFunction("GetUserByEmail(@email_id)",
                email);
            if (table.Rows.Count > 0)
                throw new RepeatedEmailException("El Email Ingresado ya Existe");
        }

        public static User GetUserById(int id)
        {
            User user;
            var table = PgConnection.Instance.ExecuteFunction("GetUserById(@user_id)", id);
            if (table.Rows.Count == 0)
                throw new UserNotFoundException("No se Encontró Usuario");
            else
            {
                
                var user_id = Convert.ToInt32(table.Rows[0][0]);
                var documentId = Convert.ToInt64(table.Rows[0][1]);
                var name = table.Rows[0][2].ToString();
                var lastname = table.Rows[0][3].ToString();
                var email = table.Rows[0][4].ToString();
                user = new User(user_id, documentId, name, lastname, email);
            }

            return user;
        }
        
        
        // - MARK: CREATES
        public static User AddUser(User user)
        {
            var table = PgConnection.Instance.
                ExecuteFunction("AddUser(@doc_id, @email, @lastname, @name, @password)",
                    user.DocumentId.ToString(), user.Email, user.Lastname, user.Name, user.Password);

            var id = Convert.ToInt32(table.Rows[0][0]);
            user.Id = id;
            user.Password = null;

            return user;
        }

        public static void AddUser_Role(int userid, int roleid)
        {
            var table = PgConnection.Instance.ExecuteFunction("AddUser_Role(@use_id, @rol_id)",
                userid, roleid); 
        }

        
        // - MARK: DELETE
        public static void DeleteUserByEmail(string email)
        {
            var table = PgConnection.Instance.ExecuteFunction("DeleteUserByEmail(@email_id)",
                email);
        }

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

        public static void DeleteUser_Role(int id)
        {
            var table = PgConnection.Instance.ExecuteFunction("DeleteUser_Role(@user_id)", id);
        }
        
        // - MARK: UPDATE
        public static int UpdateUser(User user, int id)
        {
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