using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo13
{
    public class AddReservationRoomCommand : CommandResult<ReservationRoom>
    {
        private ReservationRoom _reservationRoom;

        public AddReservationRoomCommand(ReservationRoom resRoomDto)
        {
            _reservationRoom = resRoomDto;
        }

        public void Execute()
        {
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var reservationRoomDao = (PostgresReservationRoomDAO) daoFactory.GetReservationRoomDAO();
            _reservationRoom = reservationRoomDao.Add(_reservationRoom);
        }

        public ReservationRoom GetResult()
        {
            return _reservationRoom;
        }
    }
}