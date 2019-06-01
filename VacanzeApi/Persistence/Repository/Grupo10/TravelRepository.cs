using System;
using System.Collections.Generic;
using System.Data;
using vacanze_back.VacanzeApi.Common.Entities.Grupo10;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo10;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo2;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo10
{
    public class TravelRepository{

        ///<sumary>
        /// Receive the user's id and return their travels
        ///</sumary>
        ///<exception cref="WithoutExistenceOfTravelsException"></exception>
        ///<returns>
        /// User's travels
        ///</returns>
        ///<param name="userId">User's id</param>
        public static List<Travel> GetTravels(long userId){
            List<Travel> listOfTravels = new List<Travel>();
            try{
                User user = UserRepository.GetUserById((int)userId);    
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction("GetTravels(@userId)", userId);
                if( dataTable.Rows.Count > 0){
                    foreach (DataRow dataRow in dataTable.Rows){
                        Travel travel = new Travel(
                            Convert.ToInt64(dataRow[0]),
                            dataRow[1].ToString(),
                            Convert.ToDateTime(dataRow[2]),
                            Convert.ToDateTime(dataRow[3]),
                            dataRow[4].ToString(),
                            Convert.ToInt64(dataRow[5])
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
        /// Receive the travel's id and return their associate locations
        ///</sumary>
        ///<returns>
        /// List of Locations by travel
        ///</returns>
        ///<param name="travelId">Travel's id</param>
        public static List<Location> GetLocationsByTravel(long travelId){
            List<Location> locationsByTravel = new List<Location>();
            try{
                // Validate travel id exist
                    //Throw exception
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction("GetLocationsByTravel(@travelId)", travelId);
                if( dataTable.Rows.Count > 0){
                    foreach (DataRow dataRow in dataTable.Rows){
                        Location location = new Location();
                        location.Id = Convert.ToInt32(dataRow[0]);
                        location.City = dataRow[1].ToString();
                        location.Country = dataRow[2].ToString();
                        locationsByTravel.Add(location);    
                    }
                }else{
                    //Throw Exception travel id exist
                }
            }catch(DatabaseException){

            }catch(InvalidStoredProcedureSignatureException){

            }finally{

            }
            return locationsByTravel;
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
                DataTable dataTable = pgConnection.ExecuteFunction(
                    "AddTravel(@travelName, @travelInit, @travelEnd, @travelDescription, @userId)", 
                    travel.Name, travel.Init.ToString("yyyy-MM-dd"), travel.End.ToString("yyyy-MM-dd"), travel.Description, travel.UserId);
                id = Convert.ToInt64(dataTable.Rows[0][0]);
                travel.Id = id;
            }catch(DatabaseException ex){
                throw new Exception(ex.Message);
            }catch(InvalidStoredProcedureSignatureException ex){
                throw new Exception(ex.Message);
            }finally{

            }
            return id;
        }
        
        public static Boolean AddLocationsToTravel(long travelId, List<Location> locations){
            Boolean saved = false;
            try{
                // Validate travel id exist
                    //Throw exception
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable;
                foreach( Location location in locations ){
                    // Validate location exist
                        //throw exception
                    dataTable = pgConnection.ExecuteFunction("AddLocationToTravel(@travelId, @locationId)",
                        travelId, location.Id);
                    if(!(saved = Convert.ToBoolean(dataTable.Rows[0][0]))){
                        // throw exception
                    }
                    
                }
            }catch(DatabaseException ex){
                throw new Exception(ex.Message);
            }catch(InvalidStoredProcedureSignatureException ex){
                throw new Exception(ex.Message);
            }finally{

            }
            return saved;
        }


    }

}