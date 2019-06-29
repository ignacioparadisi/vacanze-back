using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;

namespace DefaultNamespace
{
    public interface IRestaurantDAO
    {
        List<Restaurant> GetRestaurants();
        Restaurant GetRestaurant(int id);
        List<Restaurant> GetRestaurantsByCity(int location_id);
        int AddRestaurant(Restaurant restaurant);
        Restaurant UpdateRestaurant(Restaurant restaurant);
        int DeleteRestaurant(int id);
    }
}