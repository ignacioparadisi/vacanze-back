using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo4
{
    public class FlightNotFoundException: Exception
    {

        public FlightNotFoundException()
        {

        }

        public FlightNotFoundException(int id) : base($"Vuelo no encontrado")    
        {

        }


    }
}
