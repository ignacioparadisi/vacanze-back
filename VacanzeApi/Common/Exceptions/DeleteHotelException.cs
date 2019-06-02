using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class DeleteHotelException : Exception
    {
        public DeleteHotelException()
        {
        }

        public DeleteHotelException(string message) : base(message)
        {
        }
    }
}