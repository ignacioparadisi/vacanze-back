using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo8
{
    public static class CruiserConnection
    {

        public static List<Cruiser> GetCruisers()
        {
            var CruiserList = new List<Cruiser>();
            var table = PgConnection.Instance.ExecuteFunction("GetALLShip()");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var id = Convert.ToInt32(table.Rows[i][0]);
                var name = table.Rows[0][1].ToString();
                var status = Convert.ToBoolean(table.Rows[i][2]);
                var capacity = Convert.ToInt32(table.Rows[i][3]);
                var LoadingShipCap = Convert.ToInt32(table.Rows[i][4]);
                var model = table.Rows[i][5].ToString();
                var line = table.Rows[i][6].ToString();
                Cruiser cruiser = new Cruiser(id,name,status,capacity,LoadingShipCap,model,line);
                CruiserList.Add(cruiser);
            }
            return CruiserList;
        }

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

        public static int AddCruiser(Cruiser cruiser)
        {
            var table = PgConnection.Instance.ExecuteFunction(
                "AddShip(@name,@capacity,@loadingcap,@model,@line,@picture)", cruiser.Name, cruiser.Capacity,
                cruiser.LoadingShipCap, cruiser.Model, cruiser.Line, "cruiser.jpg");
            var id = Convert.ToInt32(table.Rows[0][0]);
            return id;
        }
        public static int DeleteCruiser(int id)
        {
            try
            {
                var table = PgConnection.Instance.ExecuteFunction("DeleteShip(@id)",id);
                var deletedid = Convert.ToInt32(table.Rows[0][0]);
                return deletedid;
            }
            catch (InvalidCastException)
            {
                return -1;
            }
        }
    }
}