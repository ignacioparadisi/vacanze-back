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
		public int Id { get { return _id; } set { _id = value; } }
		public GetHotelImageCommand(int id)
		{
			_id = id;
		}

		public void Execute()
		{
			DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
			HotelDAO HotelDao = factory.GetHotelDAO();
			_image = HotelDao.GetHotelImage(_id);
		}
		public String GetResult()
		{
			return _image;
		}
	}
}