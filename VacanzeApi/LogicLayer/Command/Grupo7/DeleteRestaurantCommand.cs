
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo7;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo7
{
    public class DeleteRestaurantCommand : Command
    {
        private int _id;

        public DeleteRestaurantCommand(int id)
        {
            _id = id;
        }
        public void Execute()
        {
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            PostgresRestaurantDAO restaurantDao = (PostgresRestaurantDAO) daoFactory.GetRestaurantDAO();
            restaurantDao.DeleteRestaurant(_id);
        }
    }
}