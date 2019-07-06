using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;

namespace vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo2
{
    public class UserDTO : DTO
    {

        public int Id { get; set; }
        public long DocumentId { get; set; }
        public string Email { get; set; }
        public string Lastname { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public List<Role> Roles { get; set; }

        public UserDTO(int Id, long DocumentId, string Name, string Lastname, string Email, string Password, List<Role> Roles)
        {
            this.Id = Id;
            this.DocumentId = DocumentId;
            this.Email = Email;
            this.Lastname = Lastname;
            this.Name = Name;
            this.Password = Password;
            this.Roles = Roles;
        }

        public UserDTO()
        {
        }
    }
}