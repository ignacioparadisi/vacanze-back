using System;
using System.Collections.Generic;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.Connection
{
    public class LocationConnection : Connection
    {
        public LocationConnection()
        {
            CreateStringConnection();
        }

        /// <summary>
        ///     Obtener todos los registros
        /// </summary>
        public List<Location> GetLocations()
        {
            var locationList = new List<Location>();
            try
            {
                Connect();
                StoredProcedure("GetLocations()");
                ExecuteReader();

                for (var i = 0; i < numberRecords; i++)
                {
                    var id = Convert.ToInt32(GetString(i, 0));
                    var city = GetString(i, 1);
                    var country = GetString(i, 2);
                    var location = new Location(id, city, country);
                    locationList.Add(location);
                }

                return locationList;
            }
            catch (NpgsqlException)
            {
                throw new DatabaseException("Error con la base de datos al consultar los lugares");
            }
            catch (Exception e)
            {
                throw new GeneralException(e, DateTime.Now);
            }
        }

    }
}