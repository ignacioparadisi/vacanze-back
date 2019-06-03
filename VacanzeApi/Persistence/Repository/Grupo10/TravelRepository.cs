using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using vacanze_back.VacanzeApi.Common.Entities.Grupo10;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo10;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo2;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo6;
using vacanze_back.VacanzeApi.Common.Entities.Grupo14;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo10
{
    public class TravelRepository{

        ///<sumary>
        /// Consulta de viejes por usuario
        ///</sumary>
        ///<param name="userId">Id del Usuario</param>
        ///<returns>
        /// Lista de vieajes del usuario
        ///</returns>
        ///<exception cref="WithoutExistenceOfTravelsException">
        /// La excepción es lanzada cuando el usuario no pesee ningún viaje
        ///</exception>
        ///<exception cref="UserNotFoundException">
        /// La excepción es lanzada si el usuario no existe
        ///</exception>
        public static List<Travel> GetTravels(int userId){
            List<Travel> listOfTravels = new List<Travel>();
            try{
                User user = UserRepository.GetUserById(userId); //Throw Exception if it doesn'n exist
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

        ///<sumary>
        /// Consulta de reservas de un viaje por ciudad y tipo.
        ///</sumary>
        ///<param name="travelId">Id del viaje en la que se basará la consulta</param>
        ///<param name="locationId">Id de la ciudad en la que se basará la consulta</param>
        ///<param name="type">Tipo de reserva en la que se basará la consulta, los tipos son:
        /// RESTAURANT, HOTEL, FLIGHT, CAR -> Todos en Uppercase, basado en alguno de ellos se obtendrán
        /// las reservas de dicho tipo para determinado viaje con determinada ciudad.
        ///</param>
        ///<returns>
        /// Las reservas por tipo, viaje y ciudad.
        ///</returns>
        ///<exception cref="InvalidReservationType">
        /// Cuando el cliente envía un tipo de reserva no permitido es lanzada esta excepción
        ///</exception>
        ///<exception cref="WithoutTravelReservationsException">
        /// Si el usuario no tiene reservaciones en el viaje para esa ciudad retorna esta excepción
        /// no hay restricciones de si realmente la ciudad pertenece al viaje.
        ///</exception>
        public static List<T> GetReservationsByTravelAndLocation<T>(int travelId, int locationId, string type){
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
                                DateTime.Parse(dataRow[2].ToString())
                            );
                            reservationRoom.Hotel = HotelRepository.GetHotelById(Convert.ToInt32(dataRow[5]));
                            reservationRoom.Fk_user = Convert.ToInt32(dataRow[4]);
                            reservationsOfRoom.Add(reservationRoom);
                        }
                        reservations = reservationsOfRoom.Cast<T>().ToList();
                    }else{
                        throw new WithoutTravelReservationsException(
                            travelId, locationId, "No posee reservaciones de " + type.ToLower() + " en dicha ciudad");
                    }
                }else if(type.Equals("RESTAURANT")){

                }else if(type.Equals("FLIGHT")){

                }else if(type.Equals("CAR")){

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

        ///<sumary>
        /// Consulta las ciudades relacionadas con un viaje
        ///</sumary>
        ///<param name="travelId">Id del viaje con el que se basará la consulta</param>
        ///<returns>
        /// Ciudades o Locations que estan asociadas a un viaje
        ///</returns>
        ///<exception cref="WithoutTravelLocationsException">
        /// Se instancia cuando el viaje no tiene ciudades asociadas
        ///</exception>
        public static List<Location> GetLocationsByTravel(int travelId){
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

        ///<sumary>
        /// Crear una planificación de viaje
        ///</sumary>
        ///<param name="travel">Instancia de Travel, contiene todos los atributos necesarios
        ///   para realizar la creación en la base de datos.</param>
        ///<returns>
        /// El id de Travel que fue creado
        ///</returns>
        ///<exception cref="RequiredAttributeException">
        /// Es excepción es lanzada cuando falta algun campo importante para crear el viaje
        ///</exception>
        ///<exception cref="UserNotFoundException">
        /// Es excepción es lanzada cuando el usuario no existe
        ///</exception>
        public static int AddTravel(Travel travel){
            int id = 0;
            try{
                if( string.IsNullOrEmpty(travel.Name) ||
                    string.IsNullOrEmpty(travel.Description) ||
                    travel.Init == DateTime.MinValue ||
                    travel.End == DateTime.MinValue ) 
                {
                    throw new RequiredAttributeException("Falta información importante para poder crear el viaje");
                }
                User user = UserRepository.GetUserById(travel.UserId); //Throw Exception if it doesn'n exist
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction(
                    "AddTravel(@travelName, @travelInit, @travelEnd, @travelDescription, @userId)", 
                    travel.Name, travel.Init.ToString("yyyy-MM-dd"), travel.End.ToString("yyyy-MM-dd"), travel.Description, travel.UserId);
                id = Convert.ToInt32(dataTable.Rows[0][0]);
                travel.Id = id;
            }catch(DatabaseException ex){
                throw new InternalServerErrorException("Error en el servidor : Conexion a base de datos", ex);
            }catch(InvalidStoredProcedureSignatureException ex){
                throw new InternalServerErrorException("Error en el servidor", ex);
            }
            return id;
        }
        
        public static Boolean AddLocationsToTravel(int travelId, List<Location> locations){
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

        public static Boolean UpdateTravel(Travel travel){
            Boolean saved = false;
            try{
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction(
                    "UpdateTravel(@travelId, @travelName, @travelDescription, @travelInit, @travelEnd)",
                    travel.Id, travel.Name, travel.Description, travel.Init.ToString("yyyy-MM-dd"), travel.End.ToString("yyyy-MM-dd"));
                if(Convert.ToBoolean(dataTable.Rows[0][0])){
                    saved = true;
                }
            }catch(DatabaseException ex){
                //throw new InternalServerErrorException("Error en el servidor : Conexion a base de datos", ex);
                throw new InternalServerErrorException(ex.Message);
            }catch(InvalidStoredProcedureSignatureException ex){
                throw new InternalServerErrorException("Error en el servidor", ex);
            }
            return saved;
        }

    }

}