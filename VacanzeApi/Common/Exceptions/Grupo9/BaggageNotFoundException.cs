using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class BaggageNotFoundException : Exception
    {
        public BaggageNotFoundException(string menssage) : base(menssage)
        {
        }
    }
}