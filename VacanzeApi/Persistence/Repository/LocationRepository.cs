using System;
using System.Collections.Generic;
using System.Data;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.Repository
{
    public static class LocationRepository
    {
        public static List<Location> GetLocations()
        {
            var locationList = new List<Location>();
            var results = PgConnection.Instance.ExecuteFunction("GetLocations()");
            for (var i = 0; i < results.Rows.Count; i++)
                locationList.Add(ExtractLocationFromRow(results.Rows[i]));
            return locationList;
        }

        public static Location GetLocationById(int id)
        {
            var resultTable = PgConnection.Instance.ExecuteFunction("GetLocationById(@p_id)", id);
            if (resultTable.Rows.Count == 0)
                throw new LocationNotFoundException(id);

            return ExtractLocationFromRow(resultTable.Rows[0]);
        }

        public static List<Location> GetCountries()
        {
            var locationList = new List<Location>();
            var results = PgConnection.Instance.ExecuteFunction("GetCountries()");
            for (var i = 0; i < results.Rows.Count; i++)
                locationList.Add(ExtractLocationFromRow(results.Rows[i]));
            return locationList;
        }

        public static List<Location> GetCitiesByCountry(int id)
        {
            var locationList = new List<Location>();
            var results = PgConnection.Instance.ExecuteFunction("GetCitiesByCountry(@city_id)", id);
            for (var i = 0; i < results.Rows.Count; i++)
                locationList.Add(ExtractLocationFromRow(results.Rows[i]));
            return locationList;
        }

        private static Location ExtractLocationFromRow(DataRow row)
        {
            var id = Convert.ToInt32(row[0]);
            var city = row[1].ToString();
            var country = row[2].ToString();
            return new Location(id, country, city);
        }
        
        public static int AddLocation(Location location)
        {
            // TODO: try / catch DatabseException y retornar algo tipo SaveHotelException
            var table = PgConnection.Instance.ExecuteFunction(
                "AddLocation(@city, @country)",
                location.City,
                location.Country
            );
            var savedId = Convert.ToInt32(table.Rows[0][0]);
            return savedId;
        }
    }
}