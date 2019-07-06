using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo6;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo6
{

	public class GetHotelsCommand : Command, CommandResult<List<Hotel>>
	{
		private List<Hotel> _hotels;
		public GetHotelsCommand()
		{
		}
    	/// <summary>
    	///     Metodo para ejecutar la orden de obtener todos los hoteles guardados.
    	/// </summary>
    	/// <exception cref="DatabaseException">
    	///     Lanzada si ocurre un fallo al ejecutar la funcion en la bse de
    	///     datos
    	/// </exception>
		public void Execute()
		{
			DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
			HotelDAO HotelDao = factory.GetHotelDAO();
			_hotels = HotelDao.GetHotels();
		}
    	/// <summary>
    	///     Metodo para obtener todos los hoteles guardados.
    	/// </summary>
    	/// <returns>Lista de hoteles</returns>
		public List<Hotel> GetResult()
		{
			return _hotels;
		}
	}
}