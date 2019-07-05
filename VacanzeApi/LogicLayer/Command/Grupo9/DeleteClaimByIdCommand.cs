using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo9
{
    public class DeleteClaimByIdCommand : Command
    {
        private readonly int _id;

        public DeleteClaimByIdCommand(int id)
        {
            _id = id;
        }

        public void Execute()
        {
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var claimDao = daoFactory.GetClaimDao();
            claimDao.Delete(_id);
        }
    }
}