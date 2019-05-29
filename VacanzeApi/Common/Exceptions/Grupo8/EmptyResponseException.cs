using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo8
{
    public class EmptyResponseException : Exception
    {
        public EmptyResponseException(string message) : base(message)
            {
                
            }
    }
}