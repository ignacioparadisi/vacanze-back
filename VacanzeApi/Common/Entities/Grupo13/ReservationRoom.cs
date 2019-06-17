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
        public Hotel Hotel { get; set; }

        public int Fk_user { get; set; }

        public int Fk_pay{ get; set; }
        public User User { get; set; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase ReservationRoom.
        /// </summary>
//        public ReservationRoom(int id) : base(id)
//        {
//        }
        [JsonConstructor]
        
        public ReservationRoom(int id, DateTime CheckIn, DateTime CheckOut, Hotel hotel, int user_id, User user) : base(id)
        {
            this.CheckIn = CheckIn;
            this.CheckOut = CheckOut;
            this.Hotel = hotel;
            this.Fk_user = user_id;
            this.User = user;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase ReservationRoom.
        /// </summary>
        public ReservationRoom(int id, DateTime CheckIn, DateTime CheckOut) : base(id)
        {
            this.CheckIn = CheckIn;
            this.CheckOut = CheckOut;
        }

        public ReservationRoom(int payment) : base(0)
        {
            this.Fk_pay = payment;
        }

        public ReservationRoom(int id, DateTime CheckIn, DateTime CheckOut, Hotel hotel, int user_id) : base(id)
        {
            this.CheckIn = CheckIn;
            this.CheckOut = CheckOut;
            this.Hotel = hotel;
            this.Fk_user = user_id;
        }
    }
}
