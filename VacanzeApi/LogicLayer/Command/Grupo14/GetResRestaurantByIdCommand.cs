using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities.Grupo14;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo14
{
    public class GetResRestaurantByIdCommand : CommandResult<List<Restaurant_res>>
    {
        private readonly int _id;
        private List<Restaurant_res> _result;

        public GetResRestaurantByIdCommand(int id)
        {
            _id = id;
        }

        public List<Restaurant_res> GetResult()
        {
            return _result;
        }
        
        public void Execute()
        {
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            _result = daoFactory.GetReservationRestaurantDAO().getResRestaurant(_id);
        }
    }
}
