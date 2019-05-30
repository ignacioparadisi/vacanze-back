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

        //User who made the reservation
        public User User { get; set; }
        public int Fk_user { get; set; }

        /**
         * Constructors of the class
         */
        public ReservationRoom(long id) : base(id)
        {
        }
        [JsonConstructor]
        public ReservationRoom(long id, DateTime CheckIn, DateTime CheckOut, Hotel hotel, int user_id, User user) : base(id)
        {
            this.CheckIn = CheckIn;
            this.CheckOut = CheckOut;
            this.Hotel = hotel;
            this.Fk_user = user_id;
            this.User = user;
        }

        public ReservationRoom(long id, DateTime CheckIn, DateTime CheckOut) : base(id)
        {
            this.CheckIn = CheckIn;
            this.CheckOut = CheckOut;
        }
    }
}
