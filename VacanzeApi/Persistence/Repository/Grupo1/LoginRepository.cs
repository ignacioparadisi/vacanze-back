

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using vacanze_back.VacanzeApi.Common.Entities.Grupo2;

    public class LoginRepository
    {
        public User SessionLogin(string Email, string Password)
        {
            try
            {
                var view=PgConnection.Instance.ExecuteFunction("LoginRepository(@Email,@Password)", Email, Password);
                var Id = Convert.ToInt32(view.Rows[0][0]);
                var Name = view.Rows[0][1].ToString();
                var LastName = view.Rows[0][2].ToString();
                var roles = new List<Role>();
                for (var i = 0; i < view.Rows.Count; i++)
                {
                    Role objRol = new Role(Convert.ToInt32(view.Rows[i][3]), view.Rows[i][4].ToString());
                    roles.Add(objRol);
                }
                var user = new User(Id, 000, Name, LastName, Email, Password, roles);
                return user;
            }
            catch (Exception e)
            {
               throw new Exception(e.Message+"o"+"Password or Username Incorrect");
            }

        }
    }

}
