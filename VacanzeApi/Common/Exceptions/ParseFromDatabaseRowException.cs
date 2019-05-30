using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class ParseFromDatabaseRowException : Exception
    {
        public ParseFromDatabaseRowException(string message) : base(message)
        {
        }
    }
}