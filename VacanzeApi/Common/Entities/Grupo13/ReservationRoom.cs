using Newtonsoft.Json;
using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo13
{
    public class ReservationRoom : Entity
    {
        //Checkin: Initial date of the reservation
        public DateTime CheckIn { get; set; }
        //Checkout: Last date of the reservation
        public DateTime CheckOut { get; set; }
        //Hotel of the reservation
        public int HotelId { get; set; }

        public int UserId { get; set; }

        public int PaymentId { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase ReservationRoom.
        /// </summary>
        /// 
        public ReservationRoom() : base(0)
        {
        }
        
        [JsonConstructor]
        public ReservationRoom(int id, DateTime checkIn, DateTime checkOut, int hotelId, int userId) : base(id)
        {
            CheckIn = checkIn;
            CheckOut = checkOut;
            HotelId = hotelId;
            UserId = userId;
        }
    }
}
