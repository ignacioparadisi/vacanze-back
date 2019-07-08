using DefaultNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities.Grupo14;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo14;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo14;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo14
{
    public class AddResRestaurantCommand : Command
    {
        private int _id;
        private Restaurant_res _resRestaurant;
        public int Id { get { return _id; } set { _id = value; } }
        public Restaurant_res ResRestaurant { get{ return _resRestaurant; } set{ _resRestaurant = value; } }

        public AddResRestaurantCommand(Restaurant_res _resRestaurant)
        {
            this._resRestaurant = _resRestaurant;
        }

        public void Execute()
        {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            IReservationRestaurantDAO ResRestDAO = factory.GetReservationRestaurantDAO();
            ResRestDAO.addReservation(_resRestaurant);            
        }
        public int GetResult()
        {
            return _id;
        }
    }
}
