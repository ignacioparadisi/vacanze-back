using vacanze_back.VacanzeApi.Common.Entities.Grupo10;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using System;
using vacanze_back.VacanzeApi.Common.Exceptions;
using System.Data;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo2;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo10;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo6;
using System.Linq;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo10
{
    public class Traveldao : ITravel
    {
       

        public int addtravel(Travel travel)
        {
            int id = 0;
            try{
                 if( string.IsNullOrEmpty(travel.Name) ||
                    string.IsNullOrEmpty(travel.Description) ||
                    travel.Init == DateTime.MinValue ||
                    travel.End == DateTime.MinValue ) 
                {
                    throw new RequiredAttributeException("Falta información importante para poder crear el viaje");
                }
                Entity user= UserRepository.GetUserById(travel.UserId); //Throw Exception if it doesn'n exist
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction(
                    "AddTravel(@travelName, @travelInit, @travelEnd, @travelDescription, @userId)",travel.Name, travel.Init.ToString("yyyy-MM-dd"), travel.End.ToString("yyyy-MM-dd"), travel.Description, travel.UserId);
                id = Convert.ToInt32(dataTable.Rows[0][0]);
                
               }
               catch(DatabaseException ex){
                throw new InternalServerErrorException("Error en el servidor : Conexion a base de datos", ex);
            }catch(InvalidStoredProcedureSignatureException ex){
                throw new InternalServerErrorException("Error en el servidor", ex);
            }
            return id;

            
        }

        public int Deletetravel(int travelid)
        {   int i=0;
        
            PgConnection pgConnection = PgConnection.Instance;
            
             pgConnection.ExecuteFunction("deletetravel (@travelid)", travelid);
             return i;
           
        }

        public List<Travel> Gettravel(int userId)
        {
            List<Travel> listOfTravels = new List<Travel>();
            try{
                Console.WriteLine(userId);
                 User user = (User)UserRepository.GetUserById(userId);; //Throw Exception if it doesn'n exist
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction("GetTravels(@userId)", userId);
                if( dataTable.Rows.Count > 0){
                    foreach (DataRow dataRow in dataTable.Rows){
                        Travel travel = new Travel(
                            Convert.ToInt32(dataRow[0]),
                            dataRow[1].ToString(),
                            Convert.ToDateTime(dataRow[2]),
                            Convert.ToDateTime(dataRow[3]),
                            dataRow[4].ToString(),
                            Convert.ToInt32(dataRow[5])
                        );   
                        listOfTravels.Add(travel);  
                    }
                }else{
                    throw new WithoutExistenceOfTravelsException(userId, "Animate, planifica un viaje");
                }
            }catch(DatabaseException ex){
                throw new InternalServerErrorException("Error en el servidor : Conexion a base de datos", ex);
            }catch(InvalidStoredProcedureSignatureException ex){
                throw new InternalServerErrorException("Error en el servidor", ex);
            }
            return listOfTravels;
        }

        public bool Updatetravel(Travel travel)
        {
            Boolean result=false;
            try{
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction(
                    "UpdateTravel(@travelId, @travelName, @travelDescription, @travelInit, @travelEnd)",
                    travel.Id, travel.Name, travel.Description, travel.Init.ToString("yyyy-MM-dd"), travel.End.ToString("yyyy-MM-dd"));
                if(Convert.ToBoolean(dataTable.Rows[0][0])){
                    result = true;
                }

            }catch(DatabaseException ex){
                throw new InternalServerErrorException("Error en el servidor : Conexion a base de datos", ex);
            }catch(InvalidStoredProcedureSignatureException ex){
                throw new InternalServerErrorException("Error en el servidor", ex);
            }

           return result;
        }

        public  List<Location> GetLocationsByTravel(int travelId){
            List<Location> locationsByTravel = new List<Location>();
            try{
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
                    throw new WithoutTravelLocationsException("El viaje aun no tiene ciudades");
                }
            }catch(DatabaseException ex){
                throw new InternalServerErrorException("Error en el servidor : Conexion a base de datos", ex);
            }catch(InvalidStoredProcedureSignatureException ex){
                throw new InternalServerErrorException("Error en el servidor", ex);
            }
            return locationsByTravel;
        }

         public  Boolean AddLocationsToTravel(int travelId, List<Location> locations){
            Boolean saved = false;
            try{
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable;
                foreach( Location location in locations ){
                    dataTable = pgConnection.ExecuteFunction("AddLocationToTravel(@travelId, @locationId)",
                        travelId, location.Id);
                }
            }catch(DatabaseException ex){
                throw new InternalServerErrorException("Error en el servidor : Conexion a base de datos", ex);
            }catch(InvalidStoredProcedureSignatureException ex){
                throw new InternalServerErrorException("Error en el servidor", ex);
            }
            return saved;
        }
        public  Boolean AddReservationToTravel(int travel, int reservation, string type){
            Boolean saved = false;
            try{
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction(
                    "AddReservationToTravel(@travel, @reservation, @type)",
                    travel, reservation, type);
                if(Convert.ToBoolean(dataTable.Rows[0][0]))
                    saved = true;
            }catch(DatabaseException ex){
                throw new InternalServerErrorException("Error en el servidor : Conexion a base de datos", ex);
            }catch(InvalidStoredProcedureSignatureException ex){
                throw new InternalServerErrorException("Error en el servidor", ex);
            }
            return saved;
        }

 public  List<T> GetReservationsByTravelAndLocation<T>(int travelId, int locationId, string type){
            List<T> reservations = new List<T>();
            try{
                PgConnection pgConnection = PgConnection.Instance;
                if(type.Equals("HOTEL")){
                    DataTable dataTable = pgConnection.ExecuteFunction(
                    "GetReservationsOfHotelByTravelAndLocation(@travelId, @locationId)", travelId, locationId);
                    if( dataTable.Rows.Count > 0 ){
                        List<ReservationRoom> reservationsOfRoom = new List<ReservationRoom>();
                        foreach (DataRow dataRow in dataTable.Rows){
                            ReservationRoom reservationRoom = new ReservationRoom(
                            Convert.ToInt32(dataRow[0]),
                            DateTime.Parse(dataRow[1].ToString()),
                            DateTime.Parse(dataRow[2].ToString()), Convert.ToInt32(dataRow[5]), Convert.ToInt32(dataRow[4]));
                            reservationsOfRoom.Add(reservationRoom);
                        }
                        reservations = reservationsOfRoom.Cast<T>().ToList();
                    }else{
                        throw new WithoutTravelReservationsException(
                            travelId, locationId, "No posee reservaciones de " + type.ToLower() + " en dicha ciudad");
                    }
                }else if(type.Equals("RESTAURANT")){
                    DataTable dataTable = pgConnection.ExecuteFunction(
                    "GetReservationsOfRestaurantByTravelAndLocation(@travelId, @locationId)", travelId, locationId);
                    if( dataTable.Rows.Count > 0 ){
                        List<Restautantetravel> reservationsOfRest=new List<Restautantetravel>();
                      
                        foreach (DataRow dataRow in dataTable.Rows){
                            Restautantetravel reservationRest = new Restautantetravel(
                            Convert.ToInt32(dataRow[0]),
                            DateTime.Parse(dataRow[1].ToString()),
                            Convert.ToInt32(dataRow[2].ToString()), 
                            Convert.ToInt32(dataRow[5]),
                             Convert.ToInt32(dataRow[4]));
                            reservationsOfRest.Add(reservationRest);
                        }
                        reservations = reservationsOfRest.Cast<T>().ToList();
                    }else{
                        throw new WithoutTravelReservationsException(
                            travelId, locationId, "No posee reservaciones de " + type.ToLower() + " en dicha ciudad");
                    }
                }else if(type.Equals("FLIGHT")){

                    
                    
                }else if(type.Equals("CAR")){
                    DataTable dataTable = pgConnection.ExecuteFunction(
                    "GetReservationsOfCarssByTravelAndLocation(@travelId, @locationId)", travelId, locationId);
                    if( dataTable.Rows.Count > 0 ){
                        List<ReservationVehicle> reservationsOfAuto = new List<ReservationVehicle>();
                           foreach (DataRow dataRow in dataTable.Rows){
                            ReservationVehicle reservationAuto = new ReservationVehicle(
                            Convert.ToInt32(dataRow[0]),
                            DateTime.Parse(dataRow[1].ToString()),
                            DateTime.Parse(dataRow[2].ToString()), 
                            Convert.ToInt32(dataRow[4]), 
                            Convert.ToInt32(dataRow[3]));
                            reservationsOfAuto.Add(reservationAuto);
                        }
                        reservations = reservationsOfAuto.Cast<T>().ToList();
                    }else{
                        throw new WithoutTravelReservationsException(
                            travelId, locationId, "No posee reservaciones de " + type.ToLower() + " en dicha ciudad");
                    }
                }else{
                    throw new InvalidReservationTypeException(type,"Tipo de reserva invalido : " + type);
                }
            }catch(DatabaseException ex){
                throw new InternalServerErrorException("Error en el servidor : Conexion a base de datos", ex);
            }catch(InvalidStoredProcedureSignatureException ex){
                throw new InternalServerErrorException("Error en el servidor", ex);
            }
            return reservations;
        }
    }
}

