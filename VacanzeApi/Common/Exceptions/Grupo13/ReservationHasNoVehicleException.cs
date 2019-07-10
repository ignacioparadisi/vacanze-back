namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo13
{
    public class ReservationHasNoVehicleException : GeneralException
    {
        /// <summary>
        /// 
        /// </summary>
        public ReservationHasNoVehicleException() : base("La reserva no tiene asociada ningún auto")
        {
            
        }
    }
}