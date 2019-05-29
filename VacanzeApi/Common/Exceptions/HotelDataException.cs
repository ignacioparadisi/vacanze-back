using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class HotelDataException : Exception
    {
        public HotelDataException(string message) : base(message)
        {
        }
    }
}