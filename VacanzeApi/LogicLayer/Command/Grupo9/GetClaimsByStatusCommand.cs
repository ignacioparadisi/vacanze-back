using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo9
{
    public class GetClaimsByStatusCommand : CommandResult<List<Claim>>
    {
        private readonly string _status;
        private List<Claim> _result;

        public GetClaimsByStatusCommand(string status)
        {
            _status = status;
        }

        public void Execute()
        {
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            _result = daoFactory.GetClaimDao().GetByStatus(_status);
        }

        public List<Claim> GetResult()
        {
            return _result;
        }
    }
}