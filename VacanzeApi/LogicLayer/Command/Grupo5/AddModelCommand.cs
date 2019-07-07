using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo5;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5 
{
    public class AddModelCommand : CommandResult<int>
    {
        private int _id;
        public int Id { get { return _id; } set{ _id = value; } }

        private Model _model;
        public Model Model { get{ return _model; } set{ _model = value; } }

        public AddModelCommand(Model model){
            this.Model = model;
        }

        public void Execute(){
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            IModelDAO modelDAO = daoFactory.GetModelDAO();
            _id = modelDAO.AddModel(_model);
        }

        public int GetResult () {
            return _id;
        }
    }
}