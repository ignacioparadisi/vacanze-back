using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vacanze_back.Entities;
using vacanze_back.Entities.Grupo3;
using vacanze_back.Connection;
using Npgsql;
using vacanze_back.Exceptions.Grupo3;

namespace vacanze_back.Connection.Grupo3
{
    public class RoutesConnection: Connection
    {
        private const string GET_ROUTE_BY_FLIGHT = "getroutebyflight(@_id)";

        public RoutesConnection()
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
                disconnect();
            }
        }
    }
}