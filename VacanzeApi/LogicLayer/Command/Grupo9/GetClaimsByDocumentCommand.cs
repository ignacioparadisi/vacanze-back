using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo9
{
    public class GetClaimsByDocumentCommand : CommandResult<List<Claim>>
    {
        private readonly string _document;
        private List<Claim> _result;

        public GetClaimsByDocumentCommand(string document)
        {
            _document = document;
        }

        public void Execute()
        {
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            _result = daoFactory.GetClaimDao().GetByDocument(_document);
        }

        public List<Claim> GetResult()
        {
            return _result;
        }
    }
}