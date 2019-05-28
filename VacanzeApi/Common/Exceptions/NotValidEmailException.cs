using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class NotValidEmailException : Exception
    {
        public NotValidEmailException() : base()
        {
        }

        public NotValidEmailException(string message) : base(message)
        {
        }
    }
}