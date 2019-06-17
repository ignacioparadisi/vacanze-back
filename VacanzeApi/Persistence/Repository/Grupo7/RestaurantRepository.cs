using System;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo7
{
    /// <summary>  
    ///  Clase para manejar peticiones, relacionadas a los restaurantes, contra la base de datos   
    /// </summary>  
    public static class RestaurantRepository
    {
        /// <summary>
        ///     Metodo para crear un restaurant
        /// </summary>
        /// <param name="restaurant">Objeto Restaurant a crear</param>
        /// <returns>Objeto tipo restaurant del restaurant creado</returns>
        /// <exception cref="DatabaseException">Ocurrio una excepcion en la ejecución de la función</exception>
        /// <exception cref="InvalidOperationException">Algun atributo tenia un valor invalido o nulo</exception>
        public static int AddRestaurant(Restaurant restaurant)
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

        /// <summary>
        ///     Metodo para modifcar un restaurant
        /// </summary>
        /// <param name="restaurant">Objeto restaurant con la data para el restaurant a modificar</param>
        /// <returns>Objeto tipo restaurant del restaurant modificado</returns>
        /// <exception cref="InvalidOperationException">Algun atributo tenia un valor invalido o nulo</exception>
        /// <exception cref="InvalidCastException">Algun atributo no era del tipo correcto</exception>
        /// <exception cref="DatabaseException">Ocurrio una excepcion en la ejecución de la función</exception>
        public static Restaurant UpdateRestaurant(Restaurant restaurant)
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

        /// <summary>
        ///     Metodo para obtener todos los restaurantes
        /// </summary>
        /// <returns>Lista tipo restaurant con los restaurantes obtenidos</returns>
        /// <exception cref="DatabaseException">Ocurrio una excepcion en la ejecución de la función</exception>
        public static List<Restaurant> GetRestaurants()
        {
            try
            {
                var table = PgConnection.Instance.ExecuteFunction("getrestaurants()");
                var restaurantList = new List<Restaurant>();
                for (var i = 0; i < table.Rows.Count; i++)
                {
                    var id = Convert.ToInt32(table.Rows[i][0]);
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
            catch(DatabaseException){
                throw new GetRestaurantExcepcion("No se pudieron obtener los restaurants existentes");
            }
            
        }

        /// <summary>
        ///     Metodo para obtener un unico restaurant
        /// </summary>
        /// <param name="restaurant_id">Identificador unico del restaurant a obtener</param>
        /// <returns>Objeto tipo restaurant con el restaurant obtenido</returns>
        /// <exception cref="DatabaseException">Ocurrio una excepcion en la ejecución de la función</exception>
        public static Restaurant GetRestaurant(int restaurant_id)
        {       
            try
            {
                var table = PgConnection.Instance.ExecuteFunction("GetRestaurant(@restaurant_id)" , restaurant_id);
                var id = Convert.ToInt32(table.Rows[0][0]);
                var name = table.Rows[0][1].ToString();
                var capacity = Convert.ToInt32(table.Rows[0][2]); 
                var isActive = Convert.ToBoolean(table.Rows[0][3]);
                var qualify = Convert.ToDecimal(table.Rows[0][4]);
                var specialty = table.Rows[0][5].ToString();
                var price = Convert.ToDecimal(table.Rows[0][6]);
                var businessName = table.Rows[0][7].ToString();
                var picture = table.Rows[0][8].ToString();
                var description = table.Rows[0][9].ToString();
                var phone = table.Rows[0][10].ToString();
                var location = Convert.ToInt32(table.Rows[0][11]);
                var address = table.Rows[0][12].ToString();
                Console.WriteLine(name);
                Restaurant restaurant = new Restaurant(id, name, capacity, isActive, qualify, specialty, price, businessName, picture, description, phone, location, address);
                return restaurant;
            }
            catch (DatabaseException)
            {
                throw new GetRestaurantExcepcion("No se pudo obtener el restaurant solicitado");
            }
        }

        /// <summary>
        ///     Metodo para obtener todos los restaurantes de una ciudad dada
        /// </summary>
        /// <param name="location_id">Identificador unico de la ciudad a la que pertenecen los restaurantes</param>
        /// <returns>Lista tipo restaurant con los restaurantes obtenidos</returns>
        /// <exception cref="DatabaseException">Ocurrio una excepcion en la ejecución de la función</exception>
        public static List<Restaurant> GetRestaurantsByCity(int location_id)
        {
            try
            {
                var table = PgConnection.Instance.ExecuteFunction("GetRestaurantsByCity(@location_id)", location_id);
                var restaurantList = new List<Restaurant>();
                for (var i = 0; i < table.Rows.Count; i++)
                {
                    var id = Convert.ToInt32(table.Rows[i][0]);
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
            catch(DatabaseException)
            {
                throw new GetRestaurantExcepcion("No se pudo obtener los restaurantes para esa ciudad");
            }
        }

        /// <summary>
        ///     Metodo para eliminar un restaurante 
        /// </summary>
        /// <param name="id">Identificador unico del restaurant a eliminar</param>
        /// <returns>Entero que contiene el identificador del restaurant eliminado</returns>
        /// <exception cref="DatabaseException">Ocurrio una excepcion en la ejecución de la función</exception>
        /// <exception cref="InvalidCastException">El identificador dado no corresponde a un restaurant existente</exception>
         public static int DeleteRestaurant(int id)
        {
            try
            {
                var table = PgConnection.Instance.ExecuteFunction("DeleteRestaurant(@id)",id);
                var deletedid = Convert.ToInt32(table.Rows[0][0]);
                return deletedid;
            }
            catch (InvalidCastException)
            {
                throw new DeleteRestaurantException("El restaurant no existe");
            }
            catch (DatabaseException)
            {
                throw new DeleteRestaurantException("No se pudo conectar con la base de datos");
            }
        }
    }
}