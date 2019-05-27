using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using System.Text.RegularExpressions;
using vacanze_back.VacanzeApi.Persistence.Connection;
namespace vacanze_back.VacanzeApi.Persistence.Connection.Grupo13
{
    public class RoomConnection : Connection
    {
        const String SP_SELECT = "m13_getRooms()";
        const String SP_FIND_ROOM = "m13_findByRoomId(@_id)";
        const String SP_DELETE = "{CALL m13_deleteHabitacion(_id)}";
        const String SP_INSERT = "{CALL m13_addHabitacion(@_precio,@_capacidad,@_status,@_hot_fk)}";
        //voids: insert, delete, update
        //returns: getHabitaciones-list, getHabitacion-habitacion
        private Room room;

        public void Create(Entity e)
        {
            room = (Room)e;
        }

        public List<Entity> GetRooms()
        {
            List<Entity> roomsList = new List<Entity>();
            try
            {
                Connect();
                StoredProcedure(SP_SELECT);
                ExecuteReader();
                for (int i = 0; i < numberRecords; i++)
                {
                                                //id, precio,      capacidad,        status
                    Room room = new Room(GetInt(i,0), GetDouble(i, 1), GetInt(i, 2), GetBool(i, 3));
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

        public Entity FindRoom(int id)
        {
            try
            {
                Connect();
                StoredProcedure(SP_FIND_ROOM);
                AddParameter("_id", id);
                ExecuteReader();
                for (int i = 0; i < numberRecords; i++)
                {

                 room = new Room(GetInt(i, 0), GetDouble(i, 1), GetInt(i, 2), GetBool(i, 3));
                    /*
                    room.setId(GetInt(i, 0));
                    room.price = GetDouble(i, 1);
                    room.capacity = GetInt(i, 2);
                    room.status = GetBool(i, 3);*/
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

        public void DeleteRoom(Room room)
        {
            long id = room.Id;
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

        /*
        public void InsertRoom(Room room)
        {
            try
            {
                    StoredProcedure(SP_INSERT);
                    AddParameter("_precio", room.price);
                    AddParameter("_capacidad", room.capacity);
                    AddParameter("_status", room.status);
                    //    AddParameter("_capacidad", habitacion.capacidad); QUE VOY HACER AQUI ? 
                    ExecuteReader();
                    if(rowNumber > 0)
                    {
                        room.Id=GetInt(0, 0);
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
        */
    }
}
