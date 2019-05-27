

using System;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo10;

namespace vacanze_back.VacanzeApi.Persistence.Connection.Grupo10 
{
    public class TravelConnection : Connection {

        public TravelConnection(){
            CreateStringConnection();
        }

        public List<Travel> GetTravels(long userId){
            List<Travel> listOfTravels = new List<Travel>();
            Connect();
            StoredProcedure("getEmployees()");
            ExecuteReader();

            for(int i = 0; i < numberRecords; i++){
                int travelId = GetInt(i,0);
                string travelName = GetString(i,1);
                string travelDescription = GetString(i,2);

                Travel travel = new Travel(
                    travelId, travelName, travelDescription
                );

                listOfTravels.Add(travel);
            }

            return listOfTravels;
        }

        

    }


}