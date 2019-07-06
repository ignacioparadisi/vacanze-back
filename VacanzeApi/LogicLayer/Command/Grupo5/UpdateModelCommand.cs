using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo5;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5 
{
    public class UpdateModelCommand : Command, CommandResult<bool> {
        private bool _updated;
        public bool Updated { get{ return _updated; } set{ _updated = value; } }

        private Model _model;
        public Model Model { get{ return _model; } set{ _model = value; } }

        public UpdateModelCommand(Model model){
            this._model = model;
        }

        public void Execute(){
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            IModelDAO modelDAO = daoFactory.GetModelDAO();
            _updated = modelDAO.UpdateModel(_model);
        }

        public bool GetResult(){
            return _updated;
        }
    }
}