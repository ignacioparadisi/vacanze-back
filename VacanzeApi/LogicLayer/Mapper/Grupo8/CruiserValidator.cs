using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo8
{
    public class CruiserValidator
    {
        /// <summary>
        ///     Metodo para validar todos los campos de un crucero
        /// </summary>
        /// <param name="layover">Objeto que contiene toda la informacion del crucero a validar</param>
        public static void Validate(Cruiser cruiser)
        {
            if (string.IsNullOrEmpty(cruiser.Name))
            {
                throw new InvalidAttributeException("El nombre es requerido");
            }
            if (cruiser.Capacity <= 0)
            {
                throw new InvalidAttributeException("La capacidad tiene que ser mayor a 0");
            }
            if (cruiser.LoadingShipCap <= 0)
            {
                throw new InvalidAttributeException("La capacidad de carga debe ser mayor a 0");
            }
            if (string.IsNullOrEmpty(cruiser.Model))
            {
                throw  new InvalidAttributeException("El modelo es requerido");
            }
            if (string.IsNullOrEmpty(cruiser.Line))
            {
                throw new InvalidAttributeException("La Linea del crucero es requerida");
            }

            if (string.IsNullOrEmpty(cruiser.Picture))
            {
                throw new InvalidAttributeException("La imagen es requerida");
            }
        }
    }
}