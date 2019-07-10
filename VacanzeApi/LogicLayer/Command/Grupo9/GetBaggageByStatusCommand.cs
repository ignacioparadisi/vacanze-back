using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo9;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo9
{
    public class GetBaggageByStatusCommand : CommandResult<List<BaggageDTO>>
    {
        private readonly string _status;
        private List<BaggageDTO> _result;

        public GetBaggageByStatusCommand(string status)
        {
            _status = status;
        }
        
        public List<BaggageDTO> GetResult()
        {
            return _result;
        }

        public void Execute()
        {
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var entity = daoFactory.GetBaggageDao().GetByStatus(_status);
            var mapper = MapperFactory.CreateBaggageMapper();
            _result = mapper.CreateDTOList(entity);
        }
    }
}