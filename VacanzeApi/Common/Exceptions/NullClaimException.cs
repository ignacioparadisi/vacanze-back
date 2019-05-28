using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class NullClaimException : Exception
    {
        string _Menssage;
        public NullClaimException(string menssage)
        {
            _Menssage = menssage;
        }
    }
}