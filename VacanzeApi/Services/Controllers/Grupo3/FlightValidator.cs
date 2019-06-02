using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo3;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo3;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo3;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo3
{
    /// <summary>Clase para validar la informacion de los vuelos</summary>
    public class FlightValidator
    {
        public FlightValidator(Flight flight)
        {
            this.flight = flight;
        }

        public Flight flight { get; set; }

        /// <summary>Metodo que valida la información del vuelo a agregar</summary>
        public void Validate()
        {
            
            if (flight.plane == null || flight.arrival == null || flight.departure == null ||
                flight.price == null || flight.loc_arrival == null || flight.loc_departure == null){
                    throw new ValidationErrorException("Se deben llenar todos los campos");
            }

            var plane = (Airplane) AirplanesRepository.Find(flight.plane.Id);

            if (plane == null)
                throw new ValidationErrorException("El avión seleccionado no existe");


            if( flight.loc_departure.Id == flight.loc_arrival.Id ){
                throw new ValidationErrorException("La ciudad de salida no puede ser igual a la de llegada");
            }    

            DateTime arrivalDate= DateTime.ParseExact(flight.arrival, "MM-dd-yyyy HH:mm:ss",null);
            DateTime departureDate= DateTime.ParseExact(flight.departure, "MM-dd-yyyy HH:mm:ss",null);
            TimeSpan hours = arrivalDate - departureDate;

            if( hours.TotalHours > plane.autonomy ){
                throw new ValidationErrorException("La autonomía del avión no es suficiente para realizar este vuelo");
            }


            if( DateTime.Compare(departureDate, arrivalDate) > 0 ){
                throw new ValidationErrorException("La fecha de salida no puede ser mayor a la de llegada");
            }

        }


        /*
        * Revisa si existe el vuelo
        * @param int id: id del vuelo
        */
        /* public void CheckIfExist(long id){

            FlightsConnection flightcon = new FlightsConnection();
            Flight flight = (Flight) flightcon.Find(id);

            if(flight == null){
                throw new ValidationErrorException("El vuelo a editar no existe");
            }

        }*/

    }
}