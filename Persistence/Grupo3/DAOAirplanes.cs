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
    public class DAOAirplanes : DAO
    {
        private const string GET_ALL_PLANES = "getplanes()";
        private const string FIND_PLANE = "findplane(@_id)";
        
        public DAOAirplanes()
        {
        }

        public List<Entity> Get(){
            try
            {
                List<Entity> airplanes = new List<Entity>();

                Connect();
                StoredProcedure(GET_ALL_PLANES);
                ExecuteReader();

                for (int i = 0; i < rowNumber; i++)
                {
                    Airplane airplane = new Airplane();

                    airplane.setId(GetInt(i,0));
                    airplane.autonomy = GetDouble(i,1);
                    airplane.isActive = GetBool(i,2);
                    airplane.seats = GetInt(i,3);
                    airplane.loadCapacity = GetDouble(i,4);
                    airplane.model = GetString(i,5);

                    airplanes.Add(airplane);
                }

                return airplanes;

            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Entity Find(int id){
            
            try
            {
                Airplane airplane = new Airplane();

                Connect();
                StoredProcedure(FIND_PLANE);
                AddParameter( "_id", id );
                ExecuteReader();

                for (int i = 0; i < rowNumber; i++)
                {
                    airplane.setId(GetInt(i,0));
                    airplane.autonomy = GetDouble(i,1);
                    airplane.isActive = GetBool(i,2);
                    airplane.seats = GetInt(i,3);
                    airplane.loadCapacity = GetDouble(i,4);
                    airplane.model = GetString(i,5);
                }

                return airplane;


            }
            catch (System.Exception)
            {
                
                throw;
            }

        }
    }
}