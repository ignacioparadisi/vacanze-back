using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo1
{
    public class LoginUserNotFoundException : Exception
    {
        public LoginUserNotFoundException(string message) : base(message)
        {
            
        }
    }
}