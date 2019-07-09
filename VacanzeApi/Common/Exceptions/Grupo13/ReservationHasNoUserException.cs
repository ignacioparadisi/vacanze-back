namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo13
{
    public class ReservationHasNoUserException : GeneralException
    {
        /// <summary>
        /// 
        /// </summary>
        public ReservationHasNoUserException() : base("La reserva no tiene asociado un usuario")
        {
            
        }
    }
}