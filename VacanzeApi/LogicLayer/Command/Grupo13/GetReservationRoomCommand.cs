using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo13
{
    public class GetReservationRoomCommand : CommandResult<ReservationRoom>
    {
        /// <summary>
        /// ID de la reserva de habitación a ser buscada
        /// </summary>
        private int _id;
        /// <summary>
        /// Reserva de habitación buscada
        /// </summary>
        private ReservationRoom _reservationRoom;

        public GetReservationRoomCommand(int id)
        {
            _id = id;
        }

        /// <summary>
        /// Busca una reservación de habitación por id mediante el DAO de reservación de vehículos
        /// </summary>
        public void Execute()
        {
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var roomDao = (PostgresReservationRoomDAO) daoFactory.GetReservationRoomDAO();
            _reservationRoom = roomDao.Find(_id);
        }
        
        /// <summary>
        /// Retorna la reservación de habitación buscada
        /// </summary>
        /// <returns>Reservación de habitación buscada</returns>
        public ReservationRoom GetResult()
        {
            return _reservationRoom;
        }
    }
}