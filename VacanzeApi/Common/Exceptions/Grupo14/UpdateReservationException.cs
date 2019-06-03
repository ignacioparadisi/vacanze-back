using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class UpdateReservationException : Exception
    {
        public UpdateReservationException(string message): base(message)
        {
        }
    }
}