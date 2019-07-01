using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class ClaimNotFoundException : Exception
    {
        public ClaimNotFoundException(string message) : base(message)
        {
        }
    }
}