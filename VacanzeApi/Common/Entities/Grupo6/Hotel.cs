using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo6
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AmountOfRooms { get; set; }
        public int RoomCapacity { get; set; }
        public bool IsActive { get; set; }
        public string AddressSpecification { get; set; }
        public decimal PricePerRoom { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Picture { get; set; }
        public int Stars { get; set; }
        public Location Location { get; set; }

        public void Validate()
        {
            if (Name == null)
                throw new RequiredAttributeException(
                    "El nombre del hotel no fue especificado y es requerido");
            if (AddressSpecification == null)
                throw new RequiredAttributeException(
                    "La direccion especifica del hotel es necesaria");
            if (Location == null)
                throw new RequiredAttributeException("La ubicacion del hotel es necesaria");
            if (AmountOfRooms <= 0)
                throw new InvalidAttributeException(
                    "La cantidad de habitaciones debe ser mayor a 1");
            if (RoomCapacity <= 0)
                throw new InvalidAttributeException(
                    "La capacidad de personas por habitacion debe ser mayor a 1");
            if (Stars < 1 || Stars > 5)
                throw new InvalidAttributeException(
                    "La cantidad de estrellas debe estar entre 1 y 5");
            if (PricePerRoom < 0)
                throw new InvalidAttributeException(
                    "El precio por habitacion no puede ser negativo");
        }
    }
}