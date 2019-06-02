using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class RequiredAttributeException : Exception
    {
        public RequiredAttributeException(string message) : base(message)
        {
        }
    }
}