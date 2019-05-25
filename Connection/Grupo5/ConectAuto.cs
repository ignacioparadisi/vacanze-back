using System;
using System.Collections.Generic;
using Npgsql;      
using vacanze_back.Entities.Grupo5;
namespace vacanze_back.Connection.Grupo5
{
    public class ConnectAuto:Connection
    {
        public ConnectAuto()
        {
            CreateStringConnection();
        }

        public void Agregar(Auto auto)
        {
            Connect();
            StoredProcedure("AgregarAutomovil (@aut_make,@aut_model,@aut_capacity,@aut_isActive,@aut_licence,@aut_price)");
           AddParameter("aut_make", auto.getmake().ToString());
           AddParameter("aut_model", auto.getmodel().ToString());
           AddParameter("aut_capacity", auto.getcapacity());
           AddParameter("aut_isActive", auto.getisActive());
           AddParameter("aut_licence", auto.getlicence().ToString());
           AddParameter("aut_price", auto.getprice());

           ExecuteQuery();


        }
    }
}