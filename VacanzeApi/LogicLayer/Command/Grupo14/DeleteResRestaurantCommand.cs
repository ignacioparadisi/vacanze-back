using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo14;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo14
{
    public class DeleteResRestaurantCommand : Command
    {
        private int _id;
        public int Id { get { return _id; } set { _id = value; } }
        public DeleteResRestaurantCommand(int id)
        {
            _id = id;
        }

        public void Execute()
        {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            IReservationRestaurantDAO ResRestDAO = factory.GetReservationRestaurantDAO();
            ResRestDAO.deleteResRestaurant(_id);
        }
    }
}
