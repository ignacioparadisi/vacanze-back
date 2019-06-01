using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class LastnameRequiredException : GeneralException
    {
        public LastnameRequiredException() : base()
        {
        }

        public LastnameRequiredException(string message) : base(message)
        {
        }
    }
}