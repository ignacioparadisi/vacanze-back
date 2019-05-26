using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;

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
        public string GetErrorMessageIfNotValid()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return "El nombre es requerido";
            }

            if (string.IsNullOrEmpty(Lastname))
            {
                return "El apellido es requerido";
            }

            if (string.IsNullOrEmpty(Email))
            {
                return "El correo electrónico es requerido";
            }

            var emailAddressAttribute = new EmailAddressAttribute();
            if (!emailAddressAttribute.IsValid(Email))
            {
                return "El formato del correo electrónico es inválido";
            }

            if (!Roles.Any())
            {
                return "Al menos un rol es requerido";
            }

            if (DocumentId <= 0)
            {
                return "La cédula de identidad no es válida";
            }

            return null;
        }
    }
}