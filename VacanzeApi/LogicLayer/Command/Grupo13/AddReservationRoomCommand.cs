using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo13
{
    public class AddReservationRoomCommand : CommandResult<ReservationRoom>
    {
        private ReservationRoom Res_Room;

        public AddReservationRoomCommand(ReservationRoom resRoomDto)
        {
            this.Res_Room = resRoomDto;
        }

        public void Execute()
        {
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var res_roomDao = (PostgresReservationRoomDAO) daoFactory.GetReservationRoomDAO();
            this.Res_Room.Id = res_roomDao.Add(this.Res_Room);
        }

        public ReservationRoom GetResult()
        {
            return this.Res_Room;
        }
    }
}