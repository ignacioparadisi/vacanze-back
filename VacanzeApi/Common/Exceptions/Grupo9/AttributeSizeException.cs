using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class AttributeSizeException : Exception
    {
        public AttributeSizeException(string message) : base(message)
        {
        }
    }
}