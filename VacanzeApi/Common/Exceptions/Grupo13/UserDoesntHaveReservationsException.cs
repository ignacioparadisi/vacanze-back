namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo13
{
    public class UserDoesntHaveReservationsException : GeneralException
    {
        public UserDoesntHaveReservationsException() : base("El usuario no tiene ninguna reservación")
        {
            
        }
    }
}