using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class GeneralException :Exception
    {
        public GeneralException(string message) : base(message)
        {
        }
    }
}