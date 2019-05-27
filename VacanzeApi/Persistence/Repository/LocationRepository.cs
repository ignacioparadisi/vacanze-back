using System;
using System.Collections.Generic;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.Repository
{
    public static class LocationRepository
    {
        public static List<Location> GetLocations()
        {
            var locationList = new List<Location>();
            try
            {
                var results = PgConnection.Instance.ExecuteFunction("GetLocations()");
                for (var i = 0; i < results.Rows.Count; i++)
                {
                    var id = Convert.ToInt64(results.Rows[i][0]);
                    var city = results.Rows[i][1].ToString();
                    var country = results.Rows[i][2].ToString();
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