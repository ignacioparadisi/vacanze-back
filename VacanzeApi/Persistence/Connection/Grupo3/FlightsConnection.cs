using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo3;
using vacanze_back.VacanzeApi.Persistence.Connection;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo3;

namespace vacanze_back.VacanzeApi.Persistence.Connection.Grupo3
{
    public class FlightsConnection : Connection
    {
        private string GET_ALL_FLIGHTS = "getflights()";
        private string ADD_FLIGHT = 
        "addflight(@_plane, @_price, TO_DATE( @_departure ,'MM-DD-YYYY HH:MI:SS'), TO_DATE( @_arrival ,'MM-DD-YYYY HH:MI:SS'), @_loc_departure, @_loc_arrival)";
        private string FIND_FLIGHT = "findflight(@_id)";
        private string UPDATE_FLIGHT = 
        "updateflight(@_id, @_plane, @_price, TO_DATE( @_departure ,'MM-DD-YYYY HH:MI:SS'), TO_DATE( @_arrival ,'MM-DD-YYYY HH:MI:SS'), @_loc_departure, @_loc_arrival)";

        public FlightsConnection()
        {
            
        }

        public List<Entity> Get(){
            try
            {
                List<Entity> flights = new List<Entity>();
                AirplanesConnection aircon = new AirplanesConnection();

                Connect();
                StoredProcedure(GET_ALL_FLIGHTS);
                ExecuteReader();

                for (int i = 0; i < numberRecords; i++)
                {
                    Flight flight = new Flight(GetInt(i,0));
                    flight.id = GetInt(i,0);
                    flight.plane = (Airplane) aircon.Find( GetInt(i,1) );
                    flight.price = GetDouble(i,2);
                    flight.departure = GetString(i,3);
                    flight.arrival = GetString(i,4);
                    flight.loc_departure = GetInt(i,5);
                    flight.loc_arrival = GetInt(i,6);

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

                AddParameter("_plane", (int)flight.plane.Id);
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
                throw new DbErrorException("Ups, a ocurrido un error al conectarse a la base de datos", ex);
            }
            finally{
                Disconnect();
            }
        }

        public void Update(Entity entity){
            try
            {
                Flight flight = (Flight) entity;

                Connect();
                StoredProcedure(UPDATE_FLIGHT); 

                AddParameter("_id", (int)flight.id);
                AddParameter("_plane", (int)flight.plane.Id);
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
                throw new DbErrorException("Ups, a ocurrido un error al conectarse a la base de datos", ex);
            }
            finally{
                Disconnect();
            }
        }

         public Entity Find(int id){
            
            try
            {
                Flight flight = null;
              
                Connect();
                StoredProcedure(FIND_FLIGHT);
                AddParameter( "_id", id );
                ExecuteReader();


                if ( 0 < numberRecords )
                {
                    Console.WriteLine(GetInt(0,0));
                    flight = new Flight(GetInt(0,0));
                    flight.price = GetInt(0,1);
                    flight.departure = GetString(0,2);
                    flight.arrival = GetString(0,3);
                    flight.loc_arrival = GetInt(0,4);
                    flight.loc_departure = GetInt(0,5);

                    flight.plane = new Airplane(GetInt(0,6));

                }

                return flight;


            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.ToString());
                throw new DbErrorException("Ups, a ocurrido un error al conectarse a la base de datos", ex);
            }
            catch (System.Exception)
            {
                
                throw;
            }
            finally{
                Disconnect();
            }

        }

    }

}