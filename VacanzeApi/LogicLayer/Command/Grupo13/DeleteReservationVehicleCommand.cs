using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo13
{
    public class DeleteReservationVehicleCommand: CommandResult<int>
    {
        private int _id;
        private int _result;

        public DeleteReservationVehicleCommand(int id)
        {
            _id = id;
        }

        public void Execute()
        {
            var getReservationCommand = CommandFactory.CreateFindReservationVehicleCommand(_id);
            getReservationCommand.Execute();
            var dao = DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetReservationVehicleDAO();
            _result = dao.Delete(_id);
        }

        public int GetResult()
        {
            return _result;
        }
    }
}