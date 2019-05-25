using System;
using System.Collections.Generic;
using Npgsql;      
using vacanze_back.Entities.Grupo5;
namespace vacanze_back.DAO.Grupo5
{
    public class DAOAuto:DAO
    {
        public DAOAuto()
        {
            CrearStringConexion();
        }

        public void Agregar(Auto auto)
        {
            Conectar();
            StoredProcedure("AgregarAutomovil (@aut_make,@aut_model,@aut_capacity,@aut_isActive,@aut_licence,@aut_price)");
            AgregarParametro("aut_make", auto.getmake().ToString());
            AgregarParametro("aut_model", auto.getmodel().ToString());
            AgregarParametro("aut_capacity", auto.getcapacity());
            AgregarParametro("aut_isActive", auto.getisActive());
            AgregarParametro("aut_licence", auto.getlicence().ToString());
            AgregarParametro("aut_price", auto.getprice());

            EjecutarQuery();


        }
    }
}