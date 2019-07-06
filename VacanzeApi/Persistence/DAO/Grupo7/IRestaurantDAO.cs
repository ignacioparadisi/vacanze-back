using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo7
{
    /// <summary>  
    ///  Interfaz para definir los metodos que debe implementar los DAO de restaurnates
    /// </summary> 
    public interface IRestaurantDAO
    {
        
        /// <summary>
        ///     Metodo para obtener todos los restaurantes 
        /// </summary>
        /// <returns>Restaurant Entity</returns>
        List<Restaurant> GetRestaurants();
        /// <summary>
        ///     Metodo para obtener un restaurante segun su id
        /// </summary>
        /// <param name="id">id del restaurante a buscar</param>
        /// <returns>Restaurant Entity</returns>
        Restaurant GetRestaurant(int id);
        /// <summary>
        ///     Metodo para obtener todos los restaurantes de una ciudad dada
        /// </summary>
        /// <param name="locationId">Identificador unico de la ciudad a la que pertenecen los restaurantes</param>
        /// <returns>Lista de restaurantes</returns>
        List<Restaurant> GetRestaurantsByCity(int locationId);
        
        /// <summary>
        ///     Metodo para agregar un restaurante
        /// </summary>
        /// <param name="restaurant">Objeto Restaurant a crear</param>
        /// <returns>id del restaurante agregado</returns>
        int AddRestaurant(Restaurant restaurant);
        /// <summary>
        ///     Metodo para modifcar un restaurant
        /// </summary>
        /// <param name="restaurant">Objeto restaurant con la data para el restaurant a modificar</param>
        /// <returns>Objeto tipo restaurant modificado</returns>
        Restaurant UpdateRestaurant(Restaurant restaurant);
        /// <summary>
        ///     Metodo para eliminar un restaurante segun su id
        /// </summary>
        /// <param name="id">id del restaurante a eliminar</param>
        void DeleteRestaurant(int id);
    }
}