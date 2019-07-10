namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo13
{
    public class ReservationHasNoCheckOutException : GeneralException
    {
        public ReservationHasNoCheckOutException() : base("La reserva no tiene fecha de check out")
        {
            
        }
    }
}