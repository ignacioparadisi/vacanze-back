using System;
using System.Collections.Generic;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo3;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo3;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo3
{
    public static class FlightRepository
    {
        static string GET_ALL_FLIGHTS = "getflights()";
        static string ADD_FLIGHT = 
        "addflight( @_plane, @_price, @_departure,@_arrival, @_loc_arrival, @_loc_departure )";
        static string GET_FLIGTS_BY_DATE = "getflightsbydate(@_begin, @_end)";

        static string FIND_FLIGHT = "findflight(@_id)";
        static string UPDATE_FLIGHT = 
        "updateflight(@_id, @_plane, @_price, @_departure, @_arrival, @_loc_departure, @_loc_arrival)";


        /// <summary>Devuelve lista de vuelos de la DB</summary>
        /// <returns>List<Entity> con el resultado de la query</returns>
        public static List<Entity> Get(){
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(GET_ALL_FLIGHTS);
                List<Entity> flights = new List<Entity>();

                for (var i = 0; i < table.Rows.Count; i++)
                {
                    Flight flight = new Flight(Convert.ToInt32(table.Rows[i][0]));
                    flight.Id = Convert.ToInt32(table.Rows[i][0]);
                    flight.plane = (Airplane) AirplanesRepository.Find( Convert.ToInt32(table.Rows[i][1]) );
                    flight.price = Convert.ToDouble(table.Rows[i][2]);
                    flight.departure = table.Rows[i][3].ToString();
                    flight.arrival = table.Rows[i][4].ToString();
                    flight.loc_arrival = Convert.ToInt32(table.Rows[i][5]);
                    flight.loc_departure = Convert.ToInt32(table.Rows[i][6]);

                    flights.Add(flight);
                }

                return flights;
            }
            catch (DatabaseException ex)
            {
                Console.WriteLine(ex.ToString());
                throw new DbErrorException("Ups, a ocurrido un error al conectarse a la base de datos", ex);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            finally{
            }
        }


        /// <summary>Agrega vuelo a la DB</summary>
        /// <param name="entity">Entidad con vuelo a agregar a la DB</param>
        public static void Add(Entity entity){
            try
            {
                Flight flight = (Flight) entity;

                var table = PgConnection.Instance.ExecuteFunction(
                    ADD_FLIGHT,
                    (int)flight.plane.Id, flight.price, flight.departure, flight.arrival, flight.loc_arrival, flight.loc_departure
                );
            }
            catch (DatabaseException ex)
            {
                
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.Message);
                throw new DbErrorException("Ups, a ocurrido un error al conectarse a la base de datos", ex);
            }
            finally{
            }
        }

        /// <summary>Edita vuelo a la DB</summary>
        /// <param name="entity">Entidad con vuelo a editar en la DB</param>
        public static void Update(Entity entity){
            try
            {
                Flight flight = (Flight) entity;

                var table = PgConnection.Instance.ExecuteFunction(
                    UPDATE_FLIGHT,
                    (int)flight.Id ,(int)flight.plane.Id, flight.price, flight.departure, flight.arrival, (int)flight.loc_departure, (int)flight.loc_arrival
                );
            }
            catch (DatabaseException ex)
            {
                
                Console.WriteLine(ex.ToString());
                throw new DbErrorException("Ups, a ocurrido un error al conectarse a la base de datos", ex);
            }
            finally{
            }
        }

        /// <summary>Busca vuelo especifico en la DB</summary>
        /// <param name="id">Id del vuelo a busac</param>
        /// <returns>Entity con el resultado de la query</returns>
         public static Entity Find(int id){
            
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(FIND_FLIGHT, id);
                Flight flight = null;

                if (table.Rows.Count > 0)
                {
                    flight = new Flight(Convert.ToInt32(table.Rows[0][0]));
                    flight.Id = Convert.ToInt32(table.Rows[0][0]);
                    flight.price = Convert.ToDouble(table.Rows[0][1]);
                    flight.departure = table.Rows[0][2].ToString();
                    flight.arrival = table.Rows[0][3].ToString();
                    flight.loc_arrival = Convert.ToInt32(table.Rows[0][4]);
                    flight.loc_departure = Convert.ToInt32(table.Rows[0][5]);
                    flight.plane = (Airplane) AirplanesRepository.Find( Convert.ToInt32(table.Rows[0][6]) );
                }

                return flight;


            }
            catch (DatabaseException ex)
            {
                Console.WriteLine(ex.ToString());
                throw new DbErrorException("Ups, a ocurrido un error al conectarse a la base de datos", ex);
            }
            catch (System.Exception)
            {
                
                throw;
            }
            finally{
            }

        }

        /// <summary>Busca vuelo en un rango de fechas en la DB</summary>
        /// <param name="begin">Fecha menor a buscar</param>
        /// <param name="end">Fecha mayor a buscar</param>
        /// <returns> List<Entity> con el resultado de la query</returns>
        public static List<Entity> GetByDate(string begin, string end){
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(GET_FLIGTS_BY_DATE, begin, end);
                List<Entity> flights = new List<Entity>();

                for (var i = 0; i < table.Rows.Count; i++)
                {

                    Flight flight = new Flight(Convert.ToInt32(table.Rows[i][0]));
                    flight.Id = Convert.ToInt32(table.Rows[i][0]);
                    flight.plane = (Airplane) AirplanesRepository.Find( Convert.ToInt32(table.Rows[i][1]) );
                    flight.price = Convert.ToDouble(table.Rows[i][2]);
                    flight.departure = table.Rows[i][3].ToString();
                    flight.arrival = table.Rows[i][4].ToString();
                    flight.loc_arrival = Convert.ToInt32(table.Rows[i][5]);
                    flight.loc_departure = Convert.ToInt32(table.Rows[i][6]);

                    flights.Add(flight);
                }

                return flights;
            }
            catch (DatabaseException ex)
            {
                Console.WriteLine(ex.ToString());
                throw new DbErrorException("Ups, a ocurrido un error al conectarse a la base de datos", ex);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            finally{
            }
        }

    }
}