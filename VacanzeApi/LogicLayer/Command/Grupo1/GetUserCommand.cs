using vacanze_back.VacanzeApi.Common.Entities.Grupo1;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo1;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo1 {
    public class GetUserCommand : CommandResult<Login> {
        private Login _login;

        private string _email;

        private string _password;

        /// <summary>
        ///     Metodo pare recibir los datos del usuario a loguear.
        /// </summary>
        /// <param name="loginE">Email y clave unico del usuario almacenado en la base de datos</param>

        public GetUserCommand(Login loginE){
            _email = loginE.email;
            _password = loginE.password;
        }

        /// <summary>
        /// Metodo de Command para ejecutar el sessionLogin del DAO
        /// </summary>
        /// <exception cref="LoginUserNotFoundException">Lanzada si la consulta no retorna nada </exception>
        /// <exception cref="DatabaseException">Lanzada si ocurre un fallo al ejecutar la funcion en la base de datos </exception>
        
        public void Execute(){
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            LoginDAO LoginDAO = factory.GetLoginDAO();
            _login = LoginDAO.SessionLogin(_email,_password);        
        }

        /// <summary>
        ///     Metodo de CommandResult para retornar los datos.
        /// </summary>
        /// <returns>
        ///     Lista de los datos del usuario, los cuales son: ID, nombre del usuario, apellido del usuario
        ///     el nombre del ID y el ID de la n-n user_role
        /// </returns>

        public Login GetResult(){
            return _login;
        }
    }

}