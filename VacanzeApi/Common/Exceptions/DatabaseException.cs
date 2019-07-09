using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException(string message) : base(message)
        {
        }

        public DatabaseException(): base("Error en base de datos")
        {
        }
    }
}