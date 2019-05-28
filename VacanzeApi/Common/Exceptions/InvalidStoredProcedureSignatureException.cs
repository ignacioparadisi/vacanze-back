using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class InvalidStoredProcedureSignatureException : Exception
    {
        public InvalidStoredProcedureSignatureException(string message) : base(message)
        {
        }
    }
}