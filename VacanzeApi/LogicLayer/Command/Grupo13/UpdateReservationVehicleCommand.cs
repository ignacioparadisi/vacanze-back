using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo13
{
    public class UpdateReservationVehicleCommand : CommandResult<ReservationVehicle>
    {
        /// <summary>
        ///  Reservación de vehículo actualizada
        /// </summary>
        private ReservationVehicle _reservationVehicle;
        
        public UpdateReservationVehicleCommand(ReservationVehicle resVehic)
        {
            _reservationVehicle = resVehic;
        }

        /// <summary>
        /// Actualiza la información de la reservación del vehículo
        /// </summary>
        /// <exception cref="ReservationHasNoCheckInException">Se retorna cuando la reservación no tiene fecha de checkin</exception>
        /// <exception cref="ReservationHasNoCheckOutException">Se retorna cuando la reservación no tiene fecha de checkout</exception>
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

        /// <summary>
        ///  Retorna la reservación actualizada
        /// </summary>
        /// <returns>Reservación actualizada</returns>
        public ReservationVehicle GetResult()
        {
            return _reservationVehicle;
        }
    }
}