using System.Collections.Generic;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo2
{
    public class User : Entity
    {
        public long DocumentId { get; set; }
        public string Email;
        public string Lastname;
        public string Name;
        public string Password;
        public List<Role> Roles;

        public User(long id, long documentId, string name, string lastname, string email,
            string password, List<Role> roles) : base(id)
        {
            DocumentId = documentId;
            Name = name;
            Lastname = lastname;
            Email = email;
            Password = password;
            Roles = roles;
        }
    }
}