using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo6;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo6
{

	public class DeleteHotelCommand : Command {
		private int _id;
    	/// <summary>
    	///     Metodo para obtener la id de un hotel para eliminarlo permanentemente.
    	/// </summary>
    	/// <param name="id">ID del hotel a eliminar</param>
		public DeleteHotelCommand(int id)
		{
			_id = id;
		}
    	/// <summary>
    	///     Metodo para ejecutar el eliminar un hotel permanentemente.
    	/// </summary>
    	/// <exception cref="DatabaseException">
   	 	///     Lanzada si ocurre un fallo al ejecutar la funcion en la bse de
    	///     datos
    	/// </exception>
		public void Execute()
		{
			DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
			HotelDAO HotelDao = factory.GetHotelDAO();
			HotelDao.DeleteHotel(_id);
		}

	}
}