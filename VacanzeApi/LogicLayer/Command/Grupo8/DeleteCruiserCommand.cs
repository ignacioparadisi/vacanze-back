using DefaultNamespace;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo8;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo8
{
    /// <summary>  
    ///  Comando para eliminar un Restaurante
    /// </summary> 
    public class DeleteCruiserCommand : Command
    {
        private int _id;

        public DeleteCruiserCommand(int id)
        {
            _id = id;
        }
        public void Execute()
        {
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            PostgresCruiserDAO cruiserDao = (PostgresCruiserDAO) daoFactory.GetCruiserDAO();
            cruiserDao.DeleteCruiser(_id);
        }
    }
}