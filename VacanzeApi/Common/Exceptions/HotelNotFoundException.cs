using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class HotelNotFoundException : Exception
    {
        public HotelNotFoundException()
        {
        }

        public HotelNotFoundException(int id) : base($"Id invalido: {id}")
        {
        }
    }
}