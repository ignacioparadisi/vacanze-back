using System;
using System.Data;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo5;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo5
{
    public class PostgresVehicleDAO : IVehicleDAO
    {
        public int AddVehicle(Vehicle vehicle){
            int id = 0;
            try {
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction (
                "AddVehicle(@modelId, @locationId, @license, @price, @status)", 
                    vehicle.VehicleModelId, vehicle.VehicleLocationId, vehicle.License, vehicle.Price, vehicle.Status);
                id = Convert.ToInt32 (dataTable.Rows[0][0]);
            } catch (DatabaseException ex) {
                //throw new InternalServerErrorException ("Error en el servidor : Conexion a base de datos", ex);
                throw new InternalServerErrorException (ex.Message);
            } catch (InvalidStoredProcedureSignatureException ex) {
                throw new InternalServerErrorException ("Error en el servidor", ex);
            }
            return id;
        }

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
    }
}