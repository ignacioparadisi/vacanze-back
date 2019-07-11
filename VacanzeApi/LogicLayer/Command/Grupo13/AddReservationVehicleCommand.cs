using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo2;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo13
{
    public class AddReservationVehicleCommand : CommandResult<ReservationVehicle>
    {
        /// <summary>
        /// Reservación de vehiculo guadada en la base de datos
        /// </summary>
        private ReservationVehicle _reservationAutomobile;

        /// <summary>
        /// Constructor del comando
        /// </summary>
        /// <param name="_reservationAutomobile">Reservación que se guardará en la base de datos</param>
        public AddReservationVehicleCommand(ReservationVehicle _reservationAutomobile)
        {
            this._reservationAutomobile = _reservationAutomobile;
        }

        /// <summary>
        /// Retorna la reservación de vehículo guardada en la base de datos
        /// </summary>
        /// <returns>Reservación de vehículo guardada en la base de datos</returns>
        public ReservationVehicle GetResult()
        {
            return _reservationAutomobile;
        }

        /// <summary>
        /// Guarda una reservación de vehículo en la base de datos mediente el DAO de reservación de vehículo
        /// </summary>
        /// <exception cref="ReservationHasNoUserException">Se retorna cuando el ID del usaurio es menor o igual a 0</exception>
        /// <exception cref="ReservationHasNoVehicleException">Se retorna cuando el ID del vehiculo es menor o igual a 0</exception>
        /// <exception cref="ReservationHasNoCheckInException">Se retorna cuando no se recibe la fecha de checkin</exception>
        /// <exception cref="ReservationHasNoCheckOutException">Se retorna cuando no se revibe la fecha de checkout</exception>
        public void Execute()
        {
            if (_reservationAutomobile.UserId <= 0)
                throw new ReservationHasNoUserException();
            if (_reservationAutomobile.VehicleId <= 0)
                throw new ReservationHasNoVehicleException();
            if (_reservationAutomobile.CheckIn.Equals(DateTime.MinValue))
                throw new ReservationHasNoCheckInException();
            if (_reservationAutomobile.CheckOut.Equals(DateTime.MinValue))
                throw new ReservationHasNoCheckOutException();

            IReservationVehicleDAO dao = DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetReservationVehicleDAO();
            _reservationAutomobile = dao.AddReservation(_reservationAutomobile);
        }
    }
}