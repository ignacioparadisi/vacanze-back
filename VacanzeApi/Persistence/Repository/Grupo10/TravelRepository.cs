using System;
using System.Collections.Generic;
using System.Data;
using vacanze_back.VacanzeApi.Common.Entities.Grupo10;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo10
{
    public class TravelRepository{

        ///<sumary>
        /// Receive the user's id and return their travels
        ///</sumary>
        ///<returns>
        /// User's travels
        ///</returns>
        ///<param name=userId>User's id</param>
        public static List<Travel> GetTravels(int userId){
            List<Travel> listOfTravels = new List<Travel>();
            PgConnection pgConnection = PgConnection.Instance;
            DataTable dataTable = pgConnection.ExecuteFunction("GetTravels(@userId)", userId);
            foreach (DataRow dataRow in dataTable.Rows){
                Travel travel = new Travel(
                    Convert.ToInt64(dataRow[0]),
                    dataRow[1].ToString(),
                    dataRow[2].ToString()
                );
                listOfTravels.Add(travel);  
            }
            return listOfTravels;
        }

    }




}