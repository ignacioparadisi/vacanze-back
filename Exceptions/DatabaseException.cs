using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vacanze_back.Exceptions
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