using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo13
{
    public class DeleteReservationRoomCommand : CommandResult<int>
    {
        private int _id;
        private int _result;

        public DeleteReservationRoomCommand(int id)
        {
            _id = id;
        }

        public void Execute()
        {
            var getReservationCommand = CommandFactory.CreateGetReservationRoomCommand(_id);
            getReservationCommand.Execute();
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var resRoomDao = (PostgresReservationRoomDAO) daoFactory.GetReservationRoomDAO();
            _result = resRoomDao.Delete(_id);
        }

        public int GetResult()
        {
            return _result;
        }
    }
}