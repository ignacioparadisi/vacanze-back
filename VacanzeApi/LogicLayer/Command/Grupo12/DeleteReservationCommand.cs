using vacanze_back.VacanzeApi.Common.Entities.Grupo12;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo12;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo12
{
    public class DeleteReservationCommand : Command
    {
        private int Id;

        public DeleteReservationCommand(int Id)
        {
            this.Id = Id;
        }

        public void Execute(){
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            ReservationFlightDAO ResFlightDao = factory.GetReservationFlightDAO();
            ResFlightDao.DeleteReservationFlight(this.Id);
        }
    }
}