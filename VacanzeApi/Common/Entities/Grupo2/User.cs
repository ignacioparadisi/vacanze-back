using System;
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

        public User(int id, long documentId, string name, string lastname, string email,
            string password, List<Role> roles) : base(id)
        {
            DocumentId = documentId;
            Name = name.Trim();
            Lastname = lastname.Trim();
            Email = email.Trim();
            Password = password;
            Roles = roles;
        }

        public User(int id, long documentId, string name, string lastname, string email) : base(id)
        {
            DocumentId = documentId;
            Name = name.Trim();
            Lastname = lastname.Trim();
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
        public User(int id, long documentId, string name, string lastname, string email, string password) : base(id)
        {
            DocumentId = documentId;
            Name = name.Trim();
            Lastname = lastname.Trim();
            Email = email.Trim();
            Password = password;
        }

        /// <summary>
        /// Verifica que todos los atributos del usuario sean válidos
        /// </summary>
        /// <returns>Retorna true si todos los atributos son válidos</returns>
        /// <returns>Mensaje de error que se envia al frontend en caso de que haya algun error</returns>
        public bool Validate()
        {

            if (DocumentId <= 0)
            {
                throw new NotValidDocumentIdException("La cédula de identidad no es válida");
            }

            if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
            {
                throw new NameRequiredException("El nombre es requerido");
            }

            if (string.IsNullOrEmpty(Lastname) || string.IsNullOrWhiteSpace(Lastname))
            {
                throw new LastnameRequiredException("El apellido es requerido");
            }

            if (string.IsNullOrEmpty(Email) || string.IsNullOrWhiteSpace(Email))
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

            var rolesToBeDeleted = new List<int>();
            for (var i = 0; i < Roles.Count -1; i++)
            {
                for (var j = i + 1; j < Roles.Count; j++)
                {
                    if (Roles[i].Id == Roles[j].Id)
                    {
                        rolesToBeDeleted.Add(j);
                    }
                }
            }

            rolesToBeDeleted = rolesToBeDeleted.Distinct().ToList();
            rolesToBeDeleted = rolesToBeDeleted.OrderByDescending(id => id).ToList();

            foreach (var rol in rolesToBeDeleted)
            {
                Roles.RemoveAt(rol);
            }

            foreach (var role in Roles)
            {
                role.Validate();
                if (role.Id == Role.ADMIN && Roles.Count > 1)
                {
                    throw new AdminAndMoreRolesException("El administrador no puede tener otros roles");
                }
            }

            return true;
        }

        /// <summary>
        /// Encripta la contraseña del usuario. Si es cliente verifica que no sea nula, si es empleado,
        /// genera una contraseña concatenando la primera letra del nombre, la primera letra del apellido
        /// y el número de cédula
        /// </summary>
        /// <exception cref="PasswordRequiredException">Excepción retornada si el usuario es cliente
        /// y envía una contraseña nula o vacía</exception>
        public void EncryptOrCreatePassword()
        {
            var isClient = false;
            foreach (var role in Roles)
            {
                if (role.Id == Role.CLIENT)
                {
                    isClient = true;
                }
            }
            
            if (!isClient)
            {
                Password = Name.Trim().ToLower()[0].ToString() + Lastname.Trim().ToLower()[0].ToString() + DocumentId.ToString();
                Password = Encryptor.Encrypt(Password);
            }
            else if (string.IsNullOrEmpty(Password) || string.IsNullOrWhiteSpace(Password))
            {
                throw new PasswordRequiredException("La contraseña es requerida");
                
            } else if (Password.Length < 8)
            {
                throw new NotValidFieldException("La contraseña debe tener como mínimo 8 caracteres");
            }
            else
            {
                Password = Encryptor.Encrypt(Password);
            }
        }
    }
}