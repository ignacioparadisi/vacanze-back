using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo2
{
    public class RoleDTO : DTO
    {
        // Constantes que contienen el id de los roles que pueden tener los usuarios
        public int Id { get; set; }
        public const int CLIENT = 1;
        public const int ADMIN = 2;
        public const int CHECKIN = 3;
        public const int CLAIM = 4;
        public const int CARRIER = 5;

        // Nombre del rol
        public string Name { get; set; }

        public RoleDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Valida si el id del rol recibido desde la aplicación web se encuentra entre los roles disponibles
        /// </summary>
        /// <returns>Devuelve true si el id es correcto</returns>
        /// <exception cref="NotValidIdException">Devuelve una excepcion si el id no es valido</exception>
        public bool Validate()
        {
            var availableRoles = new int[] { CLIENT, ADMIN, CHECKIN, CLAIM, CARRIER };
            if (!availableRoles.Contains(Id))
            {
                throw new NotValidIdException("El rol " + Id + " es inválido");
            }

            return true;
        }


    }
}
