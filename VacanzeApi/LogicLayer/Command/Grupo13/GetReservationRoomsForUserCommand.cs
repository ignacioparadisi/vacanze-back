using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo13
{
    public class GetReservationRoomsForUserCommand: CommandResult<List<ReservationRoom>>
    {
        private int _id;
        private List<ReservationRoom> _result;

        public GetReservationRoomsForUserCommand(int id)
        {
            _id = id;
        }
        public void Execute()
        {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            IReservationRoomDAO reservationRoomDao = factory.GetReservationRoomDAO();
            _result = reservationRoomDao.GetAllByUserId(_id);
        }

        public List<ReservationRoom> GetResult()
        {
            return _result;
        }
    }
}