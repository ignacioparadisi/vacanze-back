using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo1
{
    public class Login:Entity
    {
        public List<Role> roles;
        public string email { get; set; }
        public string password { get; set; }

        public Login(int id,List<Role> roles, string email, String password):base(id)
        {
            this.roles = roles;
            this.email = email;
            this.password = password;
        }

        public Login(int id, string email, String password):base(id)
        {
            this.email = email;
            this.password = password;
        }

        public Login(String password):base(0)
        {
            this.password = password;
        }
    }
}
