using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo4
{
    public class ErrorCheckBaggageException : Exception
    {

        public ErrorCheckBaggageException()
        {

        }

            public ErrorCheckBaggageException(int id) : base($"Error registando maletas")    
        {

        }


    }
}
