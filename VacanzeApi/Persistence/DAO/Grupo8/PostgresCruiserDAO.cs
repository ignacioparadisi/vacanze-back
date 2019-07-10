using System;
using System.Collections.Generic;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;
using vacanze_back.VacanzeApi.Persistence;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo8
{
    public class PostgresCruiserDAO: ICruiserDAO
    {
        
        /// <summary>
        ///     Metodo para obtener todos los cruceros guardados.
        /// </summary>
        /// <returns>Lista de cruceros</returns>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la base de
        ///     datos
        /// </exception>
        public List<Cruiser> GetCruisers()
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
            return cruiserList;
        }
        /// <summary>
        ///     Metodo para obtener objeto Crucero correspondiente a los datos guardados para el ID recibido.
        /// </summary>
        /// <param name="shipId">ID del crucero a obtener</param>
        /// <returns>Objeto Crucero correspondiente al ID recibido</returns>
        /// <exception cref="CruiserNotFoundException">Lanzada si no existe un Crucero para el ID recibido</exception>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la bse de
        ///     datos
        /// </exception>
        public Cruiser GetCruiser(int shipId)
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
        /// <summary>
        ///     Metodo para añadir un Crucero.
        /// </summary>
        /// <param name="cruiser">Datos a ser guardados en tipo crucero</param>
        /// <returns>ID del registro del crucero en la base de datos</returns>
        /// <exception cref="InvalidAttributeException">Algun atributo tenia un valor invalido</exception>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la bse de
        ///     datos
        /// </exception>
        public int AddCruiser(Cruiser cruiser)
        {
            cruiser.Validate();
            var table = PgConnection.Instance.ExecuteFunction(
                "AddShip(@name,@capacity,@loadingcap,@model,@line,@picture)", cruiser.Name, cruiser.Capacity,
                cruiser.LoadingShipCap, cruiser.Model, cruiser.Line, cruiser.Picture);
            var id = Convert.ToInt32(table.Rows[0][0]);
            return id;
        }
        /// <summary>
        ///     Metodo para actualizar los datos de un Crucero.
        /// </summary>
        /// <param name="cruiser">Datos a ser actualizados en tipo crucero guardados</param>
        /// <returns>ID del registro del crucero en la base de datos</returns>
        /// <exception cref="InvalidAttributeException">Algun atributo tenia un valor invalido</exception>
        /// <exception cref="NullCruiserException">El metodo recibio null como parametro</exception>
        /// <exception cref="CruiserNotFoundException">No se encontro el crucero con el Id sumunistrado en los parametros</exception>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la bse de
        ///     datos
        /// </exception>
        public Cruiser UpdateCruiser(Cruiser cruiser)
        {
            try
            {
                cruiser.Validate();
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
        /// <summary>
        ///     Metodo para eliminar un Crucero.
        /// </summary>
        /// <param name="id">Id del crucero a ser eliminado</param>
        /// <returns>ID del registro del  crucero en la base de datos</returns>
        /// <exception cref="CruiserNotFoundException">Retornado si el id ingresado no corresponde con ningun crucero</exception>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la base de
        ///     datos
        /// </exception>
        public int DeleteCruiser(int id)
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
        /// <summary>
        ///     Metodo para obtener todos los layovers (escalas) de un crucero.
        /// </summary>
        /// <param name="cruiserId">Id del crucero del cual se desea obtener los escalas</param>
        /// <returns>Lista de layovers (escalas) de un crucero</returns>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la base de
        ///     datos
        /// </exception>
        public List<Layover> GetLayovers(int cruiserId)
        {
            List<Layover> layovers = new List<Layover>();
            var table = PgConnection.Instance.ExecuteFunction("getCruisers(@cruiserId)", cruiserId);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var id = Convert.ToInt32(table.Rows[i][0]);
                var departureDate = Convert.ToString(table.Rows[i][2]);
                var arrivalDate = Convert.ToString(table.Rows[i][3]);
                var price = Convert.ToDecimal(table.Rows[i][4]);
                var locDeparture = Convert.ToInt32(table.Rows[i][5]);
                var locArrival = Convert.ToInt32(table.Rows[i][6]);
                Layover layover = new Layover(id,cruiserId,departureDate, arrivalDate, price, locDeparture, locArrival);
                layovers.Add(layover);
            }
            return layovers;
        }
        /// <summary>
        ///     Metodo para agregar un layover a un crucero.
        /// </summary>
        /// <param name="layover">Id del crucero al que se le agregara la escala</param>
        /// <returns>Lista de layovers (escalas) de un crucero</returns>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la base de
        ///     datos
        /// </exception>
        /// <exception cref="CruiserNotFoundException">Lanzada si el id del crucero en el layover no corresponde con ningun crucero guardado</exception>
        public Layover AddLayover(Layover layover)
        {
                var cruiserTable = PgConnection.Instance.ExecuteFunction("GetShip(@ship_id)", layover.CruiserId);
                if (cruiserTable.Rows.Count == 0)
                {
                    throw new CruiserNotFoundException("Crucero no encontrado");
                }
                var table = PgConnection.Instance.ExecuteFunction(
                    "addcruise(@cruiserId,@departureDate,@arrivalDate,@price,@LocDeparture,@locArrival)", layover.CruiserId, layover.DepartureDate,layover.ArrivalDate,layover.Price,layover.LocDeparture,layover.LocArrival);
                var id = Convert.ToInt32(table.Rows[0][0]);
                layover.Id = id;
                return layover;
        }
        /// <summary>
        ///     Metodo para eliminar una escala.
        /// </summary>
        /// <param name="id">Id del layover a ser eliminado</param>
        /// <returns>ID del registro del layover en la base de datos</returns>
        /// <exception cref="LayoverNotFoundException">Retornado si el id ingresado no corresponde con ningun layover</exception>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la base de
        ///     datos
        /// </exception>
        public int DeleteLayover(int id)
        {
            try
            {
                var table = PgConnection.Instance.ExecuteFunction("DeleteCruise(@id)", id);
                var deletedid = Convert.ToInt32(table.Rows[0][0]);
                return deletedid;
            }
            catch (Exception e){
                Console.Write(e);
                throw new LayoverNotFoundException("Escala no encontrada");
            }
        }
        /// <summary>
        ///     Metodo para obtener todos los layovers (escalas) de un crucero.
        /// </summary>
        /// <param name="departure">Id del la locacion de salida del crucero</param>
        /// <param name="arrival">Id de la locacion de llegada del crucero</param>
        /// <returns>Lista de layovers (escalas) de un crucero</returns>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la base de
        ///     datos
        /// </exception>
        /// <exception cref="LayoverNotFoundException">Si no se encontraron rutas para las locaciones ingresadass</exception>
        public List<Layover> GetLayoversForRes(int departure, int arrival)
        {
            List<Layover> layovers = new List<Layover>();
                var table = PgConnection.Instance.ExecuteFunction("getCruiseByLocation(@departure,@arrival)", departure, arrival);
                if (table.Rows.Count == 0)
                {
                    throw new LayoverNotFoundException("No se encontraron rutas disponibles para esas ciudades");
                }

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var id = Convert.ToInt32(table.Rows[i][0]);
                    var shipid = Convert.ToInt32(table.Rows[i][1]);
                    var departureDate = Convert.ToString(table.Rows[i][2]);
                    var arrivalDate = Convert.ToString(table.Rows[i][3]);
                    var price = Convert.ToDecimal(table.Rows[i][4]);
                    var locDeparture = Convert.ToInt32(table.Rows[i][5]);
                    var locArrival = Convert.ToInt32(table.Rows[i][6]);
                    Layover layover = new Layover(id, shipid, departureDate, arrivalDate, price, locDeparture, locArrival);
                    layovers.Add(layover);
                }
            return layovers;
        }
        
    }
}