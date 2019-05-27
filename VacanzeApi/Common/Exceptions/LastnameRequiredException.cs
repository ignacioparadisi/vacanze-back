using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class LastnameRequiredException : Exception
    {
        public LastnameRequiredException() : base()
        {
        }

        public LastnameRequiredException(string message) : base(message)
        {
        }
    }
}