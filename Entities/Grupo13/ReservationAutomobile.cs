using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vacanze_back.Entities.Grupo13
{

    public class ReservationAutomobile : Entity
    {
        private bool status;
        private DateTime CheckIn;
        private DateTime CheckOut;

        public void setStatus(bool estatus)
        {
            this.status = estatus;
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

        public ReservationAutomobile()
        {

        }

        public ReservationAutomobile(long id, bool status, DateTime CheckIn, DateTime CheckOut)
        {
            setId(id);
            this.status = status;
            this.CheckIn = CheckIn;
            this.CheckOut = CheckOut;
        }
    }
}
