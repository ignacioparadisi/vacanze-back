using System.Collections.Generic;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo5;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.LogicLayer.DTO;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo5
{
    public class VehicleMapper : Mapper<VehicleDTO, Vehicle>
    {
        public VehicleDTO CreateDTO(Vehicle vehicle){
            VehicleDTO vehicleDTO = DTOFactory.CreateVehicleDTO(
                vehicle.Id, vehicle.VehicleModelId, vehicle.VehicleLocationId,
                vehicle.License, vehicle.Price, vehicle.Status
            );
            return vehicleDTO;
        }

        public Vehicle CreateEntity(VehicleDTO dto){
            Vehicle vehicle = EntityFactory.CreateVehicle(
                dto.Id, dto.VehicleModelId, dto.VehicleLocationId,
                dto.License, dto.Price, dto.Status
            );
            return vehicle;
        }

        public List<VehicleDTO> CreateDTOList(List<Vehicle> vehicles){
            List<VehicleDTO> dtos = new List<VehicleDTO>();
            foreach(Vehicle vehicle in vehicles){
                dtos.Add(DTOFactory.CreateVehicleDTO(
                    vehicle.Id, vehicle.VehicleModelId, vehicle.VehicleLocationId,
                    vehicle.License, vehicle.Price, vehicle.Status)
                );
            }
            return dtos;
        }

        public List<Vehicle> CreateEntityList(List<VehicleDTO> dtos){
            List<Vehicle> vehicles = new List<Vehicle>();
            foreach(VehicleDTO dto in dtos){
                vehicles.Add(EntityFactory.CreateVehicle(
                    dto.Id, dto.VehicleModelId, dto.VehicleLocationId,
                    dto.License, dto.Price, dto.Status)
                );
            }
            return vehicles;
        }
    }
}