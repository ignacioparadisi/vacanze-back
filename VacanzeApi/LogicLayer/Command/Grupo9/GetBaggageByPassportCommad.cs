using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo9;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo9
{
    public class GetBaggageByPassportCommand : CommandResult<List<BaggageDTO>>
    {
        private readonly string _passportNumber;
        private List<BaggageDTO> _result;

        public GetBaggageByPassportCommand(string passportNumber)
        {
            this._passportNumber = passportNumber;
        }
        
        public List<BaggageDTO> GetResult()
        {
            return _result;
        }

        public void Execute()
        {
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var entity = daoFactory.GetBaggageDao().GetByPassport(_passportNumber);
            var mapper = MapperFactory.CreateBaggageMapper();
            _result = mapper.CreateDTOList(entity);
        }
    }
}