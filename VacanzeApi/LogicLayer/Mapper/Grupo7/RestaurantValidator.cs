using vacanze_back.VacanzeApi.Common.Entities.Grupo7;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo7
{
    public class RestaurantValidator
    {
        public static void Validate(Restaurant restaurant)
        {
            if (string.IsNullOrEmpty(restaurant.Name))
            {
                throw new InvalidAttributeException("El nombre es requerido");
            }
            if (restaurant.Capacity <= 0)
            {
                throw new InvalidAttributeException("La capacidad tiene que ser mayor a 0");
            }
            if (restaurant.Qualify < 0)
            {
                throw new InvalidAttributeException("La Calificacion tiene que ser mayor a 0");
            }
            if (string.IsNullOrEmpty(restaurant.Specialty))
            {
                throw  new InvalidAttributeException("La especialidad es requerida");
            }
            if (restaurant.Price <= 0)
            {
                throw new InvalidAttributeException("El precio tiene que ser mayor a 0");
            }

            if (string.IsNullOrEmpty(restaurant.BusinessName))
            {
                throw  new InvalidAttributeException("El nombre de negocios es requerido");
            }
            
            if (string.IsNullOrEmpty(restaurant.Picture))
            {
                throw  new InvalidAttributeException("El nombre de negocios es requerido");
            }
            if (string.IsNullOrEmpty(restaurant.Description))
            {
                throw  new InvalidAttributeException("La descripcion es requerida");
            }
            if (string.IsNullOrEmpty(restaurant.Phone))
            {
                throw  new InvalidAttributeException("El telefono es requerido");
            }
            if (restaurant.Location <= 0)
            {
                throw  new InvalidAttributeException("El id de la locacion es requerido");
            }
            if (string.IsNullOrEmpty(restaurant.Address))
            {
                throw  new InvalidAttributeException("La direccion es requerida");
            }
        }
    }
}