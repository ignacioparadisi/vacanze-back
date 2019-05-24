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
    public class DAOReservationRoom : DAO
    {
        const String SP_SELECT = "m13_getResRooms()";
        const String SP_FIND = "m13_findByReservaHabitacionId(@_id)";
        private ReservationRoom reservationRoom;
        private DAORoom dAORoom;

        public void create(Entity e)
        {
            reservationRoom = (ReservationRoom)e;
        }

        public List<Entity> getRoomReservations()
        {
            List<Entity> roomReservationList = new List<Entity>();
            try
            {
                Connect();
                StoredProcedure(SP_SELECT);
                ExecuteReader();
                for (int i = 0; i < rowNumber; i++)
                {
                    ReservationRoom roomRes = new ReservationRoom(GetInt(i, 0), GetBool(i, 1), GetDateTime(i, 2), GetDateTime(i, 3), GetInt(i, 4));

                    /*roomRes.setId(GetInt(i, 0));
                    roomRes.status = GetBool(i, 1);
                    roomRes.CheckIn = GetDateTime(i, 2);
                    roomRes.CheckOut = GetDateTime(i, 3);
                    roomRes.room_id = GetInt(i, 4);*/
                   // roomRes.room = (Room) dAORoom.findRoom(GetInt(i,4)); NO ESTA CARGANDO EL OBJECT
                   // roomRes.room.setId(GetInt(i,4); NO ESTA CARGANDO EL OBJECT.

                    roomReservationList.Add(roomRes);
                }
                return roomReservationList;
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
            return roomReservationList;
        }

        public Entity Find(int id)
        {
            try
            {
                Connect();
                StoredProcedure(SP_FIND);
                AddParameter("_id", id);
                ExecuteReader();
                for (int i = 0; i < rowNumber; i++)
                {
                    reservationRoom = new ReservationRoom(GetInt(i, 0), GetBool(i, 1), GetDateTime(i, 2), GetDateTime(i, 3), GetInt(i, 4));
                    /*
                    reservationRoom.setId(GetInt(i, 0));
                    reservationRoom.status = GetBool(i, 1);
                    reservationRoom.CheckIn = GetDateTime(i, 2);
                    reservationRoom.CheckOut = GetDateTime(i, 3);
                    reservationRoom.room_id = GetInt(i, 4);*/
                }
                return reservationRoom;
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
            return reservationRoom;
        }
    }
}
