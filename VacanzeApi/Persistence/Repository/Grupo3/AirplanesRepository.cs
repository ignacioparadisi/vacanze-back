using System;
using System.Collections.Generic;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo3;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo3;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo3
{
    public static class AirplanesRepository
    {
        static string GET_ALL_PLANES = "getplanes()";
        static string FIND_PLANE = "findplane(@_id)";

        public static List<Entity> Get()
        {
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(GET_ALL_PLANES);
                List<Entity> airplanes = new List<Entity>();

                for (var i = 0; i < table.Rows.Count; i++)
                {
                    Airplane airplane = new Airplane(Convert.ToInt32(table.Rows[i][0]));
                    airplane.autonomy = Convert.ToInt32(table.Rows[i][1]);
                    airplane.isActive = Convert.ToBoolean(table.Rows[i][2]);
                    airplane.seats = Convert.ToInt32(table.Rows[i][3]);
                    airplane.loadCapacity = Convert.ToInt32(table.Rows[i][4]);
                    airplane.model = table.Rows[i][5].ToString();

                    airplanes.Add(airplane);
                }

                return airplanes;
            }
            catch (DatabaseException ex)
            {
                Console.WriteLine(ex.ToString());
                throw new DbErrorException(
                    "Ups, a ocurrido un error al conectarse a la base de datos", ex);
            }
            finally
            {
            }
        }

        public static Entity Find(long id)
        {
            try
            {
                Airplane airplane = null;
                var table = PgConnection.Instance.ExecuteFunction(FIND_PLANE,(int)id);

                for (var i = 0; i < table.Rows.Count; i++)
                {
                    airplane = new Airplane(Convert.ToInt32(table.Rows[i][0]));
                    airplane.autonomy = Convert.ToInt32(table.Rows[i][1]);
                    airplane.isActive = Convert.ToBoolean(table.Rows[i][2]);
                    airplane.seats = Convert.ToInt32(table.Rows[i][3]);
                    airplane.loadCapacity = Convert.ToInt32(table.Rows[i][4]);
                    airplane.model = table.Rows[i][5].ToString();

                }
                return airplane;
            }
            catch (DatabaseException ex)
            {
                Console.WriteLine(ex.ToString());
                throw new DbErrorException(
                    "Ups, a ocurrido un error al conectarse a la base de datos", ex);
            }
            finally
            {
            }
        }
    }
}