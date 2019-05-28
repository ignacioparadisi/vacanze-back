using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo13
{

    public class ReservationAutomobile : Entity
    {
        //Checkin: Initial date of the reservation
        public DateTime CheckIn { get; set; }
        //Checkout: Last date of the reservation
        public DateTime CheckOut { get; set; }
        //Automobile which the reservation references
        public Automobile Automobile { get; set; }
        //User who made the reservation
        public User User { get; set; }

            /**
             * Constructor of the class.
             */
        public ReservationAutomobile(long id, DateTime CheckIn, DateTime CheckOut) : base(id)
        {
            this.CheckIn = CheckIn;
            this.CheckOut = CheckOut;
        }
    }
}
