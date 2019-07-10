using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo13;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo13
{
    public class ReservationVehicleMapper
    {
        /// <summary>
        /// Método para convertir una reserva de vehículo en DTO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>El DTO de reserva de vehículo</returns>
        public ReservationVehicleDTO CreateDTO(ReservationVehicle entity)
        {
            return new ReservationVehicleDTO
            {
                Id = entity.Id,
                CheckIn = entity.CheckIn,
                CheckOut = entity.CheckOut,
                VehicleId = entity.VehicleId,
                UserId = entity.UserId,
                PaymentId = entity.PaymentId
            };
        }
        
        /// <summary>
        /// Método para convertir un DTO en reserva de vehículo
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Una reserva de vehículo</returns>
        public ReservationVehicle CreateEntity(ReservationVehicleDTO dto)
        {
            return EntityFactory.CreateReservationVehicle(dto.Id, dto.CheckIn, dto.CheckOut,
                dto.VehicleId, dto.UserId);
        }
        
        /// <summary>
        /// Método para convertir una lista de reservas de vehículo en una lista de DTO
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>La lista de DTO de reserva de habitación</returns>
        public List<ReservationVehicleDTO> CreateDTOList(List<ReservationVehicle> entities)
        {
            List<ReservationVehicleDTO> dtoList = new List<ReservationVehicleDTO>();
            foreach (var reservation in entities)
            {
                dtoList.Add(new ReservationVehicleDTO
                {
                    Id = reservation.Id,
                    CheckIn = reservation.CheckIn,
                    CheckOut = reservation.CheckOut,
                    VehicleId = reservation.VehicleId,
                    UserId = reservation.UserId,
                    PaymentId = reservation.PaymentId
                });
            }

            return dtoList;
        }  
        
        /// <summary>
        /// Método para convertir una lista de DTO en una lista de reservas de vehículo
        /// </summary>
        /// <param name="dtoList"></param>
        /// <returns>Una lista de reservas de vehículo</returns>
        public List<ReservationVehicle> CreateEntityList(List<ReservationVehicleDTO> dtoList)
        {
            List<ReservationVehicle> entities = new List<ReservationVehicle>();
            foreach (var dto in dtoList)
            {
                entities.Add(EntityFactory.CreateReservationVehicle(dto.Id, dto.CheckIn, dto.CheckOut,
                    dto.VehicleId, dto.UserId));
            }

            return entities;
        }
    }
}