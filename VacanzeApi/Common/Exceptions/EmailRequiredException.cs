using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class EmailRequiredException : Exception
    {
        public EmailRequiredException() : base()
        {
        }

        public EmailRequiredException(string message) : base(message)
        {
        }
    }
}