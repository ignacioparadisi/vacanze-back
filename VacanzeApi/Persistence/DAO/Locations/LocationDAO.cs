using System.Collections.Generic;
using System.Data;
using vacanze_back.VacanzeApi.Common.Entities;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Locations
{
    public interface LocationDAO
    {
        List<Location> GetLocations();
        Location GetLocationById(int id);
        List<Location> GetCountries();
        List<Location> GetCitiesByCountry(int id);
        int AddLocation(Location location);
        void DeleteLocation(int id);
    }
}