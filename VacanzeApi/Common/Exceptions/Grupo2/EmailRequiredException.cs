using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class EmailRequiredException : GeneralException
    {
        public EmailRequiredException() : base()
        {
        }

        public EmailRequiredException(string message) : base(message)
        {
        }
    }
}