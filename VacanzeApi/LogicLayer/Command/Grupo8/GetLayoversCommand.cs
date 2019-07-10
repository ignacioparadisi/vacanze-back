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
    public class GetLayoversCommand: CommandResult<List<LayoverDTO>>
    {
        private List<LayoverDTO> _layoverDtoList;
        private int _id;

        public GetLayoversCommand(int id)
        {
            _id = id;
        }
        public void Execute()
        {
            _layoverDtoList = new List<LayoverDTO>();
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            PostgresCruiserDAO cruiserDao = (PostgresCruiserDAO) daoFactory.GetCruiserDAO();
            LayoverMapper layoverMapper = MapperFactory.CreateLayoverMapper();
            _layoverDtoList = layoverMapper.CreateDTOList(cruiserDao.GetLayovers(_id));
        }
        public List<LayoverDTO> GetResult()
        {
            return _layoverDtoList;
        }
    }
}