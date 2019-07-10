using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class LocationNotFoundException : Exception
    {
        public LocationNotFoundException(int id) : base($"Id invalido: {id}")
        {
        }
    }
}