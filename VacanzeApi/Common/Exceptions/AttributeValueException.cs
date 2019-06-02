using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class AttributeValueException : Exception
    {
        public AttributeValueException(string message) : base(message)
        {
        }
    }
}