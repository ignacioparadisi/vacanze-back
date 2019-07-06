using vacanze_back.VacanzeApi.Common.Entities.Grupo1;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo1;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo1 {
    public class RecoveryPasswordCommand : Command, CommandResult<Login>
    {
        private Login _newPassword; 

        private string _email;

        public RecoveryPasswordCommand(Login loginE){
            _email = loginE.email;
        }

        public void Execute()
        {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            LoginDAO LoginDAO = factory.GetLoginDAO();
            _newPassword = LoginDAO.Recovery(_email);
        }

        public Login GetResult(){
            return _newPassword;
        }
    }
}