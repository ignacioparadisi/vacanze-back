using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vacanze_back.Entities;
using vacanze_back.Entities.Grupo3;
using vacanze_back.Persistence;
using vacanze_back.Persistence.Grupo3;
using Npgsql;

namespace vacanze_back.Persistence.Grupo3
{
    public class DAOFlight : DAO
    {
        private string GET_ALL_FLIGHTS = "getflights()";
        private string ADD_FLIGHT = "addflight(@_plane, @_price, to_date(@_departure,'DD-MM-YY'), to_date(@_arrival,'DD-MM-YY'))";
        private string ADD_STOP = "addstop(@_flight,to_date(@_departure,'DD-MM-YY'), to_date(@_arrival,'DD-MM-YY'),@_loc_departure, @_loc_arrival)";

        public DAOFlight()
        {
            
        }

        public List<Entity> Get(){
            try
            {
                List<Entity> flights = new List<Entity>();
                DAOAirplanes daop = new DAOAirplanes();
                DAORoutes daor = new DAORoutes();

                Connect();
                StoredProcedure(GET_ALL_FLIGHTS);
                ExecuteReader();

                for (int i = 0; i < rowNumber; i++)
                {
                    Flight flight = new Flight(GetInt(i,0));
                    flight.plane = (Airplane) daop.Find( GetInt(i,1) );
                    flight.price = GetDouble(i,2);
                    flight.departure = GetString(i,3);
                    flight.arrival = GetString(i,4);

                    foreach (Entity item in daor.Get( GetInt(i,0) ))
                    {
                        
                        flight.routes.Add( (Route) item );   
                    }

                    flights.Add(flight);
                }

                return flights;
            }
            catch (NpgsqlException ex)
            {
                ex.ToString();
                throw;
            }
            catch (System.Exception ex)
            {
                ex.ToString();
                throw;
            }
            finally{
                Disconnect();
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

                if( rowNumber > 0){
                    flight_id = GetInt(0,0);
                }
                Disconnect();

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
            catch (NpgsqlException e)
            {
                
                Console.WriteLine(e.ToString());
                Disconnect();
                throw new Exception();
            }
        }

    }

}