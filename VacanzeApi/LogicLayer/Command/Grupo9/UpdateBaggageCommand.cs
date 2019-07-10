using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo9;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo9
{
    public class UpdateBaggageCommand : CommandResult<BaggageDTO>
    {
        private readonly int _idToSearch;
        private BaggageDTO _fieldsToUpdate;
        private BaggageDTO _result;

        public UpdateBaggageCommand(int idToSearch, BaggageDTO fieldsToUpdate)
        {
            _idToSearch = idToSearch;
            _fieldsToUpdate = fieldsToUpdate;
        }
        
        public BaggageDTO GetResult()
        {
            return _result;
        }

        public void Execute()
        {
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var baggageDao = daoFactory.GetBaggageDao();
            var mapper = MapperFactory.CreateBaggageMapper();
            var entity = mapper.CreateEntity(_fieldsToUpdate);
            CommandFactory.CreateValidateBaggageUpdateCommand(entity).Execute();
            baggageDao.Update(_idToSearch, entity);
        }
    }
}