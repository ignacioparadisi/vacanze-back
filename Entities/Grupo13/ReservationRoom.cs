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
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int room_id { get; set; }
        public Room room { get; set; }
        //    public User user { get; set; }

        public ReservationRoom(long id, bool status, DateTime CheckIn, DateTime CheckOut) : base(id)
        {
            this.status = status;
            this.CheckIn = CheckIn;
            this.CheckOut = CheckOut;
        }

        public ReservationRoom(long id, bool status, DateTime CheckIn, DateTime CheckOut, int room_id) : base(id)
        {
            this.status = status;
            this.CheckIn = CheckIn;
            this.CheckOut = CheckOut;
            this.room_id = room_id;
        }
    }
}
