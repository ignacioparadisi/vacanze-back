using vacanze_back.VacanzeApi.Common.Entities.Grupo10;
using vacanze_back.VacanzeApi.Common.Entities;
using System.Data;
using System.Collections.Generic;
namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo10

{
   public interface ITravel
   {
       int addtravel(Travel travel);
       
       List<Travel>  Gettravel(int i );

       int Deletetravel(int id);

       bool Updatetravel(Travel travel);
       List<Location> GetLocationsByTravel(int travelId);
        
       bool AddLocationsToTravel(int travelId, List<Location> locations);

       bool AddReservationToTravel(int travel, int reservation, string type);

     /*   List<T> GetReservationsByTravelAndLocation<T>(int travelId, int locationId, string type);*/
    }
}