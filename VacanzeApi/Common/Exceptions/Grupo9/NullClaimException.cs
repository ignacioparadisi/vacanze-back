using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class NullClaimException : Exception
    {
        public NullClaimException(string menssage) : base(menssage)
        {
        }
    }
}