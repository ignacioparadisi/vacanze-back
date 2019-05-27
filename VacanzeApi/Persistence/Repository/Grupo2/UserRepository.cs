using System;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;

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
        
        public User AddClient(User user)
        {
            var table = PgConnection.Instance.
                ExecuteFunction("AddUser(@_doc_id, @_name, @_lastname, @_email, @_password)",
                    user.DocumentId, user.Name, user.Lastname, user.Email, user.Password);

            var id = Convert.ToInt64(table.Rows[0][0]);
            user.Id = id;
            user.Password = null;

            return user;
        }
    }
}