using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo14;


namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo14
{
    public interface ReservationRestaurantDAO
    {
        int addReservation(Restaurant_res reserva);
        List<Restaurant_res> getResRestaurant(int user);
        List<Restaurant_res> getReservationNotPay(int user);
        int deleteResRestaurant(int resRestId);
        int updateResRestaurant(int payID, int resRestID);
        
    }
}
