using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class GetRestaurantExcepcion : Exception
    {
        string _Menssage;
        public GetRestaurantExcepcion(string menssage)
        {
            _Menssage = menssage;
        }
    }
}