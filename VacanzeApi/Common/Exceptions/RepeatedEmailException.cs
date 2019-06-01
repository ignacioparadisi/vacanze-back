using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class RepeatedEmailException : Exception
    {
        public RepeatedEmailException(string message) : base(message)
        {
        }
    }
}