using System;
using vacanze_back.Common.Entities.Grupo3;
using vacanze_back.Common.Entities;
using vacanze_back.Common.Exceptions.Grupo3;
using vacanze_back.Persistence.Connection.Grupo3;

namespace vacanze_back.Services.Controllers.Grupo3
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
            || flight.loc_arrival == null || flight.loc_departure == null){
                throw new ValidationErrorException("Debe llenar todos los campos");
            }

            // DateTime arrivalDate= DateTime.ParseExact(flight.arrival, "dd-MM-yy hh:mm:ss",null);
            // DateTime departureDate= DateTime.ParseExact(flight.departure, "dd-MM-yy hh:mm:ss",null);

            if(plane == null){
                throw new ValidationErrorException("El seleccionado avión no existe");
            }

            // if( DateTime.Compare(departureDate, arrivalDate) > 0 ){
            //     throw new ValidationErrorException("La fecha de salida no puede ser mayor a la de llegada");
            // }

            

        }

    }
}