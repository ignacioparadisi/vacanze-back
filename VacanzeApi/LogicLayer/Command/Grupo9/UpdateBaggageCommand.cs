using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo9
{
    public class UpdateBaggageCommand : CommandResult<Baggage>
    {
        private readonly int _idToSearch;
        private Baggage _result;

        public UpdateBaggageCommand(int idToSearch)
        {
            this._idToSearch = idToSearch;
        }
        
        public Baggage GetResult()
        {
            return _result;
        }

        public void Execute()
        {
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            _result = daoFactory.GetBaggageDao().GetById(_idToSearch);
        }
    }
}