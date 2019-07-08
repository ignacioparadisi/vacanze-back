using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo14;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo14;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo14
{
    public interface IReservationRestaurantDAO
    {
        int addReservation(Restaurant_res reserva);
        List<Restaurant_res> getResRestaurant(int Iduser);
        List<Restaurant_res> getReservationNotPay(int Iduser);
        int deleteResRestaurant(int resRestId);
        string updateResRestaurant(int payID, int resRestID);
        
    }
}
