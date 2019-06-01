using System;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
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
                Cruiser cruiser = new Cruiser(id, name, status, capacity, loadingShipCap, model, line, picture);
                cruiserList.Add(cruiser);
            }

            if (cruiserList.Count.Equals(0))
            {
                throw new CruiserNotFoundException("No se encontraron cruceros");
            }

            return cruiserList;
        }

        public static Cruiser GetCruiser(int shipId)
        {
            var table = PgConnection.Instance.ExecuteFunction("GetShip(@ship_id)", shipId);
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
            var cruiser = new Cruiser(id, name, status, capacity, loadingShipCap, model, line, picture);
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
                    "ModifyShip(@Id, @Status, @Name, @Capacity, @Loadcap, @Model, @line, @Picture)", cruiser.Id,
                    cruiser.Status, cruiser.Name, cruiser.Capacity, cruiser.LoadingShipCap, cruiser.Model, cruiser.Line,
                    cruiser.Picture);
                var updatedId = Convert.ToInt32(table.Rows[0][0]);
                return cruiser;
            }
            catch (NullReferenceException)
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
                var table = PgConnection.Instance.ExecuteFunction("DeleteShip(@id)", id);
                var deletedId = Convert.ToInt32(table.Rows[0][0]);
                return deletedId;
            }
            catch (InvalidCastException)
            {
                throw new CruiserNotFoundException("Crucero no encontrado");
            }
        }

        public static List<Layover> GetLayovers(int cruiserId)
        {
            List<Layover> layovers = new List<Layover>();
                var table = PgConnection.Instance.ExecuteFunction("getCruise(@cruiserId)", cruiserId);
                if (table.Rows.Count == 0)
                {
                    throw new LayoverNotFoundException("No se encontraron rutas para este crucero");
                }

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var id = Convert.ToInt32(table.Rows[i][0]);
                    var departureDate = Convert.ToString(table.Rows[i][1]);
                    var arrivalDate = Convert.ToString(table.Rows[i][2]);
                    var price = Convert.ToDouble(table.Rows[i][3]);
                    var locDeparture = Convert.ToString(table.Rows[i][4]);
                    var locArrival = Convert.ToString(table.Rows[i][5]);
                    Layover layover = new Layover(id,cruiserId,departureDate, arrivalDate, price, locDeparture, locArrival);
                    layovers.Add(layover);
                }
            return layovers;
        }
        public static int AddLayover(Layover layover)
        {
                var cruiserTable = PgConnection.Instance.ExecuteFunction("GetShip(@ship_id)", layover.CruiserId);
                if (cruiserTable.Rows.Count == 0)
                {
                    throw new CruiserNotFoundException("Crucero no encontrado");
                }
                var table = PgConnection.Instance.ExecuteFunction(
                    "AddCruise(@cruiserId,@departureDate,@arrivalDate,@price,@LocDeparture,@locArrival)", layover.CruiserId, layover.DepartureDate,layover.ArrivalDate,layover.Price,layover.LocDeparture,layover.LocArrival);
                var id = Convert.ToInt32(table.Rows[0][0]);
                return id;
            }
        public static int DeleteLayover(int id)
        {
            try
            {
                var table = PgConnection.Instance.ExecuteFunction("DeleteCruise(@id)", id);
                var deletedid = Convert.ToInt32(table.Rows[0][0]);
                return deletedid;
            }
            catch (InvalidCastException)
            {
                throw new LayoverNotFoundException("Escala no encontrada");
            }
        }
    }
}