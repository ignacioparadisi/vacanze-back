namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo13
{
    public class ReservationHasNoHotelException : GeneralException
    {
        public ReservationHasNoHotelException() : base("La Reserva no Tiene Asociado un Hotel")
        {
        }
    }
}