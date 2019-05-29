using System;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo7
{
    public static class RestaurantRepository
    {
        public static long AddRestaurant(Restaurant restaurant)
        {
            
            var table = PgConnection.Instance.ExecuteFunction(
                "addrestaurant(@name,@capacity,@isActive,@qualify,@specialty,@price,@businessName,@picture,@description,@phone,@location,@address)",
                restaurant.Name, restaurant.Capacity, restaurant.IsActive, restaurant.Qualify, restaurant.Specialty, restaurant.Price, restaurant.BusinessName, 
                restaurant.Picture, restaurant.Description, restaurant.Phone, restaurant.Location, restaurant.Address
            );

            var savedId = Convert.ToInt64(table.Rows[0][0]);
            return savedId;
        }

        public static List<Restaurant> GetRestaurants()
        {
            var table = PgConnection.Instance.ExecuteFunction("getrestaurants()");
            var restaurantList = new List<Restaurant>();
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var id = Convert.ToInt64(table.Rows[i][0]);
                var name = table.Rows[i][1].ToString();
                var capacity = Convert.ToInt32(table.Rows[i][2]); 
                var isActive = Convert.ToBoolean(table.Rows[i][3]);
                var qualify = Convert.ToDecimal(table.Rows[i][4]);
                var specialty = table.Rows[i][5].ToString();
                var price = Convert.ToDecimal(table.Rows[i][6]);
                var businessName = table.Rows[i][7].ToString();
                var picture = table.Rows[i][8].ToString();
                var description = table.Rows[i][9].ToString();
                var phone = table.Rows[i][10].ToString();
                var location = Convert.ToInt32(table.Rows[i][11]);
                var address = table.Rows[i][12].ToString();
                var restaurant = new Restaurant(id, name, capacity, isActive, qualify, specialty, price, businessName, picture, description, phone, location, address);
                restaurantList.Add(restaurant);
            }

            return restaurantList; 
        }
    }
}