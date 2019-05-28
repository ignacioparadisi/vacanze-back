using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo13
{
    public class ReservationRoomRepository
    {
        const String SP_SELECT = "m13_getResRooms()";
        const String SP_FIND = "m13_findByReservaHabitacionId(@_id)";
        const String SP_AVAILABLE = "m13_getAvailableRooms(@_checkin, @_checkout)";
        const String SP_ADD_RESERVATION = "m13_addRoomReservation (@_status, @_checkin, @_checkout,@_roo_fk,@_use_fk)";
        const String SP_DELETE_RESERVATION ="";
        const String SP_ADD_PAYMENT = "";

        private ReservationRoom _reservationRoom;
        private RoomConnection _roomConnection;

        public void create(Entity e)
        {
            _reservationRoom = (ReservationRoom)e;
        }

        /** Method GetRoomReservations()
         * Returns all room reservation from the system.
         */
        public List<Entity> GetRoomReservations()
        {
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(SP_SELECT);
                List<Entity> roomReservationList = new List<Entity>();
                RoomConnection _roomConnection = new RoomConnection();

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var id = Convert.ToInt64(table.Rows[i][0]);
                    var pickup = Convert.ToDateTime(table.Rows[i][1]);
                    var returndate = Convert.ToDateTime(table.Rows[i][2]);
                    var use_id = Convert.ToInt64(table.Rows[i][3]);
                    var fk_hotel = Convert.ToInt64(table.Rows[i][4]);
                    ReservationRoom roomRes = new ReservationRoom(id, pickup, returndate);
                    roomRes.Hotel.Id = fk_hotel;
                    roomRes.User.Id = use_id;

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
                var table = PgConnection.Instance.ExecuteFunction(SP_FIND, id);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var pickup = Convert.ToDateTime(table.Rows[i][1]);
                    var returndate = Convert.ToDateTime(table.Rows[i][2]);
                    var userid = Convert.ToInt64(table.Rows[i][3]);
                    var autfk = Convert.ToInt64(table.Rows[i][4]);
                    // var payfk = Convert.ToInt64(table.Rows[i][5]);
                    _reservationRoom = new ReservationRoom(id, pickup, returndate);
                    //  _reservation.User.Id = userid;
                    //  _reservation.Automobile.Id = autfk;
                    //Falta Payment
                }
                return _reservationRoom;
            }
            catch (NpgsqlException e)
            {
                e.ToString();
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return _reservationRoom;
        }

        /** Method GetAvailableRoomReservations()
         * Returns all room reservations from the system which are available within the range of dates that were passed.
         */
         /*
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
                    roomRes.Room = (Room)_roomConnection.FindRoom(GetInt(i, 4));

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
        */
        /** Method Add()
         * Inserts in the DataBase the room reservation that was passed.
         */
        //Falta el user
        /*
        public void Add(Entity entity)
        {
            try
            {
                ReservationRoom reservationRoom = (ReservationRoom)entity;
                int res_id = 0;

                Connect();
                StoredProcedure(SP_ADD_RESERVATION);

                AddParameter("_checkin", reservationRoom.CheckIn);
                AddParameter("_checkout", reservationRoom.CheckOut);
                AddParameter("_roo_fk", reservationRoom.Room.Id);
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
        */
    }
}
