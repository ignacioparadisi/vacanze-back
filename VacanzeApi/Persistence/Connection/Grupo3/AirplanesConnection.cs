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
    public class AirplanesConnection : Connection
    {
        private const string GET_ALL_PLANES = "getplanes()";
        private const string FIND_PLANE = "findplane(@_id)";
        
        public AirplanesConnection()
        {
        }

        public List<Entity> Get(){
            try
            {
                List<Entity> airplanes = new List<Entity>();

                Connect();
                StoredProcedure(GET_ALL_PLANES);
                ExecuteReader();

                for (int i = 0; i < cantidadRegistros; i++)
                {
                    Airplane airplane = new Airplane(GetInt(i,0));
                    airplane.autonomy = GetDouble(i,1);
                    airplane.isActive = GetBool(i,2);
                    airplane.seats = GetInt(i,3);
                    airplane.loadCapacity = GetDouble(i,4);
                    airplane.model = GetString(i,5);

                    airplanes.Add(airplane);
                }

                return airplanes;

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

        public Entity Find(long id){
            
            try
            {
                Airplane airplane = null;
                
                Connect();
                StoredProcedure(FIND_PLANE);
                AddParameter( "_id", (int)id );
                ExecuteReader();

                for (int i = 0; i < cantidadRegistros; i++)
                {
                    airplane = new Airplane(GetInt(i,0));
                    airplane.autonomy = GetDouble(i,1);
                    airplane.isActive = GetBool(i,2);
                    airplane.seats = GetInt(i,3);
                    airplane.loadCapacity = GetDouble(i,4);
                    airplane.model = GetString(i,5);
                }

                return airplane;


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