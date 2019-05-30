using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class DeleteEntityException : Exception
    {
        public DeleteEntityException()
        {
        }

        public DeleteEntityException(string message) : base(message)
        {
        }
    }
}