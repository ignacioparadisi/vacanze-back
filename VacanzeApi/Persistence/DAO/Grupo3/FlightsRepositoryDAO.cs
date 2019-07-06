using System;
using System.Collections.Generic;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo3;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo3;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo3;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo3;
using vacanze_back.VacanzeApi.Persistence.Repository;
using System.Data;


namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo3
{
    public  class FlightRepositoryDAO: IFlightDAO
    {
        static string GET_ALL_FLIGHTS = "getflights()";
        static string ADD_FLIGHT =
        "addflight( @_plane, @_price, @_departure,@_arrival, @_loc_arrival, @_loc_departure )";
        static string GET_FLIGTS_BY_DATE = "getflightsbydate(@_begin, @_end)";
        static string GET_FLIGHTS_BY_LOCATION = "getflightsbylocation(@_departure, @_arrival)";
        static string FIND_FLIGHT = "findflight(@_id)";
        static string UPDATE_FLIGHT =
        "updateflight(@_id, @_plane, @_price, @_departure, @_arrival, @_loc_departure, @_loc_arrival)";
        static string DELETE_FLIGHT = "deleteflight(@_id)";
        static string GET_OUTBOUND_FLIGHTS = "getOutBoundFlights(@_loc_departure, @_loc_arrival, @_departure)";
        static string GET_ROUNTRIP_FLIGHTS = "getRounTripFlights(@_loc_departure, @_loc_arrival, @_departure, @_arrival)";


        /// <summary>Devuelve lista de vuelos de la DB</summary>
        /// <returns>List<Entity> con el resultado de la query</returns>
        public List<Entity> Get()
        {
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(GET_ALL_FLIGHTS);
                List<Entity> flights = new List<Entity>();

                for (var i = 0; i < table.Rows.Count; i++)
                {
                    Flight flight = GetRow(table,i);

                    flights.Add(flight);
                }

                return flights;
            }
            catch (DatabaseException ex)
            {
                Console.WriteLine(ex.ToString());
                throw new DbErrorException("¡Ups! Hubo un error con la Base de Datos", ex);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            finally
            {
            }
        }


        /// <summary>Agrega vuelo a la DB</summary>
        /// <param name="entity">Entidad con vuelo a agregar a la DB</param>
        public int Add(FlightDTO flight)
        {
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(
                    ADD_FLIGHT,flight.plane.Id, flight.price, flight.departure, flight.arrival, flight.loc_departure.Id, flight.loc_arrival.Id
                );

                if (table.Rows.Count > 0){
                    return Convert.ToInt32(table.Rows[0][0]);
                }

                return 0;

            }
            catch (DatabaseException ex)
            {

                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.Message);
                throw new DbErrorException("¡Ups! Hubo un error con la Base de Datos", ex);
            }
            finally
            {
            }
        }

        /// <summary>Edita vuelo a la DB</summary>
        /// <param name="entity">Entidad con vuelo a editar en la DB</param>
        public void Update(Entity entity)
        {
            try
            {
                Flight flight = (Flight)entity;

                var table = PgConnection.Instance.ExecuteFunction(
                    UPDATE_FLIGHT,
                    (int)flight.Id, (int)flight.plane.Id, flight.price, flight.departure, flight.arrival, (int)flight.loc_departure.Id, (int)flight.loc_arrival.Id
                );
            }
            catch (DatabaseException ex)
            {

                Console.WriteLine(ex.ToString());
                throw new DbErrorException("¡Ups! Hubo un error con la Base de Datos", ex);
            }
            finally
            {
            }
        }

        /// <summary>Funcion para borrar un vuelo de la DB</summary>
        /// <param name="entiry">Entidad con id a borrar</param>
        public void Delete(int id)
        {
            try
            {

                var table = PgConnection.Instance.ExecuteFunction(
                   DELETE_FLIGHT,id);

                // throw new DbErrorException(flight.Id.ToString());
            }
            catch (DatabaseException ex)
            {

                Console.WriteLine(ex.ToString());
                throw new DbErrorException("¡Ups! Hubo un error con la Base de Datos", ex);
            }
            finally
            {
            }
        }

        /// <summary>Busca vuelo especifico en la DB</summary>
        /// <param name="id">Id del vuelo a busac</param>
        /// <returns>Entity con el resultado de la query</returns>
        public int Find(int id)
        {

            try
            {
                var table = PgConnection.Instance.ExecuteFunction(FIND_FLIGHT, id);
               Flight flight = null;
                //DAOFactory Flight=DAOFactory

                AirplanesRepositoryDAO airplane=new AirplanesRepositoryDAO();
                if (table.Rows.Count > 0)
                {
                    flight = new Flight(Convert.ToInt32(table.Rows[0][0]));
                    flight.Id = Convert.ToInt32(table.Rows[0][0]);
                    flight.price = Convert.ToDouble(table.Rows[0][1]);
                    flight.departure = table.Rows[0][2].ToString();
                    flight.arrival = table.Rows[0][3].ToString();
                    flight.loc_departure = LocationRepository.GetLocationById(Convert.ToInt32(table.Rows[0][4]));
                    flight.loc_arrival = LocationRepository.GetLocationById(Convert.ToInt32(table.Rows[0][5]));
                    flight.plane = airplane.Find(Convert.ToInt32(table.Rows[0][6]));
                }
                return flight.Id;
            }

            catch (DatabaseException ex)
            {
                Console.WriteLine(ex.ToString());
                throw new DbErrorException("¡Ups! Hubo un error con la Base de Datos", ex);
            }
            catch (System.Exception)
            {

                throw;
            }
           

        }

        /// <summary>Busca vuelo en un rango de fechas en la DB</summary>
        /// <param name="begin">Fecha menor a buscar</param>
        /// <param name="end">Fecha mayor a buscar</param>
        /// <returns> List<Entity> con el resultado de la query</returns>
        public List<Entity> GetByDate(string begin, string end)
        {
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(GET_FLIGTS_BY_DATE, begin, end);
                List<Entity> flights = new List<Entity>();

                for (var i = 0; i < table.Rows.Count; i++)
                {

                    Flight flight = GetRow(table,i);

                    flights.Add(flight);
                }

                return flights;
            }
            catch (DatabaseException ex)
            {
                Console.WriteLine(ex.ToString());
                throw new DbErrorException("¡Ups! Hubo un error con la Base de Datos", ex);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            finally
            {
            }
        }

        /// <summary>Busca vuelo en un rango de fechas en la DB</summary>
        /// <param name="departure">Locacion de salida del vuelo</param>
        /// <param name="arrival">Locacion de llegada del vuelo</param>
        /// <returns> List<Entity> con el resultado de la query</returns>
        public List<Entity> GetByLocation(int departure, int arrival)
        {
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(GET_FLIGHTS_BY_LOCATION, departure, arrival);
                List<Entity> flights = new List<Entity>();

                for (var i = 0; i < table.Rows.Count; i++)
                {

                    Flight flight = GetRow(table,i);

                    flights.Add(flight);
                }

                return flights;
            }
            catch (DatabaseException ex)
            {
                Console.WriteLine(ex.ToString());
                throw new DbErrorException("¡Ups! Hubo un error con la Base de Datos", ex);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            finally
            {
            }
        }

        /// <summary>Busca por ciudad de salida y llegada y determinada fecha de salida</summary>
        /// <param name="departure">Ciudad de salida del vuelor</param>
        /// <param name="arrival">Ciudad de llegada del vuelo</param>
        /// <param name="departuredate">Fecha de salida del vuelo</param>
        /// <returns> List<Entity> con el resultado de la query</returns>
        public List<Entity> GetOutboundFlights(int departure, int arrival, string departuredate)
        {
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(GET_OUTBOUND_FLIGHTS, departure, arrival, departuredate);
                List<Entity> flights = new List<Entity>();

                for (var i = 0; i < table.Rows.Count; i++)
                {

                    Flight flight = GetRow(table,i);
                    flights.Add(flight);
                }

                return flights;
            }
            catch (DatabaseException ex)
            {
                Console.WriteLine(ex.ToString());
                throw new DbErrorException("¡Ups! Hubo un error con la Base de Datos", ex);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            finally
            {
            }
        }

        /// <summary>Busca por ciudad de salida y llegada y determinada fecha de salida y llegada</summary>
        /// <param name="departure">Ciudad de salida del vuelor</param>
        /// <param name="arrival">Ciudad de llegada del vuelo</param>
        /// <param name="departuredate">Fecha de salida del vuelo</param>
        /// <param name="arrivaldate">Fecha de llegada del vuelo</param>
        /// <returns> List<Entity> con el resultado de la query</returns>
        public List<Entity> GetRoundTripFlights(int departure, int arrival, string departuredate, string arrivaldate)
        {
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(GET_ROUNTRIP_FLIGHTS, departure, arrival, departuredate, arrivaldate);
                List<Entity> flights = new List<Entity>();

                for (var i = 0; i < table.Rows.Count; i++)
                {

                    Flight flight = GetRow(table,i);
                    flights.Add(flight);
                }

                return flights;
            }
            catch (DatabaseException ex)
            {
                Console.WriteLine(ex.ToString());
                throw new DbErrorException("¡Ups! Hubo un error con la Base de Datos", ex);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            finally
            {
            }
        }

        static private Flight GetRow(DataTable table, int i){
            AirplanesRepositoryDAO airplane=new AirplanesRepositoryDAO();

            Flight flight = new Flight(Convert.ToInt32(table.Rows[i][0]));
            flight.Id = Convert.ToInt32(table.Rows[i][0]);
            flight.plane = airplane.Find(Convert.ToInt32(table.Rows[i][1]));
            flight.price = Convert.ToDouble(table.Rows[i][2]);
            flight.departure = table.Rows[i][3].ToString();
            flight.arrival = table.Rows[i][4].ToString();
            flight.loc_departure = LocationRepository.GetLocationById(Convert.ToInt32(table.Rows[i][5]));
            flight.loc_arrival = LocationRepository.GetLocationById(Convert.ToInt32(table.Rows[i][6]));

            return flight;

        }

    }
}