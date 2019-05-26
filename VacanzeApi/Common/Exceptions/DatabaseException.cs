using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class DatabaseException : Exception
    {
        string _Menssage;
        public DatabaseException(string menssage)
        {
            _Menssage = menssage;
        }
    }
}