using System;
using System.Collections.Generic;
using System.Data;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo7
{
    public class PostgresRestaurantDAO: IRestaurantDAO
    {
        /// <summary>
        ///     Metodo para obtener un restaurante segun su id
        /// </summary>
        /// <param name="id">id del restaurante a buscar</param>
        /// <returns>Restaurant Entity</returns>
        /// <exception cref="RestaurantNotFoundExeption">No se encontro ningun restaurante con el id suministrado</exception>
        public Restaurant GetRestaurant(int id)
        {       

                var table = PgConnection.Instance.ExecuteFunction("GetRestaurant(@restaurant_id)" , id);
                if (table.Rows.Count == 0 )
                    throw new RestaurantNotFoundExeption("No se encontro ningun restaurante con el Id " + id);
                return ExtractRestaurantFromRow(table.Rows[0]);
        }
        
        /// <summary>
        ///     Metodo para obtener todos los restaurantes
        /// </summary>
        /// <returns>Lista de restaurantes</returns>
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
        
        /// <summary>
        ///     Metodo para obtener todos los restaurantes de una ciudad dada
        /// </summary>
        /// <param name="locationId">Identificador unico de la ciudad a la que pertenecen los restaurantes</param>
        /// <returns>Lista de restaurantes</returns>
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
        
        /// <summary>
        ///     Metodo para agregar un restaurante
        /// </summary>
        /// <param name="restaurant">Objeto Restaurant a crear</param>
        /// <returns>id del restaurante agregado</returns>
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
        
        /// <summary>
        ///     Metodo para modifcar un restaurant
        /// </summary>
        /// <param name="restaurant">Objeto restaurant con la data para el restaurant a modificar</param>
        /// <returns>Objeto tipo restaurant modificado</returns>
        /// <exception cref="RestaurantNotFoundExeption">Retorna BadRequest en caso de no encontrar el restaurante a actualizar</exception>
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

        /// <summary>
        ///     Metodo para eliminar un restaurante segun su id
        /// </summary>
        /// <param name="id">id del restaurante a eliminar</param>
        public void DeleteRestaurant(int id)
        {
            var table = PgConnection.Instance.ExecuteFunction("GetRestaurant(@id)" , id);
            if (table.Rows.Count == 0)
                throw new RestaurantNotFoundExeption("El restaurante que desea eliminar no existe");
            PgConnection.Instance.ExecuteFunction("DeleteRestaurant(@id)",id);
        }

        /// <summary>
        ///     Metodo extraer un restaurante de la tabla retornada por las consultas a la base de datos ejecutadas por el PgConnection
        /// </summary>
        /// <param name="row">fila a extraer</param>
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