using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo6;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo6
{

	public class GetHotelsByCityCommand : Command, CommandResult<Hotel>
	{
		private int _city;
		private List<Hotel> _hotels;
		public GetHotelsByCityCommand(int city)
		{
			_city = city;
		}

		public void Execute()
		{
			DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
			HotelDAO HotelDao = factory.GetHotelDAO();
			_hotels = HotelDao.GetHotelsByCity(_city);
		}
		public String GetResult()
		{
			return _hotels;
		}
	}
}