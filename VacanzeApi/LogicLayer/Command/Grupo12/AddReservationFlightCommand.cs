using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo12;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo12;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo12;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo12
{
    public class AddReservationFlightCommand: Command, CommandResult<int>
    {
        private int Id;
        public FlightRes FlightReservation { get; set; }   

        
        public AddReservationFlightCommand(FlightRes flightReservation) 
        {
            this.FlightReservation = flightReservation;
        } 
        

        public void Execute(){

        //try
       // {

            //Obtiene el DAO correspondiente por medio de las factories
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            ReservationFlightDAO ResFlightDao = factory.GetReservationFlightDAO();


            //Valida que el vuelo existe
            bool f = ResFlightDao.FindFlight(FlightReservation._id_fli); 
            if( !f ){

                    throw new ValidationErrorException("El vuelo a reservar no existe");

            }

            //Valida que el usuario existe
            f = ResFlightDao.FindUser(FlightReservation._id_user); 
            if( !f ){

                    throw new ValidationErrorException("El usuario no existe");

            }

            //Valida que el número a reservar sea válido
            if(FlightReservation._numPas < 1){
                throw new ValidationErrorException("El número de acientos a reservar es inválido");
            }

            //Guarda en la reserva los nombres de los asientos a reservar
            string seat=ResFlightDao.ConSeatNum(FlightReservation._numPas,FlightReservation._id_fli);
            FlightReservation._seatNum = seat;

            //Valida que se pueda reservar
            if( !seat.Equals("0") ){
               
                this.Id = ResFlightDao.AddReservationFlight(FlightReservation);

            }else{
                Console.WriteLine("El numero de asientos excede a la cantida de reservas disponibles");
                throw new ValidationErrorException("No existen suficientes asientos sin reservar que coincida con su solicitud");            
            }


        /*  }
        catch (ValidationErrorException ex)
        {
           throw new ValidationErrorException(ex.Message);
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }*/

            

        }

        public int GetResult(){
            return this.Id;
        }

    }
}