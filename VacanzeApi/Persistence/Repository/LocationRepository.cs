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
                throw new EntityNotFoundException($"Location with id {id} not found");

            return ExtractLocationFromRow(resultTable.Rows[0]);
        }

        private static Location ExtractLocationFromRow(DataRow row)
        {
            var id = Convert.ToInt64(row[0]);
            var city = row[1].ToString();
            var country = row[2].ToString();
            return new Location(id, city, country);
        }
    }
}