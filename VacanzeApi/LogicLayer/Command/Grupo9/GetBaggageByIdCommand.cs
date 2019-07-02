using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo9
{
    public class GetBaggageByIdCommand : CommandResult<Baggage>
    {
        private readonly int _idToUpdate;
        private readonly Baggage _baggage;
        private Baggage _result;

        public GetBaggageByIdCommand(int idToUpdate,Baggage baggage)
        {
            this._idToUpdate = idToUpdate;
            this._baggage = baggage;
        }
        
        public Baggage GetResult()
        {
            return _result;
        }

        public void Execute()
        {
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            _result = daoFactory.GetBaggageDao().Update(_idToUpdate,_baggage);
        }
    }
}