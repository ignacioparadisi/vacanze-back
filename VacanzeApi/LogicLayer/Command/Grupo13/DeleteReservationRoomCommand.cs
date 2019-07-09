using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo13
{
    public class DeleteReservationRoomCommand : CommandResult<int>
    {
        private ReservationRoom Res_Room;
        private int Result;

        public DeleteReservationRoomCommand(ReservationRoom resRoomDto)
        {
            Res_Room = resRoomDto;
        }

        public void Execute()
        {
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var res_roomDao = (PostgresReservationRoomDAO) daoFactory.GetReservationRoomDAO();
            Result = res_roomDao.Add(this.Res_Room);
        }

        public int GetResult()
        {
            return Result;
        }
    }
}