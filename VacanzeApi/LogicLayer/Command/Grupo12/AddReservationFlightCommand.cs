using vacanze_back.VacanzeApi.Common.Entities.Grupo12;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo12;

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

            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            ReservationFlightDAO ResFlightDao = factory.GetReservationFlightDAO();
            string seat=ResFlightDao.ConSeatNum(FlightReservation._numPas,FlightReservation._id_fli);
            FlightReservation._seatNum = seat;
            this.Id = ResFlightDao.AddReservationFlight(FlightReservation);

        }

        public int GetResult(){
            return this.Id;
        }

    }
}