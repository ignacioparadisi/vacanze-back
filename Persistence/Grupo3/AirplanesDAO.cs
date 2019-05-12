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
    public class AirplanesDAO : DAO
    {
        private const string GET_ALL_PLANES = "getallaviones()";
        private const string FIND_PLANE = "findavion(@_id)";
        
        public AirplanesDAO()
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
                    airplane.model = GetString(i,1);
                    airplane.seats = GetInt(i,2);
                    airplane.loadCapacity = GetDouble(i,3);
                    airplane.autonomy = GetDouble(i,4);

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
                    airplane.model = GetString(i,1);
                    airplane.seats = GetInt(i,2);
                    airplane.loadCapacity = GetDouble(i,3);
                    airplane.autonomy = GetDouble(i,4);
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