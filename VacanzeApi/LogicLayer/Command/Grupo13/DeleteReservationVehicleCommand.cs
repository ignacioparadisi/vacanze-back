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
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var resVehicDao = (PostgresReservationVehicleDAO) daoFactory.GetReservationVehicleDAO();
            _result = resVehicDao.Delete(_id);
        }

        public int GetResult()
        {
            return _result;
        }
    }
}