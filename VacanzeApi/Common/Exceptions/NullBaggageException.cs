using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class NullBaggageException : Exception
    {
        string _Menssage;
        public NullBaggageException(string menssage)
        {
            _Menssage = menssage;
        }
    }
}