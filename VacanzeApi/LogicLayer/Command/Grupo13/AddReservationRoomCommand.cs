using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo13
{
    public class AddReservationRoomCommand : CommandResult<ReservationRoom>
    {
        private ReservationRoom _reservationRoom;

        public AddReservationRoomCommand(ReservationRoom resRoom)
        {
            _reservationRoom = resRoom;
        }

        public void Execute()
        {
            if (_reservationRoom.UserId == 0)
                throw new ReservationHasNoUserException();
            if (_reservationRoom.HotelId == 0)
                throw new ReservationHasNoHotelException();
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