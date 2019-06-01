using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class PasswordRequiredException : GeneralException
    {
        public PasswordRequiredException(string message) : base(message)
        {
        }
    }
}