using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo13
{
    public class UpdateReservationVehicleCommand : CommandResult<ReservationVehicle>
    {
        private ReservationVehicle _reservationVehicle;
        
        public UpdateReservationVehicleCommand(ReservationVehicle resVehic)
        {
            _reservationVehicle = resVehic;
        }

        public void Execute()
        {
            if (_reservationVehicle.CheckIn.Equals(DateTime.MinValue))
                throw new ReservationHasNoCheckInException();
            if (_reservationVehicle.CheckOut.Equals(DateTime.MinValue))
                throw new ReservationHasNoCheckOutException();
            
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var reservationVehicleDao = (PostgresReservationVehicleDAO) daoFactory.GetReservationVehicleDAO();
            _reservationVehicle = reservationVehicleDao.Update(_reservationVehicle);
        }

        public ReservationVehicle GetResult()
        {
            return _reservationVehicle;
        }
    }
}