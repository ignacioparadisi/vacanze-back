using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo9
{
    public class AddClaimCommand : CommandResult<int>
    {
        private readonly Claim _claim;

        public AddClaimCommand(Claim claim)
        {
            _claim = claim;
        }

        public void Execute()
        {
            CommandFactory.CreateValidateClaimCreationCommand(_claim).Execute();
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var savedId = daoFactory.GetClaimDao().Add(_claim);
            _claim.Id = savedId;
        }

        public int GetResult()
        {
            return _claim.Id;
        }
    }
}