using System;

namespace vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo13
{
    public class ReservationVehicleDTO
    {
        /// <summary>
        /// ID de la reservación de vehículo
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Fecha de inicio de alquiler del vehículo
        /// </summary>
        public DateTime CheckIn { get; set; }
        
        /// <summary>
        /// Fecha de retorno del vehículo
        /// </summary>
        public DateTime CheckOut { get; set; }
        
        /// <summary>
        /// ID del vehículo al cual pertenece la reservación
        /// </summary>
        public int VehicleId { get; set; }

        /// <summary>
        /// ID del usuario a quien pertenece la reservación
        /// </summary>
        public int UserId { get; set; }

        public int PaymentId { get; set; }
    }
}