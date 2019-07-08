using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo5;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5 
{
    public class GetModelByIdCommand : CommandResult<Model> 
    {
        private int _modelId;

        private Model _model;
        public Model Model { get{ return _model; } set{ _model = value; } }


        public GetModelByIdCommand(int _modelId){
            this._modelId = _modelId;
        }

        public void Execute(){
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            IModelDAO modelDAO = daoFactory.GetModelDAO();
            _model = modelDAO.GetModelById(_modelId);
        }

        public Model GetResult(){
            return _model;
        }
    }
}