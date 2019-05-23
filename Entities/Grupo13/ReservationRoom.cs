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

        public void setStatus(bool status)
        {
            this.status = status;
        }

        public void setCheckIn(DateTime CheckIn)
        {
            this.CheckIn = CheckIn;
        }
        public void setFechaCheckOut(DateTime CheckOut)
        {
            this.CheckOut = CheckOut;
        }

        public bool getStatus()
        {
            return this.status;
        }

        public DateTime getCheckIn()
        {
            return this.CheckIn;
        }

        public DateTime getheckOut()
        {
            return this.CheckOut;
        }
        public ReservationRoom()
        {
        }

        public ReservationRoom(long id, bool status, DateTime CheckIn, DateTime CheckOut)
        {
            setId(id);
            this.status = status;
            this.CheckIn = CheckIn;
            this.CheckOut = CheckOut;
        }
    }
}
