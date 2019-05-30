using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class NullBaggageException : Exception
    {
        public NullBaggageException(string menssage): base(menssage)
        {
           
        }
    }
}