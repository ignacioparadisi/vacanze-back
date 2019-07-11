namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo13
{
    public class ReservationHasNoCheckInException : GeneralException
    {
        public ReservationHasNoCheckInException() : base("La reserva no tiene fecha de check in")
        {
            
        }
    }
}