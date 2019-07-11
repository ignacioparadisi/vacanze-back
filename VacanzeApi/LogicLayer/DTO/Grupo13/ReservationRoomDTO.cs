using System;

namespace vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo13
{
    public class ReservationRoomDTO
    {
        /// <summary>
        /// ID de la reservación de habitación
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Fecha de checkin de la reservación de habitación
        /// </summary>
        public DateTime CheckIn { get; set; }
        
        /// <summary>
        /// Fecha de checkout de la reservación de habitación
        /// </summary>
        public DateTime CheckOut { get; set; }
        
        /// <summary>
        /// ID del hotel al cual pertenece la reservación
        /// </summary>
        public int HotelId { get; set; }

        /// <summary>
        /// ID del usuario a quien pertenece la reservación
        /// </summary>
        public int UserId { get; set; }

        public int PaymentId { get; set; }
    }
}