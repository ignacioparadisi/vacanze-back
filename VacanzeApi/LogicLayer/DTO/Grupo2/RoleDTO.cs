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
        public int _id { get; set; }
        public const int _CLIENT = 1;
        public const int _ADMIN = 2;
        public const int _CHECKIN = 3;
        public const int _CLAIM = 4;
        public const int _CARRIER = 5;

        // Nombre del rol
        public string _Name { get; set; }

        public RoleDTO(int id, string name)
        {
            _Name = name;
        }

        /// <summary>
        /// Valida si el id del rol recibido desde la aplicación web se encuentra entre los roles disponibles
        /// </summary>
        /// <returns>Devuelve true si el id es correcto</returns>
        /// <exception cref="NotValidIdException">Devuelve una excepcion si el id no es valido</exception>
        public bool Validate()
        {
            var availableRoles = new int[] { _CLIENT, _ADMIN, _CHECKIN, _CLAIM, _CARRIER };
            if (!availableRoles.Contains(_id))
            {
                throw new NotValidIdException("El rol " + _id + " es inválido");
            }

            return true;
        }


    }
}
