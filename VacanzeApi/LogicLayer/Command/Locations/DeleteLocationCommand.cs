using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Locations;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Locations
{

	public class DeleteLocationCommand : Command {
		private int _id;
        /// <summary>
        ///     Metodo para btene la id de una ubicacion
        /// </summary>
        /// <param name="id">ID del objeto location a eliminar</param>
		public DeleteLocationCommand(int id)
		{
			_id = id;
		}
        /// <summary>
        ///     Metodo para ejecutar el elimnar una ubicacion
        /// </summary>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la base de
        ///     datos
        /// </exception>
		public void Execute()
		{
			DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            LocationDAO locationDao = factory.GetLocationDAO();
			locationDao.DeleteLocation(_id);
		}

	}
}