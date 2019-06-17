using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo1
{
    public class Login:Entity
    {
        public List<Role> Roles;
        public string Email { get; set; }
        public string Password { get; set; }

        public Login(int Id,List<Role> Roles, string Email, String Password):base(Id)
        {
            this.Id = Id;
            this.Roles = Roles;
            this.Email = Email;
            this.Password = Password;
        }
    }
}
