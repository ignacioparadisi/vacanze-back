using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo6;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo6
{

	public class GetHotelsCommand : Command, CommandResult<Hotel>
	{
		private String _image;
		private List<Hotel> _hotels;
		public GetHotelsCommand()
		{

		}

		public void Execute()
		{
			DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
			HotelDAO HotelDao = factory.GetHotelDAO();
			_hotels = HotelDao.GetHotels();
		}
		public String GetResult()
		{
			return _hotels;
		}
	}
}