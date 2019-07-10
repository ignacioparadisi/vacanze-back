using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo13;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo13
{
    public class ReservationRoomMapper : Mapper<ReservationRoomDTO, ReservationRoom>
    {
        /// <summary>
        /// Método para convertir una reserva de habitación en DTO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>El DTO de reserva de habitación</returns>
        public ReservationRoomDTO CreateDTO(ReservationRoom entity)
        {
            return new ReservationRoomDTO
            {
                Id = entity.Id,
                CheckIn = entity.CheckIn,
                CheckOut = entity.CheckOut,
                HotelId = entity.HotelId,
                UserId = entity.UserId,
                PaymentId = entity.PaymentId
            };
        }
        
        /// <summary>
        /// Método para convertir un DTO en reserva de habitación
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Una reserva de habitación</returns>
        public ReservationRoom CreateEntity(ReservationRoomDTO dto)
        {
            return EntityFactory.CreateReservationRoom(dto.Id, dto.CheckIn, dto.CheckOut,
                dto.HotelId, dto.UserId);
        }
        
        /// <summary>
        /// Método para convertir una lista de reservas de habitación en una lista de DTO
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>La lista de DTO de reserva de habitación</returns>
        public List<ReservationRoomDTO> CreateDTOList(List<ReservationRoom> entities)
        {
            List<ReservationRoomDTO> dtoList = new List<ReservationRoomDTO>();
            foreach (var reservation in entities)
            {
                dtoList.Add(new ReservationRoomDTO
                {
                    Id = reservation.Id,
                    CheckIn = reservation.CheckIn,
                    CheckOut = reservation.CheckOut,
                    HotelId = reservation.HotelId,
                    UserId = reservation.UserId,
                    PaymentId = reservation.PaymentId
                });
            }

            return dtoList;
        }  
        
        /// <summary>
        /// Método para convertir una lista de DTO en una lista de reservas de habitación
        /// </summary>
        /// <param name="dtoList"></param>
        /// <returns>Una lista de reservas de habitación</returns>
        public List<ReservationRoom> CreateEntityList(List<ReservationRoomDTO> dtoList)
        {
            List<ReservationRoom> entities = new List<ReservationRoom>();
            foreach (var dto in dtoList)
            {
                entities.Add(EntityFactory.CreateReservationRoom(dto.Id, dto.CheckIn, dto.CheckOut,
                    dto.HotelId, dto.UserId));
            }

            return entities;
        }
    }
}