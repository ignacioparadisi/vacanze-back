using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class PasswordRequiredException : Exception
    {
        public PasswordRequiredException(string message) : base(message)
        {
        }
    }
}