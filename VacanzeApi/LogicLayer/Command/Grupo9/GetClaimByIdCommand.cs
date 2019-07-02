using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo9
{
    public class GetClaimByIdCommand : CommandResult<Claim>
    {
        private readonly int _idToSearch;
        private Claim _result;

        public GetClaimByIdCommand(int idToSearch)
        {
            _idToSearch = idToSearch;
        }

        public void Execute()
        {
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            _result = daoFactory.GetClaimDao().GetById(_idToSearch);
        }

        public Claim GetResult()
        {
            // TODO: Tal vez se deberia verificar si es null?
            return _result;
        }
    }
}