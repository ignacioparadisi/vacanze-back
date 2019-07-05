using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class RestaurantNotFoundExeption : Exception
    {
        public RestaurantNotFoundExeption(string message) : base(message)
        {
            
        }
    }
}