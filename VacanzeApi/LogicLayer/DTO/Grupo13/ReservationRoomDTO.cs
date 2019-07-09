using System;

namespace vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo13
{
    public class ReservationRoomDTO
    {
        public int Id { get; set; }
        
        public DateTime CheckIn { get; set; }
        
        public DateTime CheckOut { get; set; }
        
        public int HotelId { get; set; }

        public int UserId { get; set; }

        public int PayId { get; set; }
    }
}