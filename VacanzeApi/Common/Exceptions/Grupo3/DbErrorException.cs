using System;

namespace vacanze_back.Common.Exceptions.Grupo3
{

    public class DbErrorException : Exception
    {
        public DbErrorException()
        {
        }

        public DbErrorException(string message)
            : base(message)
        {
        }

        public DbErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
