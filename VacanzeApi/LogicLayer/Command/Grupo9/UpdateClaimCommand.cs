using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo9;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo9
{
    public class UpdateClaimCommand : Command
    {
        private readonly ClaimDto _fieldsToUpdate;
        private readonly int _id;

        public UpdateClaimCommand(int id, ClaimDto fieldsToUpdate)
        {
            _id = id;
            _fieldsToUpdate = fieldsToUpdate;
        }

        public void Execute()
        {
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var claimDao = daoFactory.GetClaimDao();
            var mapper = MapperFactory.CreateClaimMapper();
            var entity = mapper.CreateEntity(_fieldsToUpdate);
            CommandFactory.CreateValidateClaimUpdateCommand(entity).Execute();
            claimDao.Update(_id, entity);
        }
    }
}