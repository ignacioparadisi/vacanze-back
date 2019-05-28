using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message)
        {
        }
    }
}