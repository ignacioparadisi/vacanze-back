using System;
using System.Collections.Generic;
using Npgsql;
using System.Data;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo12;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo12;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo12
{
    public class  FlightResConnection 
    {

        public int AddReservationFlight(FlightRes resflight){
              
             var table = PgConnection.Instance.ExecuteFunction(
             "AddReservationFlight(@seatNum,@tim,@numPas,@id_user,@pay,@id_fli)",
              resflight._seatNum,resflight._timestamp,resflight._numPas,
              resflight._id_user,resflight._id_pay,resflight._id_fli);
              
              var id = Convert.ToInt32(table.Rows[0][0]);
            return id;
           
        }



    }
}