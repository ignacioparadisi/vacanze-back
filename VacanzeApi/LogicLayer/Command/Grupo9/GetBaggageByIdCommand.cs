using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo9;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo9
{
    public class GetBaggageByIdCommand : CommandResult<BaggageDTO>
    {
        private readonly int _id;
        private BaggageDTO _result;

        public GetBaggageByIdCommand(int id)
        {
            _id = id;
        }

        public BaggageDTO GetResult()
        {
            return _result;
        }

        public void Execute()
        {
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var mapper = MapperFactory.CreateBaggageMapper();
            var entity = daoFactory.GetBaggageDao().GetById(_id);
            _result = mapper.CreateDTO(entity);
        }
    }
}