using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo5;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5 
{
    public class GetModelsCommand : Command, CommandResult<List<Model>> 
    {
        private List<Model> models;

        public GetModelsCommand(){}

        public void Execute(){
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            IModelDAO modelDAO = daoFactory.GetModelDAO();
            models = modelDAO.GetModels();
        }

        public List<Model> GetResult(){
            return models;
        }
    }
}