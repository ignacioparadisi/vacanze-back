using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.Entities;
using vacanze_back.Entities.Grupo13;
using vacanze_back.Persistence;
namespace vacanze_back.Persistence.Grupo13
{
    public class DAORoom : DAO
    {
        const String SP_SELECT = "m13_gethabitaciones()";
        const String SP_FIND_ROOM = "m13_findByHabitacionId(@_id)";
        const String SP_DELETE = "{CALL m13_deleteHabitacion(_id)}";
        const String SP_INSERT = "{CALL m13_addHabitacion(_precio,_capacidad,_status,_hot_fk)}";
        //voids: insert, delete, update
        //returns: getHabitaciones-list, getHabitacion-habitacion
        private Room room;

        public void create(Entity e)
        {
            room = (Room)e;
        }

        public List<Entity> getRooms()
        {
            List<Entity> roomsList = new List<Entity>();
            try
            {
                Connect();
                    StoredProcedure(SP_SELECT);
                    ExecuteReader();
                    for (int i = 0; i < rowNumber; i++)
                    {
                    Room room = new Room();
                    room.setId(GetInt(i, 0));
                    room.price = GetDouble(i, 1);
                    room.capacity = GetInt(i, 2);
                    room.status = GetBool(i, 3);

                    roomsList.Add(room);
                    }
                return roomsList;
            }
            catch (NpgsqlException e)
            {
                e.ToString();
            }
            catch (Exception e)
            {
                e.ToString();
            }
            finally
            {
                Disconnect();
            }
            return roomsList;
        }

        public Entity findRoom(int id)
        {
            room = new Room();
            try
            {
                Connect();
                    StoredProcedure(SP_FIND_ROOM);
                    AddParameter("_id", id);
                    ExecuteReader();
                    for (int i = 0; i < rowNumber; i++)
                    {
                    room.setId(GetInt(i, 0));
                    room.price = GetDouble(i, 1);
                    room.capacity = GetInt(i, 2);
                    room.status = GetBool(i, 3);
                    }
                return room;
            }
            catch (NpgsqlException e)
            {
                e.ToString();
            }
            catch (Exception e)
            {
                e.ToString();
            }
            finally
            {
                Disconnect();  
            }
            return room;
        }

        public void deleteRoom(Room room)
        {
            long id = room.getIt();
            try
            {
                if (Connect())
                {
                    StoredProcedure(SP_DELETE);
                    AddParameter("_id", id);
                    ExecuteQuery();
                }
            }
            catch (NpgsqlException e)
            {
                e.ToString();
            }
            catch (Exception e)
            {
                e.ToString();
            }
            finally
            {
                Disconnect();
            }

        }   

        public void insertRoom(Room room)
        {
            try
            {
                if (Connect())
                {
                    StoredProcedure(SP_INSERT);
                    AddParameter("_precio", room.price);
                    AddParameter("_capacidad", room.capacity);
                    AddParameter("_status", room.status);
//    AddParameter("_capacidad", habitacion.capacidad); QUE VOY HACER AQUI ? 
                    ExecuteQuery();
                }
            }
            catch (NpgsqlException e)
            {
                e.ToString();
            }
            catch (Exception e)
            {
                e.ToString();
            }
            finally
            {
                Disconnect();
            }
        } //Incompleto
    }
}
