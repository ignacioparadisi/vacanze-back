using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo13
{
    public class UpdateReservationRoomCommand : CommandResult<ReservationRoom>
    {
        /// <summary>
        /// Reservacion de habitación actualizada
        /// </summary>
        private ReservationRoom _reservationRoom;
        
        public UpdateReservationRoomCommand(ReservationRoom resRoom)
        {
            _reservationRoom = resRoom;
        }

        /// <summary>
        /// Actualiza la información de una reservación de habitación medienate el DAO de reservación de habitación
        /// </summary>
        public void Execute()
        {
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var reservationRoomDao = (PostgresReservationRoomDAO) daoFactory.GetReservationRoomDAO();
            _reservationRoom = reservationRoomDao.Update(_reservationRoom);
        }

        /// <summary>
        /// Retorna la reservación actualizada
        /// </summary>
        /// <returns>Reservación actualizada</returns>
        public ReservationRoom GetResult()
        {
            return _reservationRoom;
        }
    }
}