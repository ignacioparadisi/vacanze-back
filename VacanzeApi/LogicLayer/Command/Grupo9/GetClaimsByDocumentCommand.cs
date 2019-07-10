using System.Collections.Generic;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo9;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo9
{
    public class GetClaimsByDocumentCommand : CommandResult<List<ClaimDto>>
    {
        private readonly string _document;
        private List<ClaimDto> _result;

        public GetClaimsByDocumentCommand(string document)
        {
            _document = document;
        }

        public void Execute()
        {
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var mapper = MapperFactory.CreateClaimMapper();
            var entityList = daoFactory.GetClaimDao().GetByDocument(_document);
            _result = mapper.CreateDTOList(entityList);
        }

        public List<ClaimDto> GetResult()
        {
            return _result;
        }
    }
}