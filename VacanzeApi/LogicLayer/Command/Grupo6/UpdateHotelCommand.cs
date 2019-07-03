using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo6;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo6
{

	public class UpdateHotelCommand : Command, CommandResult<Hotel>
	{
		private int _id;
		private Hotel _hotel;
		public int Id { get { return _id; } set { _id = value; } }
		public UpdateHotelCommand(int id,Hotel hotel)
		{
			_id = id;
			_hotel = hotel;
		}

		public void Execute()
		{
			DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
			HotelDAO HotelDao = factory.GetHotelDAO();
			HotelDao.UpdateHotel(_id, _hotel);
			_hotel = HotelDao.GetHotelById(_id);
		}

		public Hotel GetResult()
		{
			Console.WriteLine("estoy aqui");
			Console.WriteLine(_hotel.Name);
			Console.WriteLine(_hotel.Location.Id);
			return _hotel;
		}
	}
}