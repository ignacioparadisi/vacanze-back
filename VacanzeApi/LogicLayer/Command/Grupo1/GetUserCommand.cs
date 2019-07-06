using vacanze_back.VacanzeApi.Common.Entities.Grupo1;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo1;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo1 {
    public class GetUserCommand : Command, CommandResult<Login> {
        private Login _login;

        private string _email;

        private string _password;

        public GetUserCommand(Login loginE){
            _email = loginE.email;
            _password = loginE.password;
        }

        public void Execute(){
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            LoginDAO LoginDAO = factory.GetLoginDAO();
            _login = LoginDAO.SessionLogin(_email,_password);        
        }

        public Login GetResult(){
            return _login;
        }
    }

}