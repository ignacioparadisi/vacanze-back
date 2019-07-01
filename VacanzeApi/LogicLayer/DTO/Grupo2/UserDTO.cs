using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;

namespace vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo2
{
    public class UserDTO : DTO
    {

        public int _Id { get; set; }
        public long _DocumentId { get; set; }
        public string _Email { get; set; }
        public string _Lastname { get; set; }
        public string _Name { get; set; }
        public string _Password { get; set; }
        public List<Role> _Roles { get; set; }

        public UserDTO(int Id, long DocumentId, string Email, string Lastname, string Name, string Password, List<Role> Roles)
        {
            _Id = Id;
            _DocumentId = DocumentId;
            _Email = Email;
            _Lastname = Lastname;
            _Name = Name;
            _Password = Password;
            _Roles = Roles;
        }

        public UserDTO()
        {
        }
    }
}
