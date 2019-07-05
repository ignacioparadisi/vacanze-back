using System;
using System.Collections.Generic;
using System.Data;
using DefaultNamespace;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo7
{
    public class PostgresRestaurantDAO: IRestaurantDAO
    {
        
        public Restaurant GetRestaurant(int id)
        {       

                var table = PgConnection.Instance.ExecuteFunction("GetRestaurant(@restaurant_id)" , id);
                if (table.Rows.Count == 0 )
                    throw new RestaurantNotFoundExeption("No se encontro ningun restaurante con el Id " + id);
                return ExtractRestaurantFromRow(table.Rows[0]);
        }
        
        public List<Restaurant> GetRestaurants()
        {

            var table = PgConnection.Instance.ExecuteFunction("getrestaurants()");
            var restaurantList = new List<Restaurant>();
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var restaurant = ExtractRestaurantFromRow(table.Rows[i]);
                restaurantList.Add(restaurant);
            }
            return restaurantList;
        }
        
        public List<Restaurant> GetRestaurantsByCity(int locationId)
        {
            var table = PgConnection.Instance.ExecuteFunction("GetRestaurantsByCity(@location_id)", locationId);
                var restaurantList = new List<Restaurant>();
                for (var i = 0; i < table.Rows.Count; i++)
                {
                    var restaurant = ExtractRestaurantFromRow(table.Rows[i]);
                    restaurantList.Add(restaurant);
                }
            return restaurantList;
        }
        
        public int AddRestaurant(Restaurant restaurant)
        {
            var table = PgConnection.Instance.ExecuteFunction(
                    "addrestaurant(@name,@capacity,@isActive,@qualify,@specialty,@price,@businessName,@picture,@description,@phone,@location,@address)",
                    restaurant.Name, restaurant.Capacity, restaurant.IsActive, restaurant.Qualify, restaurant.Specialty, restaurant.Price, restaurant.BusinessName, 
                    restaurant.Picture, restaurant.Description, restaurant.Phone, restaurant.Location, restaurant.Address
                );
            var savedId = Convert.ToInt32(table.Rows[0][0]);
            return savedId;
        }
        
        public Restaurant UpdateRestaurant(Restaurant restaurant)
        {
            var table = PgConnection.Instance.ExecuteFunction("GetRestaurant(@restaurant_id)" , restaurant.Id);
            
            if (table.Rows.Count == 0)
                throw new RestaurantNotFoundExeption("No se encontro el Restaurante que se desea actualizar");
            
            PgConnection.Instance.ExecuteFunction(
                "ModifyRestaurant(@id,@name,@capacity,@isActive,@qualify,@specialty,@price,@businessName,@picture,@description,@phone,@location,@address)",Convert.ToInt32(restaurant.Id),
                restaurant.Name, restaurant.Capacity, restaurant.IsActive, restaurant.Qualify, restaurant.Specialty, restaurant.Price, restaurant.BusinessName, 
                restaurant.Picture, restaurant.Description, restaurant.Phone, restaurant.Location, restaurant.Address);
            
            return GetRestaurant(restaurant.Id);
        }

        public void DeleteRestaurant(int id)
        {
            PgConnection.Instance.ExecuteFunction("DeleteRestaurant(@id)",id);
        }

        private Restaurant ExtractRestaurantFromRow(DataRow row)
        {
            var restaurantId = Convert.ToInt32(row[0]);
            var name = row[1].ToString();
            var capacity = Convert.ToInt32(row[2]); 
            var isActive = Convert.ToBoolean(row[3]);
            var qualify = Convert.ToDecimal(row[4]);
            var specialty =row[5].ToString();
            var price = Convert.ToDecimal(row[6]);
            var businessName = row[7].ToString();
            var picture = row[8].ToString();
            var description = row[9].ToString();
            var phone = row[10].ToString();
            var location = Convert.ToInt32(row[11]);
            var address = row[12].ToString();
            Restaurant restaurant = new Restaurant(restaurantId, name, capacity, isActive, qualify, specialty, price, businessName, picture, description, phone, location, address);
            return restaurant;
        }
    }
}