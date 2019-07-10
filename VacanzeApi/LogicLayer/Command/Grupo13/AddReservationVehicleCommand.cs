using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo2;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo13
{
    public class AddReservationVehicleCommand : CommandResult<ReservationVehicle>
    {
        private ReservationVehicle _reservationAutomobile;

        public AddReservationVehicleCommand(ReservationVehicle _reservationAutomobile)
        {
            this._reservationAutomobile = _reservationAutomobile;
        }
        public ReservationVehicle GetResult()
        {
            return _reservationAutomobile;
        }

        public void Execute()
        {
            if (_reservationAutomobile.UserId == 0 )
            {
                throw new ReservationHasNoUserException();
            }

            if (_reservationAutomobile.VehicleId == 0)
            {
                throw new ReservationHasNoVehicleException();
            }
                
            IReservationVehicleDAO dao = DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetReservationVehicleDAO();
            _reservationAutomobile = dao.AddReservation(_reservationAutomobile);
        }
    }
}