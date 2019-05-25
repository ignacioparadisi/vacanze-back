using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vacanze_back.Entities;
using vacanze_back.Entities.Grupo3;
using vacanze_back.Connection;
using vacanze_back.Connection.Grupo3;
using Npgsql;
using vacanze_back.Exceptions.Grupo3;

namespace vacanze_back.Connection.Grupo3
{
    public class FlightsConnection : Connection
    {
        private string GET_ALL_FLIGHTS = "getflights()";
        private string ADD_FLIGHT = "addflight(@_plane, @_price, to_date(@_departure,'DD-MM-YY'), to_date(@_arrival,'DD-MM-YY'))";
        private string ADD_STOP = "addstop(@_flight,to_date(@_departure,'DD-MM-YY'), to_date(@_arrival,'DD-MM-YY'),@_loc_departure, @_loc_arrival)";

        public FlightsConnection()
        {
            
        }

        public List<Entity> Get(){
            try
            {
                List<Entity> flights = new List<Entity>();
                AirplanesConnection aircon = new AirplanesConnection();
                RoutesConnection routecon = new RoutesConnection();

                Connect();
                StoredProcedure(GET_ALL_FLIGHTS);
                ExecuteReader();

                for (int i = 0; i < cantidadRegistros; i++)
                {
                    Flight flight = new Flight(GetInt(i,0));
                    flight.plane = (Airplane) aircon.Find( GetInt(i,1) );
                    flight.price = GetDouble(i,2);
                    flight.departure = GetString(i,3);
                    flight.arrival = GetString(i,4);

                    foreach (Entity item in routecon.Get( GetInt(i,0) ))
                    {
                        
                        flight.routes.Add( (Route) item );   
                    }

                    flights.Add(flight);
                }

                return flights;
            }
            catch (NpgsqlException ex)
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
                disconnect();
            }
        }

        public void Add(Entity entity){
            try
            {
                Flight flight = (Flight) entity;
                int flight_id = 0;

                Connect();
                StoredProcedure(ADD_FLIGHT);

                AddParameter("_plane", flight.plane);
                AddParameter("_price", flight.price);
                AddParameter("_departure", flight.departure);
                AddParameter("_arrival", flight.arrival);

                ExecuteReader();

                if( cantidadRegistros > 0){
                    flight_id = GetInt(0,0);
                }
                disconnect();

                foreach (Route route in flight.routes)
                {
                    Connect();
                    StoredProcedure(ADD_STOP);

                    AddParameter("_flight", flight_id);
                    AddParameter("_departure", route.departureDate);
                    AddParameter("_arrival", route.arrivalDate);
                    AddParameter("_loc_departure", route.locDeparture);
                    AddParameter("_loc_arrival", route.locArrival);

                    ExecuteReader();
                }
            }
            catch (NpgsqlException ex)
            {
                
                Console.WriteLine(ex.ToString());
                throw new DbErrorException("Ups, a ocurrido un error al conectarse a la base de datos", ex);
            }
            finally{
                disconnect();
            }
        }

    }

}