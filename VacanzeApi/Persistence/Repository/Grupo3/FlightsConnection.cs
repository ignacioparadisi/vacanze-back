using System;
using System.Collections.Generic;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo3;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo3;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo3
{
    public class FlightsConnection : Connection
    {
        private readonly string ADD_FLIGHT =
            "addflight(@_plane, @_price, TO_TIMESTAMP( @_departure ,'MM-DD-YYYY HH24:MI:SS')::timestamp without time zone, TO_TIMESTAMP( @_arrival ,'MM-DD-YYYY HH24:MI:SS')::timestamp without time zone, @_loc_departure, @_loc_arrival)";

        private readonly string GET_ALL_FLIGHTS = "getflights()";

        private readonly string GET_FLIGTS_BY_DATE =
            "getflightsbydate(@_begin::timestamp without time zone, @_end::timestamp without time zone)";

        public List<Entity> Get()
        {
            try
            {
                var flights = new List<Entity>();
                var aircon = new AirplanesConnection();

                Connect();
                StoredProcedure(GET_ALL_FLIGHTS);
                ExecuteReader();

                for (var i = 0; i < numberRecords; i++)
                {
                    var flight = new Flight(GetInt(i, 0));
                    flight.plane = (Airplane) aircon.Find(GetInt(i, 1));
                    flight.price = GetDouble(i, 2);
                    flight.departure = GetString(i, 3);
                    flight.arrival = GetString(i, 4);
                    flight.loc_departure = GetInt(i, 5);
                    flight.loc_arrival = GetInt(i, 6);

                    flights.Add(flight);
                }

                return flights;
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.ToString());
                throw new DbErrorException(
                    "Ups, a ocurrido un error al conectarse a la base de datos", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            finally
            {
                Disconnect();
            }
        }

        public void Add(Entity entity)
        {
            try
            {
                var flight = (Flight) entity;
                var flight_id = 0;

                Connect();
                StoredProcedure(ADD_FLIGHT);

                AddParameter("_plane", (int) flight.plane.Id);
                AddParameter("_price", flight.price);
                AddParameter("_departure", flight.departure);
                AddParameter("_arrival", flight.arrival);
                AddParameter("_loc_departure", flight.loc_departure);
                AddParameter("_loc_arrival", flight.loc_arrival);

                ExecuteReader();
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.ToString());
                throw new DbErrorException(
                    "Ups, a ocurrido un error al conectarse a la base de datos", ex);
            }
            finally
            {
                Disconnect();
            }
        }

        public List<Entity> GetByDate(string begin, string end)
        {
            try
            {
                var flights = new List<Entity>();
                var aircon = new AirplanesConnection();

                Connect();
                StoredProcedure(GET_FLIGTS_BY_DATE);
                AddParameter("_begin", begin);
                AddParameter("_end", end);
                ExecuteReader();

                for (var i = 0; i < numberRecords; i++)
                {
                    var flight = new Flight(GetInt(i, 0));
                    flight.plane = (Airplane) aircon.Find(GetInt(i, 1));
                    flight.price = GetDouble(i, 2);
                    flight.departure = GetString(i, 3);
                    flight.arrival = GetString(i, 4);
                    flight.loc_departure = GetInt(i, 5);
                    flight.loc_arrival = GetInt(i, 6);

                    flights.Add(flight);
                }

                return flights;
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.ToString());
                throw new DbErrorException(
                    "Ups, a ocurrido un error al conectarse a la base de datos", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            finally
            {
                Disconnect();
            }
        }
    }
}