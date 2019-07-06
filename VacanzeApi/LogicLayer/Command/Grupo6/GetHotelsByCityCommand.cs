using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo6;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo6
{

	public class GetHotelsByCityCommand : Command, CommandResult<List<Hotel>>
	{
		private int _city;
		private List<Hotel> _hotels;
		/// <summary>
    	///     Metodo para obtener la id de la ciudad de los hoteles.
	    /// </summary>
    	/// <param name="city">ID del Location de donde se quiere conocer sus hoteles</param>
		public GetHotelsByCityCommand(int city)
		{
			_city = city;
		}
    	/// <summary>
    	///     Metodo para ejecutar la orden de listar hoteles por Location.
    	/// </summary>
    	/// <exception cref="DatabaseException">
    	///     Lanzada si ocurre un fallo al ejecutar la funcion en la bse de
    	///     datos
    	/// </exception>
		public void Execute()
		{
			DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
			HotelDAO HotelDao = factory.GetHotelDAO();
			_hotels = HotelDao.GetHotelsByCity(_city);
		}
		/// <summary>
    	///     Metodo para obtener listar hoteles por Location.
    	/// </summary>
		/// <returns>
    	///     Lista de hoteles ubicados en la ciudad recibida, si no hay hoteles en la ciudad, la lista
    	///     estaria vacia
    	/// </returns>
		public List<Hotel> GetResult()
		{
			return _hotels;
		}
	}
}