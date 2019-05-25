using System;
using vacanze_back.Entities.Grupo3;
using vacanze_back.Entities;
using vacanze_back.Exceptions.Grupo3;
using vacanze_back.Connection.Grupo3;

namespace vacanze_back.Controllers.Grupo3
{
    public class FlightValidator
    {
        public Flight flight { get; set; }

        public FlightValidator(Flight flight){
            this.flight = flight;
        }

        public void Validate(){
            AirplanesConnection aircon = new AirplanesConnection();
            Airplane plane = (Airplane) aircon.Find(flight.plane.Id);

            if(flight.plane == null || flight.arrival == null || flight.departure == null || flight.price == null
            || flight.routes == null || flight.routes[0].locArrival == null || flight.routes[0].locDeparture == null){
                throw new ValidationErrorException("Debe llenar todos los campos");
            }

            DateTime arrivalDate= DateTime.ParseExact(flight.arrival, "dd-mm-yy",null);
            DateTime departureDate= DateTime.ParseExact(flight.departure, "dd-mm-yy",null);;

            if(plane == null){
                throw new ValidationErrorException("El seleccionado avión no existe");
            }

            if( DateTime.Compare(departureDate, arrivalDate) > 0 ){
                throw new ValidationErrorException("La fecha de salida no puede ser mayor a la de llegada");
            }

            

        }

    }
}