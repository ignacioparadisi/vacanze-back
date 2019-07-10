using System;
using System.Collections.Generic;
using System.Data;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Locations
{
    public class PostgresLocationDAO : LocationDAO
    {
        /// <summary>
        ///     Metodo para obtener todos los ubicaciones guardados.
        /// </summary>
        /// <returns>Lista de Locations</returns>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la base de
        ///     datos
        /// </exception>
        public List<Location> GetLocations()
        {
            var locationList = new List<Location>();
            var results = PgConnection.Instance.ExecuteFunction("GetLocations()");
            for (var i = 0; i < results.Rows.Count; i++)
                locationList.Add(ExtractLocationFromRow(results.Rows[i]));
            return locationList;
        }
        /// <summary>
        ///     Metodo para obtener la ubicacion por id.
        /// </summary>
        /// <param name="id">ID del hotel a buscar</param>
        /// <returns>objeto tipo Location</returns>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la base de
        ///     datos
        /// </exception>
        public Location GetLocationById(int id)
        {
            var resultTable = PgConnection.Instance.ExecuteFunction("GetLocationById(@p_id)", id);
            if (resultTable.Rows.Count == 0)
                throw new LocationNotFoundException(id);

            return ExtractLocationFromRow(resultTable.Rows[0]);
        }

        /// <summary>
        ///     Metodo para obtener todos los paises.
        /// </summary>
        /// <returns>Lista de Locations</returns>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la base de
        ///     datos
        /// </exception>
        public List<Location> GetCountries()
        {
            var locationList = new List<Location>();
            var results = PgConnection.Instance.ExecuteFunction("GetCountries()");
            for (var i = 0; i < results.Rows.Count; i++)
                locationList.Add(ExtractLocationFromRow(results.Rows[i]));
            return locationList;
        }
        /// <summary>
        ///     Metodo para obtener todos las ciudades por pais
        /// </summary>
        /// <param name="id">ID del alguna ciudad del pais deseado, para buscar las demas ciudades</param>
        /// <returns>Lista de Locations</returns>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la base de
        ///     datos
        /// </exception>
        public List<Location> GetCitiesByCountry(int id)
        {
            var locationList = new List<Location>();
            var results = PgConnection.Instance.ExecuteFunction("GetCitiesByCountry(@city_id)", id);
            for (var i = 0; i < results.Rows.Count; i++)
                locationList.Add(ExtractLocationFromRow(results.Rows[i]));
            return locationList;
        }

        /// <summary>
        ///     Metodo para formar un <see cref="Location" /> a partir de un <see cref="DataRow" />.
        ///     El <see cref="DataRow" /> debe cumplir con un orden especifico de parametros para funcionar,
        ///     el cual es respetado por todos las funciones almancenadas relacionadas al modulo de Hotel.
        /// </summary>
        /// <param name="row">
        ///     Fila en donde se encuentran los datos para generar el <see cref="Location" />
        /// </param>
        /// <returns><see cref="Location" /> segun los datos recibidos</returns>
        /// <exception cref="IndexOutOfRangeException">
        ///     Lanzado si el DataRow no devuelve la cantidad de atributos necesarios
        /// </exception>
        /// <exception cref="FormatException">
        ///     Lanzado si algun elemento del DataRow no esta en el orden correcto y por lo tanto no se
        ///     puede convertir al tipo de dato correspondiente para el <see cref="Location" />
        /// </exception>
        private Location ExtractLocationFromRow(DataRow row)
        {
            var id = Convert.ToInt32(row[0]);
            var city = row[1].ToString();
            var country = row[2].ToString();
            return new Location(id, country, city);
        }
        /// <summary>
        ///     Metodo para agregar una ubicacion
        /// </summary>
        /// <param name="location">Objeto tipo Location a agregar</param>
        /// <returns>ID de la ubicacion creada</returns>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la base de
        ///     datos
        /// </exception>
        public int AddLocation(Location location)
        {
            var table = PgConnection.Instance.ExecuteFunction(
                "AddLocation(@city, @country)",
                location.City,
                location.Country
            );
            var savedId = Convert.ToInt32(table.Rows[0][0]);
            return savedId;
        }
        /// <summary>
        ///     Metodo para elimnar una ubicacion
        /// </summary>
        /// <param name="id">ID del objeto location a eliminar</param>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la base de
        ///     datos
        /// </exception>
        public void DeleteLocation(int id)
        {
            PgConnection.Instance.ExecuteFunction("DeleteLocation(@_id)", id);
        }
    }
}