using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class GetRestaurantExcepcion : Exception
    {
        public GetRestaurantExcepcion(string message): base(message)
        {
        }
    }
}