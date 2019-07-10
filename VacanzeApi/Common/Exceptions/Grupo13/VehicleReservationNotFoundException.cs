using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo13
{
    public class VehicleReservationNotFoundException : GeneralException
    {

        public VehicleReservationNotFoundException()
        {
        }

        /// <summary>
        /// Permite el envio de mensajes con el id de la reservacion de automovil que fue ingresado y no encontrado.
        /// </summary>
        /// <param name="id"></param>
        public VehicleReservationNotFoundException(int id) : base($"El automovil ${id} no fue encontrado ")
        {
        }

    }
}
