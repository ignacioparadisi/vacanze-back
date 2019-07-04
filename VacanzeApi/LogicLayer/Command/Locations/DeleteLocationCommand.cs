using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Locations;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Locations
{

	public class DeleteLocationCommand : Command {
		private int _id;
		public DeleteLocationCommand(int id)
		{
			_id = id;
		}

		public void Execute()
		{
			DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            LocationDAO locationDao = factory.GetLocationDAO();
			locationDao.DeleteLocation(_id);
		}

	}
}