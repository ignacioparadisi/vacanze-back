using System;
using Newtonsoft.Json;
using vacanze_back.VacanzeApi.Common.Entities.Grupo11;
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
       
        public int UserId{ get; set; }

        public int PaymentId{ get; set; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase ReservationAutomobile.
        /// </summary>
        [JsonConstructor]
        public ReservationAutomobile(int id, DateTime CheckIn, DateTime CheckOut) : base(id)
        {
            this.CheckIn = CheckIn;
            this.CheckOut = CheckOut;
        }

        public ReservationAutomobile(int id, DateTime CheckIn, DateTime CheckOut, Auto automobile, int usuario) : base(id)
        {
            this.CheckIn = CheckIn;
            this.CheckOut = CheckOut;
            this.UserId = usuario;
            this.Automobile = automobile;
        }

        public ReservationAutomobile(int payment) : base(0)
        {
            this.PaymentId = payment;
        }
    }
}
