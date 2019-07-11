using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo9;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo9
{
    public class AddClaimCommand : CommandResult<int>
    {
        private readonly ClaimDto _claimDto;

        public AddClaimCommand(ClaimDto claimDto)
        {
            _claimDto = claimDto;
        }

        public void Execute()
        {
            var mapper = MapperFactory.CreateClaimMapper();
            var claim = mapper.CreateEntity(_claimDto);
            CommandFactory.CreateValidateClaimCreationCommand(claim).Execute();
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var savedId = daoFactory.GetClaimDao().Add(claim);
            _claimDto.Id = savedId;
        }

        public int GetResult()
        {
            return _claimDto.Id;
        }
    }
}