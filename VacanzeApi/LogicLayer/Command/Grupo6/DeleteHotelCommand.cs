using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo6;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo6
{

	public class DeleteHotelCommand : Command {
		private int _id;
		public int Id { get { return _id; } set { _id = value; } }
		public DeleteHotelCommand(int id)
		{
			_id = id;
		}

		public void Execute()
		{
			DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
			HotelDAO HotelDao = factory.GetHotelDAO();
			HotelDao.DeleteHotel(_id);
		}

	}
}