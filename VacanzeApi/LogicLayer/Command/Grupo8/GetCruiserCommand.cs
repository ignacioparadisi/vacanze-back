using DefaultNamespace;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo8;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo8
{
    public class GetCruiserCommand: CommandResult<CruiserDTO>
    {
        private int _id;
        private CruiserDTO _cruiserDTO;

        public GetCruiserCommand(int id)
        {
            _id = id;
        }
        public void Execute()
        {
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            PostgresCruiserDAO cruiserDao = (PostgresCruiserDAO) daoFactory.GetCruiserDAO();
            CruiserMapper cruiserMapper = MapperFactory.CreateCruiserMapper();
            _cruiserDTO = cruiserMapper.CreateDTO(cruiserDao.GetCruiser(_id));
        }
        public CruiserDTO GetResult()
        {
            return _cruiserDTO;
        }
    }
}