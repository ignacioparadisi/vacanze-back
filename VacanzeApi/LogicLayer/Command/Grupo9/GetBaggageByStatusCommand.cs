using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo9
{
    public class GetBaggageByStatusCommand : CommandResult<List<Baggage>>
    {
        private readonly string _status;
        private List<Baggage> _result;

        public GetBaggageByStatusCommand(string status)
        {
            _status = status;
        }
        
        public List<Baggage> GetResult()
        {
            return _result;
        }

        public void Execute()
        {
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            _result = daoFactory.GetBaggageDao().GetByStatus(_status);
        }
    }
}