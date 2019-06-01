using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class RepeatedEmailException : GeneralException
    {
        public RepeatedEmailException(string message) : base(message)
        {
        }
    }
}