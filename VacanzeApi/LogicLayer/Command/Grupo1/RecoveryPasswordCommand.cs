using vacanze_back.VacanzeApi.Common.Entities.Grupo1;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo1;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo1 {
    public class RecoveryPasswordCommand : CommandResult<Login>
    {
        private Login _newPassword; 

        private string _email;


        /// <summary>
        ///     Metodo para recibir el email para el cambio de la contraseña.
        /// </summary>
        /// <param name="loginE">Email unico del usuario almacenado en la base de datos</param>
        
        public RecoveryPasswordCommand(Login loginE){
            _email = loginE.email;
        }


        /// <summary>
        ///     Metodo de Command para ejecutar el Recovery del DAO.
        /// </summary>
        /// <exception cref="PasswordRecoveryException">Lanzada si la consulta no retorna nada </exception>
        /// <exception cref="DatabaseException">Lanzada si ocurre un fallo al ejecutar la funcion en la base de datos </exception>

        public void Execute()
        {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            LoginDAO LoginDAO = factory.GetLoginDAO();
            _newPassword = LoginDAO.Recovery(_email);
        }


        /// <summary>
        ///     Metodo de CommandResult para retornar los datos.
        /// </summary>
        /// <returns>
        /// <returns>
        ///     Lista de los datos del usuario, los cuales son: nombre del usuario, apellido del usuario,
        ///     email del ususario y la nueva contraseña.
        /// </returns>
        public Login GetResult(){
            return _newPassword;
        }
    }
}