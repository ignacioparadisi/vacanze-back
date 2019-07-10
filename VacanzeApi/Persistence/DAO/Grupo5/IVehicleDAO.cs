using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo5
{
    public interface IVehicleDAO
    {
        int AddVehicle(Vehicle vehicle);
        Vehicle GetVehicleById(int vehicleId);
        List<Vehicle> GetAvailableVehiclesByLocation(int locationId);
        bool UpdateVehicle(Vehicle vehicle);
    } 
}