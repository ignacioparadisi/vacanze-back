using System;
using System.Data;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo5;
using vacanze_back.VacanzeApi.Persistence.DAO.Locations;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo5
{
    public class PostgresVehicleDAO : IVehicleDAO
    {   
        ///<sumary>Creación de un nuevo vehiculo</sumary>
        ///<param name="vehicle">Instancia de Vehicle</param>
        ///<returns>Id del vehiculo</returns>
        ///<exception cref="ModelNotFoundException"> Si no existe el modelo </exception>
        ///<exception cref="LocationNotFoundException"> Si no existe la ciudad </exception>
        ///<exception cref="UniqueAttributeException">
        /// Es excepción es lanzada cuando ya existe un carro con dicha matricula
        ///</exception>
        public int AddVehicle(Vehicle vehicle){
            int id = 0;
            try {
                PostgresModelDAO modelDAO = new PostgresModelDAO();
                modelDAO.GetModelById(vehicle.VehicleModelId);//Throw ModelNotFoundException
                PostgresLocationDAO locationDAO = new PostgresLocationDAO();
                locationDAO.GetLocationById(vehicle.VehicleLocationId);// Throw LocationNotFoundException
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction (
                    "LicenseUniqueness(@modelName)", vehicle.License);
                if(Convert.ToBoolean(dataTable.Rows[0][0])){
                    throw new UniqueAttributeException("La matricula " + vehicle.License + " ya existe");
                }else{
                    dataTable = pgConnection.ExecuteFunction (
                    "AddVehicle(@modelId, @locationId, @license, @price, @status)", 
                        vehicle.VehicleModelId, vehicle.VehicleLocationId, vehicle.License, vehicle.Price, vehicle.Status);
                    id = Convert.ToInt32 (dataTable.Rows[0][0]);
                }
            } catch (DatabaseException ex) {
                throw new InternalServerErrorException ("Error en el servidor : Conexion a base de datos", ex);
            } catch (InvalidStoredProcedureSignatureException ex) {
                throw new InternalServerErrorException ("Error en el servidor", ex);
            }
            return id;
        }

        ///<sumary>Obtener vehiculos por su Id</sumary>
        ///<param name="vehicleId">Id del vehiculo</param>
        ///<returns>Instancia del Vehiculo</returns>
        ///<exception cref="VehicleNotFoundException"> Si no existe el vehiculo </exception>
        public Vehicle GetVehicleById(int vehicleId){
            Vehicle vehicle = null;
            try{
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction("GetVehicleById(@vehicleId)", vehicleId);
                if(dataTable.Rows.Count == 0){
                    throw new VehicleNotFoundException(vehicleId, "No se ha encontrado un vehiculo con Id ");
                }
                else{
                    vehicle = new Vehicle(
                        Convert.ToInt32(dataTable.Rows[0][0]),
                        Convert.ToInt32(dataTable.Rows[0][1]),
                        Convert.ToInt32(dataTable.Rows[0][2]),
                        dataTable.Rows[0][3].ToString(),
                        Convert.ToDouble(dataTable.Rows[0][4]), 
                        true
                    );
                }
            }catch(DatabaseException ex){
                throw new InternalServerErrorException("Error en el servidor : Conexion a base de datos", ex);
            }catch(InvalidStoredProcedureSignatureException ex){
                throw new InternalServerErrorException("Error en el servidor", ex);
            }
            return vehicle;
        }

        ///<sumary>Obtener vehiculos disponibles por ciudad</sumary>
        ///<param name="locationId">Id de la ciudad</param>
        ///<returns>Lista de vehiculos disponibles</returns>
        ///<exception cref="LocationNotFoundException"> Si no existe la ciudad </exception>
        ///<exception cref="NotVehiclesAvailableException"> 
        /// Si no hay existencia de vehiculos disponibles
        ///</exception>
        public List<Vehicle> GetAvailableVehiclesByLocation(int locationId){
            List<Vehicle> vehicles = new List<Vehicle>();
            try{
                PostgresLocationDAO locationDAO = new PostgresLocationDAO();
                Location location = locationDAO.GetLocationById(locationId);// Throw LocationNotFoundException
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction("GetAvailableVehiclesByLocation(@locationId)", locationId);
                if( dataTable.Rows.Count > 0){
                    foreach (DataRow dataRow in dataTable.Rows){
                        Vehicle vehicle = new Vehicle(
                            Convert.ToInt32(dataRow[0]),
                            Convert.ToInt32(dataRow[1]),
                            Convert.ToInt32(dataRow[2]),
                            dataRow[3].ToString(),
                            Convert.ToDouble(dataRow[4]),
                            Convert.ToBoolean(dataRow[5])
                        );
                        vehicle.VehicleModel = 
                            new PostgresModelDAO().GetModelById(vehicle.VehicleModelId);
                        vehicle.VehicleLocation = 
                            new PostgresLocationDAO().GetLocationById(vehicle.VehicleLocationId);
                        vehicles.Add(vehicle);
                    }
                }else{
                    throw new NotVehiclesAvailableException(locationId, 
                        "No existen Vehiculos disponibles en " +  location.City);
                }

            }catch(DatabaseException ex){
                throw new InternalServerErrorException("Error en el servidor : Conexion a base de datos", ex);
            }catch(InvalidStoredProcedureSignatureException ex){
                throw new InternalServerErrorException("Error en el servidor", ex);
            }
            return vehicles;
        }

        ///<sumary>Obtener todos los vehiculos</sumary>
        ///<returns>Lista de vehiculos</returns>
        ///<exception cref="NotVehiclesAvailableException"> 
        /// Si no hay existencia de vehiculos disponibles
        ///</exception>
        public List<Vehicle> GetVehicles(){
            List<Vehicle> vehicles = new List<Vehicle>();
               try{
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction("GetVehicles()");
                if( dataTable.Rows.Count > 0){
                    foreach (DataRow dataRow in dataTable.Rows){
                        Vehicle vehicle = new Vehicle(
                            Convert.ToInt32(dataRow[0]),
                            Convert.ToInt32(dataRow[1]),
                            Convert.ToInt32(dataRow[2]),
                            dataRow[3].ToString(),
                            Convert.ToDouble(dataRow[4]),
                            Convert.ToBoolean(dataRow[5])
                        );
                        vehicle.VehicleLocation =
                            new PostgresLocationDAO().GetLocationById(vehicle.VehicleLocationId);
                        vehicle.VehicleModel =
                            new PostgresModelDAO().GetModelById(vehicle.VehicleModelId);
                        vehicles.Add(vehicle);
                    }
                }else{
                    throw new NotVehiclesAvailableException("No hay existencia de vehiculos");
                }

            }catch(DatabaseException ex){
                throw new InternalServerErrorException("Error en el servidor : Conexion a base de datos", ex);
            }catch(InvalidStoredProcedureSignatureException ex){
                throw new InternalServerErrorException("Error en el servidor", ex);
            }

            return vehicles;
        }
        
        ///<sumary>Modificar un vehiculo</sumary>
        ///<param name="vehicle">Instancia de Vehicle</param>
        ///<returns>True si actualizo correctamente</returns>
        ///<exception cref="VehicleNotFoundException"> Si el vehiculo no existe </exception>
        ///<exception cref="ModelNotFoundException"> Si el modelo no existe </exception>
        ///<exception cref="LocationNotFoundException"> Si la ciudad no existe </exception>
        public bool UpdateVehicle(Vehicle vehicle){
            bool updated = false;
            try{
                GetVehicleById(vehicle.Id);// Throw VehicleNotFoundException
                PostgresModelDAO modelDAO = new PostgresModelDAO();
                modelDAO.GetModelById(vehicle.VehicleModelId);//Throw ModelNotFoundException
                PostgresLocationDAO locationDAO = new PostgresLocationDAO();
                locationDAO.GetLocationById(vehicle.VehicleLocationId);// Throw LocationNotFoundException
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction(
                "UpdateVehicle(@vehid, @vehmodel, @vehlocation, @vehlicense, @vehprice, @vehstatus)",
                vehicle.Id, vehicle.VehicleModelId, vehicle.VehicleLocationId, vehicle.License, vehicle.Price, vehicle.Status);
                if(Convert.ToBoolean(dataTable.Rows[0][0])){
                    updated = true;
                }
            }catch(DatabaseException ex){
                throw new InternalServerErrorException("Error en el servidor : Conexion a base de datos", ex);
            }catch(InvalidStoredProcedureSignatureException ex){
                throw new InternalServerErrorException("Error en el servidor", ex);
            }
            return updated;
        }

        ///<sumary>Modificar el estatus de un vehiculo</sumary>
        ///<param name="vehicleId">Id del vehiculo</param>
        ///<param name="status">Bool del status</param>
        ///<returns>True si actualizo correctamente</returns>
        ///<exception cref="VehicleNotFoundException"> Si el vehiculo no existe </exception>
        public bool UpdateVehicleStatus(int vehicleId, bool status){
            bool updated = false;
            try{
                GetVehicleById(vehicleId);// Throw VehicleNotFoundException
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction("UpdateVehicleStatus(@vehid, @vehstatus)",
                    vehicleId, status);
                if(Convert.ToBoolean(dataTable.Rows[0][0])){
                    updated = true;
                }
            }catch(DatabaseException ex){
                throw new InternalServerErrorException("Error en el servidor : Conexion a base de datos", ex);
            }catch(InvalidStoredProcedureSignatureException ex){
                throw new InternalServerErrorException("Error en el servidor", ex);
            }
            return updated;
        }
    }
}