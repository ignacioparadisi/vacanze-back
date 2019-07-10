using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo13
{
    public class DeleteReservationVehicleCommand
    {
        private ReservationVehicle _reservationVehicle;
        private int _result;

        public DeleteReservationVehicleCommand(ReservationVehicle reservationVehicle)
        {
            _reservationVehicle = reservationVehicle;
        }

        public void Execute()
        {
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var resVehicDao = (PostgresReservationVehicleDAO) daoFactory.GetReservationVehicleDAO();
            _result = resVehicDao.Delete(_reservationVehicle);
        }

        public int GetResult()
        {
            return _result;
        }
    }
}