using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class NotValidIdException : Exception
    {
        public NotValidIdException() : base()
        {
        }

        public NotValidIdException(string message) : base(message)
        {
        }
    }
}