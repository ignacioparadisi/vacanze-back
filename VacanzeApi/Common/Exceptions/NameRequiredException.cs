using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class NameRequiredException : Exception
    {
        public NameRequiredException() : base()
        {
        }

        public NameRequiredException(string message) : base(message)
        {
        }
    }
}