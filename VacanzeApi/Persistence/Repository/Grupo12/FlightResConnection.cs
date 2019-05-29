using System;
using System.Collections.Generic;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo12;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo12;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo12
{
    public class  FlightResConnection : Connection
    {

        public string AddReservationFlight(Entity entity){
            try{
              Connect();
              var addReservation=(FlightRes) entity;

               return ""; 
                
            }catch (NpgsqlException ex){
                Console.WriteLine(ex.ToString());
                throw new DBFailedException("Disculpe,Ocurrio un Error en la Base de Datos");
            }finally{
                Disconnect();
            }
        }



    }
}