using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo8
{
    public static class CruiserRepository
    {

        public static List<Cruiser> GetCruisers()
        {
            var cruiserList = new List<Cruiser>();
            var table = PgConnection.Instance.ExecuteFunction("GetALLShip()");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var id = Convert.ToInt32(table.Rows[i][0]);
                var name = table.Rows[i][1].ToString();
                var status = Convert.ToBoolean(table.Rows[i][2]);
                var capacity = Convert.ToInt32(table.Rows[i][3]);
                var loadingShipCap = Convert.ToInt32(table.Rows[i][4]);
                var model = table.Rows[i][5].ToString();
                var line = table.Rows[i][6].ToString();
                var picture = table.Rows[i][7].ToString();
                Cruiser cruiser = new Cruiser(id,name,status,capacity,loadingShipCap,model,line,picture);
                cruiserList.Add(cruiser);
            }
            if (cruiserList.Count.Equals(0))
            {
                throw new CruiserNotFoundException("No se encontraron cruceros");
            }
            return cruiserList;
        }

        public static Cruiser GetCruiser(int ship_id)
        {
            var table = PgConnection.Instance.ExecuteFunction("GetShip(@ship_id)" , ship_id);
                if (table.Rows.Count == 0)
                {
                    throw new CruiserNotFoundException("Crucero no encontrado");
                }
                var id = Convert.ToInt32(table.Rows[0][0]);
                var name = table.Rows[0][1].ToString();
                var status = Convert.ToBoolean(table.Rows[0][2]);
                var capacity = Convert.ToInt32(table.Rows[0][3]);
                var loadingShipCap = Convert.ToInt32(table.Rows[0][4]);
                var model = table.Rows[0][5].ToString();
                var line = table.Rows[0][6].ToString();
                var picture = table.Rows[0][7].ToString();
                Console.WriteLine(name);
                Cruiser cruiser = new Cruiser(id,name,status,capacity,loadingShipCap,model,line,picture);
                return cruiser;
        }

        public static int AddCruiser(Cruiser cruiser)
        {
            var table = PgConnection.Instance.ExecuteFunction(
                    "AddShip(@name,@capacity,@loadingcap,@model,@line,@picture)", cruiser.Name, cruiser.Capacity,
                    cruiser.LoadingShipCap, cruiser.Model, cruiser.Line, cruiser.Picture);
                var id = Convert.ToInt32(table.Rows[0][0]);
            return id;
        }

        public static Cruiser UpdateCruiser(Cruiser cruiser)
        {
            try
            { 
                var table = PgConnection.Instance.ExecuteFunction(
                    "ModifyShip(@Id, @Status, @Name, @Capacity, @Loadcap, @Model, @line, @Picture)",Convert.ToInt32(cruiser.Id),
                    cruiser.Status, cruiser.Name, cruiser.Capacity,cruiser.LoadingShipCap, cruiser.Model, cruiser.Line,cruiser.Picture);
                var updatedid = Convert.ToInt32(table.Rows[0][0]);
                return cruiser;
            }
            catch (NullReferenceException e)
            {
                throw new NullCruiserException("El crucero no puede ser null");
            }
            catch (InvalidCastException)
            {
                throw new CruiserNotFoundException("Crucero no encontrado");
            }
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
                throw new CruiserNotFoundException("Crucero no encontrado");
            }
        }
    }
}