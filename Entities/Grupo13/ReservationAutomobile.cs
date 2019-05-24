using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.Entities;

namespace vacanze_back.Entities.Grupo13
{

    public class ReservationAutomobile : Entity
    {
        private bool status { get; set; }
        private DateTime CheckIn { get; set; }
        private DateTime CheckOut { get; set; }
        //  private Automobile automobile { get; set; }

        public void setStatus(bool status)
        {
            this.status = status;
        }

        public void setCheckIn(DateTime date)
        {
            this.CheckIn = date;
        }
        public void setCheckOut(DateTime date)
        {
            this.CheckOut = date;
        }

        public bool getStatus()
        {
            return this.status;
        }

        public DateTime getCheckIn()
        {
            return this.CheckIn;
        }

        public DateTime getCheckOut()
        {
            return this.CheckOut;
        }

        public ReservationAutomobile(long id, bool status, DateTime CheckIn, DateTime CheckOut) : base(id)
        {
            this.status = status;
            this.CheckIn = CheckIn;
            this.CheckOut = CheckOut;
        }
    }
}
