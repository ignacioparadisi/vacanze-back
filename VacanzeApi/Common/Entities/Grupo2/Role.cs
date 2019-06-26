using System.Linq;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo2
{
    public class Role : Entity
    {
        // Constantes que contienen el id de los roles que pueden tener los usuarios
        public const int CLIENT = 1;
        public const int ADMIN = 2;
        public const int CHECKIN = 3;
        public const int CLAIM = 4;
        public const int CARRIER = 5;
        // Nombre del rol
        public string Name { get; set; }

        public Role (int id, string name) : base (id)
        {
            Name = name;
        }

        /// <summary>
        /// Valida si el id del rol recibido desde la aplicación web se encuentra entre los roles disponibles
        /// </summary>
        /// <returns>Devuelve true si el id es correcto</returns>
        /// <exception cref="NotValidIdException">Devuelve una excepcion si el id no es valido</exception>
        public bool Validate()
        {
            var availableRoles = new int[] {CLIENT, ADMIN, CHECKIN, CLAIM, CARRIER};
            if (!availableRoles.Contains(Id))
            {
                throw new NotValidIdException("El rol " + Id + " es inválido");
            }

            return true;
        }
        
        
    }
}