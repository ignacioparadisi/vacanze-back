using System;
using System.Collections.Generic;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo8
{
    public static class CruiserConnection
    {

//        public List<Cruiser> GetCruisers()
//        {
//            var table = PgConnection.Instance.ExecuteFunction();
//        }

        public static Cruiser GetCruiser(int ship_id)
        {
            var table = PgConnection.Instance.ExecuteFunction("GetShip(@ship_id)" , ship_id);
            try
            {
                var id = Convert.ToInt32(table.Rows[0][0]);
                var name = table.Rows[0][1].ToString();
                var status = Convert.ToBoolean(table.Rows[0][2]);
                var capacity = Convert.ToInt32(table.Rows[0][3]);
                var LoadingShipCap = Convert.ToInt32(table.Rows[0][4]);
                var model = table.Rows[0][5].ToString();
                var line = table.Rows[0][6].ToString();
                Console.WriteLine(name);
                Cruiser cruiser = new Cruiser(id,name,status,capacity,LoadingShipCap,model,line);
                return cruiser;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
          
        }
    }
}