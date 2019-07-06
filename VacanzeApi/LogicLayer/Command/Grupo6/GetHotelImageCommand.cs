using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo6;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo6
{

	public class GetHotelImageCommand : Command, CommandResult<String>
	{
		private int _id;
		private String _image;
    	/// <summary>
    	///     etodo para obtener el id de un hotel 
    	/// </summary>
    	/// <param name="id">Id del hotel del que se quiere su imagen</param>
		public GetHotelImageCommand(int id)
		{
			_id = id;
		}
    	/// <summary>
    	///     ejecuta la accion de la imagen en base64 del hotel.
    	/// </summary>
    	/// <exception cref="DatabaseException">
    	///     Lanzada si ocurre un fallo al ejecutar la funcion en la bse de
    	///     datos
    	/// </exception>
		public void Execute()
		{
			DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
			HotelDAO HotelDao = factory.GetHotelDAO();
			_image = HotelDao.GetHotelImage(_id);
		}
		/// <summary>
    	///     Devuelve la imagen en base64 del hotel.
    	/// </summary>
    	/// <returns>Imagen en base64</returns>
		public String GetResult()
		{
			return _image;
		}
	}
}