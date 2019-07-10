using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo13
{
    public class GetReservationRoomCommand : CommandResult<ReservationRoom>
    {
        private int _id;
        private ReservationRoom _reservationRoom;

        public GetReservationRoomCommand(int id)
        {
            _id = id;
        }

        public void Execute()
        {
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var roomDao = (PostgresReservationRoomDAO) daoFactory.GetReservationRoomDAO();
            _reservationRoom = roomDao.Find(_id);
        }
        
        public ReservationRoom GetResult()
        {
            return _reservationRoom;
        }
    }
}