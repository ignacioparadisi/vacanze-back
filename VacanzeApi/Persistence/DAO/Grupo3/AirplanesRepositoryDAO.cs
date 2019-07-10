using System;
using System.Collections.Generic;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo3;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo3;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo3;


namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo3
{
    public class AirplanesRepositoryDAO : IAirplaneDAO
    {
        static string GET_ALL_PLANES = "getplanes()";
        static string FIND_PLANE = "findplane(@_id)";

        /// <summary>Busca aviones en la DB</summary>
        /// <returns> List<Entity> con el resultado de la query </returns>
        public List<Airplane> Get()
        {
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(GET_ALL_PLANES);
                List<Airplane> airplanes = new List<Airplane>();

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
        }

        /// <summary>Busca vuelo especifico en la DB</summary>
        /// <param name="id">Id del avion a buscar</param>
        /// <returns>Entity con el resultado de la query</returns>
        public Airplane Find(long id)
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

        public static explicit operator AirplanesRepositoryDAO(Entity v)
        {
            throw new NotImplementedException();
        }
    }
}