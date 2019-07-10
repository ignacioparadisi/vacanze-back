using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo4
{
    public class ErrorInSaleFlightException : Exception
    {

        public ErrorInSaleFlightException()
        {

        }

        public ErrorInSaleFlightException(int id) : base($"Error registando en ventas el boleto")    
        {

        }


    }
}
