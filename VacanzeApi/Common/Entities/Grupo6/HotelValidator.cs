using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.Command.Locations; 
using vacanze_back.VacanzeApi.LogicLayer.Command; 

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo6
{
    public class HotelValidator
    {
        /// <summary>
        ///     Valida que los campos de un <see cref="Hotel" /> sean correctos, lanzando excepciones
        ///     segun los errores detectados.
        /// </summary>
        /// <exception cref="RequiredAttributeException">Algun atributo requerido estaba como null</exception>
        /// <exception cref="InvalidAttributeException">Algun atributo tenia un valor invalido</exception>
        public static void Validate(Hotel hotel)
        {
            if (hotel.Name == null)
                throw new RequiredAttributeException(
                    "El nombre del hotel no fue especificado y es requerido");
            if (hotel.AddressSpecification == null)
                throw new RequiredAttributeException(
                    "La direccion especifica del hotel es necesaria");
            if (hotel.Location == null)
                throw new RequiredAttributeException("La ubicacion del hotel es necesaria");
            if (hotel.AmountOfRooms <= 0)
                throw new InvalidAttributeException(
                    "La cantidad de habitaciones debe ser mayor a 1");
            if (hotel.RoomCapacity <= 0)
                throw new InvalidAttributeException(
                    "La capacidad de personas por habitacion debe ser mayor a 1");
            if (hotel.Stars < 1 || hotel.Stars > 5)
                throw new InvalidAttributeException(
                    "La cantidad de estrellas debe estar entre 1 y 5");
            if (hotel.PricePerRoom < 0)
                throw new InvalidAttributeException(
                    "El precio por habitacion no puede ser negativo");

            try
            {     
                GetLocationByIdCommand commandId =  CommandFactory.GetLocationByIdCommand(hotel.Location.Id);
                commandId.Execute ();
            }
            catch (LocationNotFoundException)
            {
                throw new InvalidAttributeException(
                    $"El location (id: {hotel.Location.Id}) no es valido");
            }
        }
    }
}