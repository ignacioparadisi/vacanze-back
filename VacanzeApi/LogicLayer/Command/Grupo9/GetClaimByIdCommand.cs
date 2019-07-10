using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo9;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo9
{
    public class GetClaimByIdCommand : CommandResult<ClaimDto>
    {
        private readonly int _idToSearch;
        private ClaimDto _result;

        public GetClaimByIdCommand(int idToSearch)
        {
            _idToSearch = idToSearch;
        }

        public void Execute()
        {
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var mapper = MapperFactory.CreateClaimMapper();
            var entity = daoFactory.GetClaimDao().GetById(_idToSearch);
            _result = mapper.CreateDTO(entity);
        }

        public ClaimDto GetResult()
        {
            // TODO: Tal vez se deberia verificar si es null?
            return _result;
        }
    }
}