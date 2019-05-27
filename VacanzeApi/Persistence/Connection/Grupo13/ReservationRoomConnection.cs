using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Persistence.Connection;

namespace vacanze_back.VacanzeApi.Persistence.Connection.Grupo13
{
    public class ReservationRoomConnection : Connection
    {
        const String SP_SELECT = "m13_getResRooms()";
        const String SP_FIND = "m13_findByReservaHabitacionId(@_id)";
        const String SP_AVAILABLE = "m13_getAvailableRooms(@_checkin, @_checkout)";
        const String SP_ADD_RESERVATION = "m13_addRoomReservation (@_status, @_checkin, @_checkout,@_roo_fk,@_use_fk)";
        const String SP_DELETE_RESERVATION ="";
        const String SP_ADD_PAYMENT = "";

        private ReservationRoom reservationRoom;
        private RoomConnection dAORoom;

        public void create(Entity e)
        {
            reservationRoom = (ReservationRoom)e;
        }

        /** Method GetRoomReservations()
         * Returns all room reservation from the system.
         */
        public List<Entity> getRoomReservations()
        {
            try
            {
                List<Entity> roomReservationList = new List<Entity>();
                RoomConnection dAORoom = new RoomConnection();

                Connect();
                StoredProcedure(SP_SELECT);
                ExecuteReader();
                for (int i = 0; i < numberRecords; i++)
                {
                    ReservationRoom roomRes = new ReservationRoom(GetInt(i, 0), GetBool(i, 1), GetDateTime(i, 2), GetDateTime(i, 3));
                    roomRes.room = (Room) dAORoom.FindRoom(GetInt(i, 4));

                    roomReservationList.Add(roomRes);
                }
                return roomReservationList;
            }
            catch (NpgsqlException e)
            {
                e.ToString();
                throw;
            }
            catch (Exception e)
            {
                e.ToString();
                throw;
            }
            finally
            {
                Disconnect();
            }
        }

        /** Method Find(int id)
         * @param id : int
         * Returns the room reservation with the id that was passed.
         */
        public Entity Find(int id)
        {
            try
            {
                Connect();
                StoredProcedure(SP_FIND);
                AddParameter("_id", id);
                ExecuteReader();
                for (int i = 0; i < numberRecords; i++)
                {
                    reservationRoom = new ReservationRoom(GetInt(i, 0), GetBool(i, 1), GetDateTime(i, 2), GetDateTime(i, 3));
                    reservationRoom.room = (Room)dAORoom.FindRoom(GetInt(i, 4));
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

        /** Method GetAvailableRoomReservations()
         * Returns all room reservations from the system which are available within the range of dates that were passed.
         */
        public List<Entity> GetAvailableRoomReservations(DateTime checkIn, DateTime checkOut)
        {
            List<Entity> roomReservationList = new List<Entity>();
            try
            {
                Connect();
                StoredProcedure(SP_AVAILABLE);
                AddParameter("_checkin", checkIn);
                AddParameter("_checkout", checkOut);
                ExecuteReader();
                for (int i = 0; i < numberRecords; i++)
                {
                    ReservationRoom roomRes = new ReservationRoom(GetInt(i, 0), GetBool(i, 1), GetDateTime(i, 2), GetDateTime(i, 3));
                    roomRes.room = (Room)dAORoom.FindRoom(GetInt(i, 4));

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

        /** Method Add()
         * Inserts in the DataBase the room reservation that was passed.
         */
        //Falta el user
        public void Add(Entity entity)
        {
            try
            {
                ReservationRoom reservationRoom = (ReservationRoom)entity;
                int res_id = 0;

                Connect();
                StoredProcedure(SP_ADD_RESERVATION);

                AddParameter("_status", reservationRoom.status);
                AddParameter("_checkin", reservationRoom.checkIn);
                AddParameter("_checkout", reservationRoom.checkOut);
                AddParameter("_roo_fk", reservationRoom.room.Id);
            //    AddParameter("_use_fk",reservationRoom.user.Id);

                ExecuteReader();

                if (numberRecords > 0)
                {
                    res_id = GetInt(0, 0);
                }
                Disconnect();
            }
            catch (NpgsqlException e)
            {

                Console.WriteLine(e.ToString());
                Disconnect();
                throw new Exception();
            }
        }
    }
}
