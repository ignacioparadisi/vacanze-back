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
    public class DAORoutes: DAO
    {
        private const string GET_ROUTE_BY_FLIGHT = "getroutebyflight(@_id)";

        public DAORoutes()
        {

        }

        public List<Entity> Get(int id){
            try
            {
                List<Entity> routes = new List<Entity>();

                Connect();
                StoredProcedure(GET_ROUTE_BY_FLIGHT);
                AddParameter("_id", id);
                ExecuteReader();

                for (int i = 0; i < rowNumber; i++)
                {
                    Route route = new Route(GetInt(i,0));
                    route.locDeparture = GetInt(i,1);
                    route.locArrival = GetInt(i,2);
                    route.arrivalDate = GetString(i,3);
                    route.departureDate = GetString(i,4);

                    routes.Add(route);
                }

                return routes;

            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e.ToString());
                throw;
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