using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vacanze_back.Entities;
using vacanze_back.Entities.Grupo3;
using vacanze_back.Persistence;
using Npgsql;

namespace vacanze_back.Persistence.Grupo3
{
    public class DAOFlight : DAO
    {
        private string ADD_FLIGHT = "addflight(@_plane, @_price, to_date(@_departure,'DD-MM-YY'), to_date(@_arrival,'DD-MM-YY'))";
        private string ADD_STOP = "addstop(@_flight,to_date(@_departure,'DD-MM-YY'), to_date(@_arrival,'DD-MM-YY'),@_loc_departure, @_loc_arrival)";

        public DAOFlight()
        {
            
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