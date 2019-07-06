using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo9
{
    public class GetBaggageByIdCommand : CommandResult<Baggage>
    {
        private readonly int _id;
        private Baggage _result;

        public GetBaggageByIdCommand(int id)
        {
            _id = id;
        }

        public Baggage GetResult()
        {
            return _result;
        }

        public void Execute()
        {
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            _result = daoFactory.GetBaggageDao().GetById(_id);
        }
    }
}