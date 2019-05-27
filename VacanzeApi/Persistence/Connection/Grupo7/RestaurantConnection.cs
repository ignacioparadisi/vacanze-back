using System;
using System.Collections.Generic;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.Connection.Grupo7
{
    public class RestaurantConnection : Connection
    {
        public RestaurantConnection()
        {
            CreateStringConnection();
        }

        /// <summary>
        ///     Obtener todos los restaurantes
        /// </summary>
        public List<Restaurant> GetRestaurants()
        {
            var restaurantList = new List<Restaurant>();
            try
            {
                Connect();
                StoredProcedure("getrestaurants()");
                ExecuteReader();

                for (var i = 0; i < numberRecords; i++)
                {
                    var id = Convert.ToInt32(GetString(i, 0));
                    var name = GetString(i, 1);
                    var capacity = Convert.ToInt32(GetString(i, 2));
                    var isActive = Convert.ToBoolean(GetString(i, 3));
                    var specialty = GetString(i, 4);
                    var price = Convert.ToInt32(GetString(i, 5));
                    var businessName = GetString(i, 6);
                    var picture = GetString(i, 7);
                    var description = GetString(i, 8);
                    var phone = GetString(i, 9);
                    var location = Convert.ToInt32(GetString(i, 10));
                    var address = GetString(i, 11);
                    var restaurant = new Restaurant(id,name,capacity,isActive,specialty,price,businessName,picture,description,phone,location,address);
                    restaurantList.Add(restaurant);
                }

                return restaurantList;
            }
            catch (NpgsqlException)
            {
                throw new DatabaseException("Error con la base de datos al consultar los restaurantes");
            }
            catch (Exception e)
            {
                throw new GeneralException(e, DateTime.Now);
            }
        }

        public long AddRestaurant(Restaurant restaurant)
        {
            Connect();
            StoredProcedure(
                "addrestaurant2(@name,@capacity,@isActive,@specialty,@price,@businessName,@picture,@description,@phone,@location,@address)");
            AddParameter("name", restaurant.Name);
            AddParameter("capacity", restaurant.Capacity);
            AddParameter("isActive", restaurant.IsActive);
            AddParameter("specialty", restaurant.Specialty);
            AddParameter("price", restaurant.Price);
            AddParameter("businessName", restaurant.BusinessName);
            AddParameter("picture", restaurant.Picture);
            AddParameter("description", restaurant.Description);
            AddParameter("phone", restaurant.Phone);
            AddParameter("location", 1); // TODO: Usar id de una ubicacion recibida!!
            AddParameter("address", restaurant.Address);
            ExecuteReader();
            var savedId = GetInt(0, 0);
            return savedId;
        }
    }
}