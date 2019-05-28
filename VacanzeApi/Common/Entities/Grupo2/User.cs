using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo2
{
    public class User : Entity
    {
        public long DocumentId;
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

        public User(long id, long documentId, string name, string lastname, string email) : base(id)
        {
            DocumentId = documentId;
            Name = name;
            Lastname = lastname;
            Email = email;
        }

        /// <summary>
        /// Constructor que se utiliza al momento de crear un usuario para convertir de JSON a User
        /// </summary>
        /// <param name="documentId">Cédula de identidad</param>
        /// <param name="name">Nombre del usuario</param>
        /// <param name="lastname">Apellido del usuario</param>
        /// <param name="email">Correo electrónico del usuario</param>
        /// <param name="password">Contraseña del usuario</param>
        [JsonConstructor]
        public User(long documentId, string name, string lastname, string email, string password) : base(0)
        {
            DocumentId = documentId;
            Name = name;
            Lastname = lastname;
            Email = email;
            if (!string.IsNullOrEmpty(password))
            {
                Password = Encryptor.Encrypt(password);
            }
        }
        
        /// <summary>
        /// Verifica todos los atributos del usuario para verificar que sean válidos
        /// </summary>
        /// <returns>Mensaje de error que se envia al frontend en caso de que haya algun error</returns>
        public void Validate()
        {
            if (DocumentId <= 0)
            {
                throw new NotValidDocumentIdException("La cédula de identidad no es válida");
            }
            
            if (string.IsNullOrEmpty(Name))
            {
                throw new NameRequiredException("El nombre es requerido"); 
            }

            if (string.IsNullOrEmpty(Lastname))
            {
                throw new LastnameRequiredException("El apellido es requerido");
            }

            if (string.IsNullOrEmpty(Email))
            {
                throw new EmailRequiredException("El correo electrónico es requerido");
            }

            var emailAddressAttribute = new EmailAddressAttribute();
            if (!emailAddressAttribute.IsValid(Email))
            {
                throw new NotValidEmailException("El formato del correo electrónico es inválido");
            }

            if (Roles == null || !Roles.Any())
            {
                throw new RoleRequiredException("Al menos un rol es requerido");
            }

            foreach (var role in Roles)
            {
                role.Validate();
            }
        }
    }
}