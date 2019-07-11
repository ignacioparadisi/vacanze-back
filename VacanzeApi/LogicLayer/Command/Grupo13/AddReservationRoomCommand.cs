using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo13
{
    public class AddReservationRoomCommand : CommandResult<ReservationRoom>
    {
        /// <summary>
        /// Reservación de habitación guardada en la base de datos
        /// </summary>
        private ReservationRoom _reservationRoom;

        /// <summary>
        /// Constructor del Comando
        /// </summary>
        /// <param name="resRoom">La reservación de habitación que va a ser guardada en base de datos</param>
        public AddReservationRoomCommand(ReservationRoom resRoom)
        {
            _reservationRoom = resRoom;
        }

        /// <summary>
        /// Crea un DAO de Reservación de Habitación y ejecuta la función de Add para añadir la reservación
        /// a la base de datos
        /// </summary>
        /// <exception cref="ReservationHasNoUserException">Se retorna cuando el id del usuario es menor o igual a 0</exception>
        /// <exception cref="ReservationHasNoHotelException">Se retorna cuando el ID del hotel es menor o igual a 0</exception>
        /// <exception cref="ReservationHasNoCheckInException">Se retorna cuando no se recibe fecha de checkin</exception>
        /// <exception cref="ReservationHasNoCheckOutException">Se retorna cuando no se recibe fecha de chekout</exception>
        public void Execute()
        {
            if (_reservationRoom.UserId <= 0)
                throw new ReservationHasNoUserException();
            if (_reservationRoom.HotelId <= 0)
                throw new ReservationHasNoHotelException();
            if (_reservationRoom.CheckIn.Equals(DateTime.MinValue))
                throw new ReservationHasNoCheckInException();
            if (_reservationRoom.CheckOut.Equals(DateTime.MinValue))
                throw new ReservationHasNoCheckOutException();
            
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var reservationRoomDao = (PostgresReservationRoomDAO) daoFactory.GetReservationRoomDAO();
            _reservationRoom = reservationRoomDao.Add(_reservationRoom);
        }

        /// <summary>
        /// Funcion que retorna la reservacion de habitacion hecha
        /// </summary>
        /// <returns>Reservación de hotel registrada</returns>
        public ReservationRoom GetResult()
        {
            return _reservationRoom;
        }
    }
}