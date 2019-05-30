using System;
using System.Collections.Generic;
using System.Data;
using vacanze_back.VacanzeApi.Common.Entities.Grupo10;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo10;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo10
{
    public class TravelRepository{

        ///<sumary>
        /// Receive the user's id and return their travels
        ///</sumary>
        ///<returns>
        /// User's travels
        ///</returns>
        ///<param name="userId">User's id</param>
        public static List<Travel> GetTravels(int userId){
            List<Travel> listOfTravels = new List<Travel>();
            try{
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction("GetTravels(@userId)", userId);
                if( dataTable.Rows.Count > 0){
                    foreach (DataRow dataRow in dataTable.Rows){
                        Travel travel = new Travel(
                            Convert.ToInt64(dataRow[0]),
                            dataRow[1].ToString(),
                            dataRow[2].ToString()
                        );   
                        listOfTravels.Add(travel);  
                    }
                }else{
                    throw new WithoutExistenceOfTravelsException(userId, "Animate, planifica un viaje");
                }
            }catch(DatabaseException){
                
            }catch(InvalidStoredProcedureSignatureException){
                
            }finally{

            }
            return listOfTravels;
        }

        ///<sumary>
        /// Create a Travel for a specific User
        ///</sumary>
        ///<returns>
        /// Travel's id
        ///</returns>
        ///<param name="travel">Travel that should be inserted at the db</param>
        public static long AddTravel(Travel travel){
            long id = 0;
            try{
                //1. Validate: User exist & Is a Client
                    //throw exception
                //2. Validate: Name repeated
                    //throw exception
                if(string.IsNullOrEmpty(travel.Name)){
                    throw new NameRequiredException("El viaje debe tener un nombre");
                }
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction("AddTravel(@travelName, @travelDescription, @userId)", 
                    travel.Name, travel.Description, travel.UserId);
                id = Convert.ToInt64(dataTable.Rows[0][0]);
                travel.Id = id;
            }catch(DatabaseException){

            }catch(InvalidStoredProcedureSignatureException){
                
            }finally{

            }
            return id;
        }

    }




}