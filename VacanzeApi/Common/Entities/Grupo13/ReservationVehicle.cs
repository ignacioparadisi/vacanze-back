using System;
using Newtonsoft.Json;
using vacanze_back.VacanzeApi.Common.Entities.Grupo11;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo13
{

    public class ReservationVehicle : Entity
    {
        //CheckIn: Initial date of the reservation
        public DateTime CheckIn { get; set; }
        //CheckOut: Last date of the reservation
        public DateTime CheckOut { get; set; }
        //Automobile which the reservation references
        public int VehicleId { get; set; }
       
        public int UserId{ get; set; }

        public int PaymentId{ get; set; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase ReservationAutomobile.
        /// </summary>
        [JsonConstructor]
        public ReservationVehicle(int id, DateTime CheckIn, DateTime CheckOut) : base(id)
        {
            this.CheckIn = CheckIn;
            this.CheckOut = CheckOut;
        }

        public ReservationVehicle(int id, DateTime checkIn, DateTime checkOut, int vehicleId, int userId) : base(id)
        {
            CheckIn = checkIn;
            CheckOut = checkOut;
            UserId = userId;
            VehicleId = vehicleId;
        }

        public ReservationVehicle() : base(0)
        {
            
        }

        public ReservationVehicle(int paymentId) : base(0)
        {
            PaymentId = paymentId;
        }
    }
}
