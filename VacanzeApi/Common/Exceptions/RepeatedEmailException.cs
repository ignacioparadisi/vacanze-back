using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class RepeatedEmailException : Exception
    {
        string _Menssage;
        public RepeatedEmailException(string message)
        {
            _Menssage = message;
        }
    }
}