using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo6;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo6
{

	public class UpdateHotelCommand : CommandResult<Hotel>
	{
		private int _id;
		private Hotel _hotel;
		/// <summary>
    	///     Metodo para obtener la id y e hotel a  actualizar.
    	/// </summary>
    	/// <param name="id">ID del hotel a actualizar</param>
    	/// <param name="newData">
    	///     Objeto Hotel de donde se obtendran los datos para la actualizacion.
    	///     Este objeto debe pasar la validacion del metodo <see cref="Hotel.Validate()" />
    	///     de manera que si solo se quiere actualizar un par de campos, igual se debe que mandar
    	///     el objeto completo y valido, sin cambiar los atributos que no quieras actualizar.
    	/// </param>
		public UpdateHotelCommand(int id,Hotel hotel)
		{
			_id = id;
			_hotel = hotel;
		}
   		/// <summary>
    	///     Metodo para ejecutar el actualizar los datos de un hotel.
    	/// </summary>
    	/// <exception cref="RequiredAttributeException">Algun atributo requerido estaba como null</exception>
    	/// <exception cref="InvalidAttributeException">Algun atributo tenia un valor invalido</exception>
    	/// <exception cref="DatabaseException">
    	///     Lanzada si ocurre un fallo al ejecutar la funcion en la bse de
    	///     datos
    	/// </exception>
		public void Execute()
		{
			DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
			HotelDAO HotelDao = factory.GetHotelDAO();
			HotelDao.UpdateHotel(_id, _hotel);
			_hotel = HotelDao.GetHotelById(_id);
		}
   		/// <summary>
    	///     Metodo para retornas los datos de un hotel.
   	 	/// </summary>
    	/// <returns>Objeto Hotel con los campos actualizados</returns>
		public Hotel GetResult()
		{
			return _hotel;
		}
	}
}