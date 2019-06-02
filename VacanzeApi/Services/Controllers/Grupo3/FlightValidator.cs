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

        /// <summary>Metodo que valida la informacion del vuelo a agregar</summary>
        public void Validate()
        {
            
            if (flight.plane == null || flight.arrival == null || flight.departure == null ||
                flight.price == null || flight.loc_arrival == null || flight.loc_departure == null){
                    throw new ValidationErrorException("All fields must be filled");
            }

            var plane = (Airplane) AirplanesRepository.Find(flight.plane.Id);

            if( flight.loc_departure.Id == flight.loc_arrival.Id ){
                throw new ValidationErrorException("The departure city and the arrival city cannot be the same");
            }    

            DateTime arrivalDate= DateTime.ParseExact(flight.arrival, "MM-dd-yyyy HH:mm:ss",null);
            DateTime departureDate= DateTime.ParseExact(flight.departure, "MM-dd-yyyy HH:mm:ss",null);
            TimeSpan hours = arrivalDate - departureDate;

            if( hours.TotalHours > plane.autonomy){
                throw new ValidationErrorException("The autonomy of the plane is not enough to make this flight");
            }

            if (plane == null)
                throw new ValidationErrorException("The plane you selected does not exist");

            if( DateTime.Compare(departureDate, arrivalDate) > 0 ){
                throw new ValidationErrorException("The departure date cannot be greater than the arrival date");
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