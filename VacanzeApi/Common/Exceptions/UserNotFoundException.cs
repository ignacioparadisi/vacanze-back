using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class UserNotFoundException : GeneralException
    {
        public UserNotFoundException(string message) : base(message)
        {
        }
    }
}