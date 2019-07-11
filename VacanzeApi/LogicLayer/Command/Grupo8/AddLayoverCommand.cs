using vacanze_back.VacanzeApi.Persistence.DAO.Grupo8;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo8;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo8
{
    public class AddLayoverCommand
    {
        private LayoverDTO _layoverDTO;
        
        public AddLayoverCommand(LayoverDTO layoverDTO)
        {
            _layoverDTO = layoverDTO;
        }
        public void Execute()
        {
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            PostgresCruiserDAO layoverDao = (PostgresCruiserDAO) daoFactory.GetCruiserDAO();
            LayoverMapper layoverMapper = MapperFactory.CreateLayoverMapper();
            Layover layover = (Layover) layoverMapper.CreateEntity(_layoverDTO);
            _layoverDTO = layoverMapper.CreateDTO(layoverDao.AddLayover(layover));
        }
        public LayoverDTO GetResult()
        {
            return _layoverDTO;
        }
    }
}