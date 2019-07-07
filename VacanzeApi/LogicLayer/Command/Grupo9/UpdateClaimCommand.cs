using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo9
{
    public class UpdateClaimCommand : Command
    {
        private readonly Claim _fieldsToUpdate;
        private readonly int _id;

        public UpdateClaimCommand(int id, Claim fieldsToUpdate)
        {
            _id = id;
            _fieldsToUpdate = fieldsToUpdate;
        }

        public void Execute()
        {
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var claimDao = daoFactory.GetClaimDao();
            CommandFactory.CreateValidateClaimUpdateCommand(_fieldsToUpdate).Execute();
            claimDao.Update(_id, _fieldsToUpdate);
        }
    }
}