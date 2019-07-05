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
            try
            {
                var table = PgConnection.Instance.ExecuteFunction("GetRestaurant(@restaurant_id)" , id);
                return ExtractRestaurantFromRow(table.Rows[0]);
            }
            catch (DatabaseException)
            {
                throw new GetRestaurantExcepcion("No se pudo obtener el restaurant solicitado");
            }
        }
        
        public List<Restaurant> GetRestaurants()
        {
            try
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
            catch(DatabaseException){
                throw new GetRestaurantExcepcion("No se pudieron obtener los restaurants existentes");
            }
        }
        
        public List<Restaurant> GetRestaurantsByCity(int locationId)
        {
            try
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
            catch(DatabaseException)
            {
                throw new GetRestaurantExcepcion("No se pudo obtener los restaurantes para esa ciudad");
            }
        }
        
        public int AddRestaurant(Restaurant restaurant)
        {
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(
                    "addrestaurant(@name,@capacity,@isActive,@qualify,@specialty,@price,@businessName,@picture,@description,@phone,@location,@address)",
                    restaurant.Name, restaurant.Capacity, restaurant.IsActive, restaurant.Qualify, restaurant.Specialty, restaurant.Price, restaurant.BusinessName, 
                    restaurant.Picture, restaurant.Description, restaurant.Phone, restaurant.Location, restaurant.Address
                );
                var savedId = Convert.ToInt32(table.Rows[0][0]);
                return savedId;
            }
            catch(DatabaseException)
            {
                throw new AddRestaurantException("No se pudo agregar el restaurant");
            }
            catch(InvalidOperationException)
            {
                throw new AddRestaurantException("No se llenaron los campos necesarios para poder crear un restaurant");
            } 
        }
        
        public Restaurant UpdateRestaurant(Restaurant restaurant)
        {
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(
                    "ModifyRestaurant(@id,@name,@capacity,@isActive,@qualify,@specialty,@price,@businessName,@picture,@description,@phone,@location,@address)",Convert.ToInt32(restaurant.Id),
                    restaurant.Name, restaurant.Capacity, restaurant.IsActive, restaurant.Qualify, restaurant.Specialty, restaurant.Price, restaurant.BusinessName, 
                    restaurant.Picture, restaurant.Description, restaurant.Phone, restaurant.Location, restaurant.Address
                );
                var id = Convert.ToInt32(table.Rows[0][0]);
                return restaurant;
            }
            catch (InvalidOperationException)
            {
                throw new UpdateRestaurantException("Error, no se pudo actualizar el restaurant");
            }
            catch (InvalidCastException)
            {
                throw new UpdateRestaurantException("Error, no se pudo actualizar el restaurant");
            }
            catch (DatabaseException)
            {
                throw new UpdateRestaurantException("Error, no se pudo conectar con la base de datos");
            }
           
        }

        public void DeleteRestaurant(int id)
        {
            try
            {
                PgConnection.Instance.ExecuteFunction("DeleteRestaurant(@id)",id);
            }
            catch (NpgsqlException e)
            {
                throw new DatabaseException(e.Message);
            }
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