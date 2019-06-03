using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class DeleteReservationException : Exception
    {
        public DeleteReservationException(string message): base(message)
        {
        }
    }
}