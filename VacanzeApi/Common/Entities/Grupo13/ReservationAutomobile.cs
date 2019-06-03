using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo13
{

    public class ReservationAutomobile : Entity
    {
        //CheckIn: Initial date of the reservation
        public DateTime CheckIn { get; set; }
        //CheckOut: Last date of the reservation
        public DateTime CheckOut { get; set; }
        //Automobile which the reservation references
        public Auto Automobile { get; set; }
       
        public int Fk_user { get; set; }

        public int Fk_pay{ get; set; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase ReservationAutomobile.
        /// </summary>
        public ReservationAutomobile(long id, DateTime CheckIn, DateTime CheckOut) : base(id)
        {
            this.CheckIn = CheckIn;
            this.CheckOut = CheckOut;
        }
    }
}
