using System.Collections.Generic;
using DefaultNamespace;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo8;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo8
{
    public class GetCruisersCommand: CommandResult<List<CruiserDTO>>
    {
        private List<CruiserDTO> _cruiserDtoList;
        public void Execute()
        {
            _cruiserDtoList = new List<CruiserDTO>();
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            PostgresCruiserDAO cruiserDao = (PostgresCruiserDAO) daoFactory.GetCruiserDAO();
            CruiserMapper cruiserMapper = MapperFactory.CreateCruiserMapper();
            _cruiserDtoList = cruiserMapper.CreateDTOList(cruiserDao.GetCruisers());
        }
        public List<CruiserDTO> GetResult()
        {
            return _cruiserDtoList;
        }
    }
}