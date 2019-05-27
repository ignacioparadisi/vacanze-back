using vacanze_back.VacanzeApi.Common.Entities.Grupo3;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo3;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo3;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo3
{
    public class FlightValidator
    {
        public FlightValidator(Flight flight)
        {
            this.flight = flight;
        }

        public Flight flight { get; set; }

        public void Validate()
        {
            var aircon = new AirplanesConnection();
            var plane = (Airplane) aircon.Find(flight.plane.Id);

            if (flight.plane == null || flight.arrival == null || flight.departure == null ||
                flight.price == null
                || flight.loc_arrival == null || flight.loc_departure == null)
                throw new ValidationErrorException("Debe llenar todos los campos");

            // DateTime arrivalDate= DateTime.ParseExact(flight.arrival, "dd-MM-yy hh:mm:ss",null);
            // DateTime departureDate= DateTime.ParseExact(flight.departure, "dd-MM-yy hh:mm:ss",null);

            if (plane == null)
                throw new ValidationErrorException("El seleccionado avión no existe");

            // if( DateTime.Compare(departureDate, arrivalDate) > 0 ){
            //     throw new ValidationErrorException("La fecha de salida no puede ser mayor a la de llegada");
            // }
        }
    }
}