using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo13
{
    public class FindReservationVehicleCommand : CommandResult<ReservationVehicle>
    {

        private int _id;
        private ReservationVehicle _reservationVehicle;

        public FindReservationVehicleCommand(int reservationId)
        {
            _id = reservationId;
        }
        
        public ReservationVehicle GetResult()
        {
            return _reservationVehicle;
        }

        public void Execute()
        {
            var dao = DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetReservationVehicleDAO();
            _reservationVehicle = dao.Find(_id);
            if (_reservationVehicle.Id == 0)
            {
                throw new VehicleReservationNotFoundException();
            }
        }
    }
}