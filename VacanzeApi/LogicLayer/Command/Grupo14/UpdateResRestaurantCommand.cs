using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo14;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo14
{
    public class UpdateResRestaurantCommand : Command
    {
        private readonly int idPay;
        private reservationRestaurant restId;
        private string _result;

        public UpdateResRestaurantCommand(int idPay, reservationRestaurant restId)
        {
            this.idPay = idPay;
            this.restId = restId;
        }

        public string GetResult()
        {
            return _result;
        }

        public void Execute()
        {
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            _result = daoFactory.GetReservationRestaurantDAO().updateResRestaurant(idPay, restId.rest_id);
        }
    }
}
