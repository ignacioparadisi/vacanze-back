using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo13
{
    public class ReservationRoom : Entity
    {
        //Checkin: Initial date of the reservation
        public DateTime CheckIn { get; set; }
        //Checkout: Last date of the reservation
        public DateTime CheckOut { get; set; }
        //Room which the reservation is tied.
        public Room Room { get; set; }
        private User User { get; set; }

        //public User user { get; set; }

        /**
         * Constructors of the class
         */
        public ReservationRoom(long id) : base(id)
        {
        }
        public ReservationRoom(long id, bool status, DateTime CheckIn, DateTime CheckOut) : base(id)
        {
            this.CheckIn = CheckIn;
            this.CheckOut = CheckOut;
        }

        public ReservationRoom(long id, bool status, DateTime CheckIn, DateTime CheckOut, Room room) : base(id)
        {
            this.CheckIn = CheckIn;
            this.CheckOut = CheckOut;
            this.Room = room;
        }
    }
}
