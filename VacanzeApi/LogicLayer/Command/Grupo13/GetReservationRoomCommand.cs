using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo13
{
    public class GetReservationRoomCommand : CommandResult<ReservationRoom>
    {
        private int Id;
        private ReservationRoom Res_Room;

        public GetReservationRoomCommand(int id)
        {
            Id = id;
        }

        public void Execute()
        {
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var res_roomDao = (PostgresReservationRoomDAO) daoFactory.GetReservationRoomDAO();
            Res_Room = res_roomDao.Find(Id);
        }
        
        public ReservationRoom GetResult()
        {
            return this.Res_Room;
        }
    }
}