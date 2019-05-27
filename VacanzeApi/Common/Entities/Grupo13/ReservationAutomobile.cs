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
        //Checkin: Initial date of the reservation
        private DateTime CheckIn { get; set; }
        //Checkout: Last date of the reservation
        private DateTime CheckOut { get; set; }
        // private Automobile automobile { get; set; }
        // private User user { get; set; }

            /**
             * Constructor of the class.
             */
        public ReservationAutomobile(long id, bool status, DateTime CheckIn, DateTime CheckOut) : base(id)
        {
            this.status = status;
            this.CheckIn = CheckIn;
            this.CheckOut = CheckOut;
        }
    }
}
