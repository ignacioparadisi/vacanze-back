using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo9
{
    public class AddClaimCommand : Command
    {
        private readonly Claim _claim;

        public AddClaimCommand(Claim claim)
        {
            _claim = claim;
        }

        public void Execute()
        {
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            daoFactory.GetClaimDao().Add(_claim);
        }
    }
}