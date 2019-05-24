using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.Entities;

namespace vacanze_back.Entities.Grupo13
{
    public class ReservationRoom : Entity
    {
        public bool status { get; set; }
        //Checkin: Initial date of the reservation
        public DateTime checkIn { get; set; }
        //Checkout: Last date of the reservation
        public DateTime checkOut { get; set; }
        //Room which the reservation is tied.
        public Room room { get; set; }

        //public User user { get; set; }

            /**
             * Constructors of the class
             */
        public ReservationRoom(long id, bool status, DateTime CheckIn, DateTime CheckOut) : base(id)
        {
            this.status = status;
            this.checkIn = CheckIn;
            this.checkOut = CheckOut;
        }

        public ReservationRoom(long id, bool status, DateTime CheckIn, DateTime CheckOut, Room room) : base(id)
        {
            this.status = status;
            this.checkIn = CheckIn;
            this.checkOut = CheckOut;
            this.room = room;
        }
    }
}
