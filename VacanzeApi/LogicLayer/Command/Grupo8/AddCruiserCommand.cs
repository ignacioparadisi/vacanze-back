using DefaultNamespace;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo8;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo8
{
    public class AddCruiserCommand
    {
        private CruiserDTO _cruiserDTO;
        
        public AddCruiserCommand(CruiserDTO cruiserDTO)
        {
            _cruiserDTO = cruiserDTO;
        }
        public void Execute()
        {
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            PostgresCruiserDAO cruiserDao = (PostgresCruiserDAO) daoFactory.GetCruiserDAO();
            CruiserMapper cruiserMapper = MapperFactory.CreateCruiserMapper();
            Cruiser cruiser = (Cruiser) cruiserMapper.CreateEntity(_cruiserDTO);
            _cruiserDTO.Id = cruiserDao.AddCruiser(cruiser);
        }
        public CruiserDTO GetResult()
        {
            return _cruiserDTO;
        }
    }
}