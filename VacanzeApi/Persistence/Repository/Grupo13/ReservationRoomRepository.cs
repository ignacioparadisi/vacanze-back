using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo6;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo13
{
    public class ReservationRoomRepository
    {
        const String SP_SELECT = "m13_getResRooms()";
        const String SP_FIND = "m13_findbyroomreservationid(@_id)";
        const String SP_AVAILABLE = "m13_getAvailableRooms(@_checkin, @_checkout)";
        const String SP_ADD_RESERVATION = "m13_addRoomReservation(@_checkin, @_checkout,@_use_fk,@_hot_fk)";
        const String SP_DELETE_RESERVATION = "m13_deleteRoomReservation(@_rooid)";
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
                    var timestamp = Convert.ToDateTime(table.Rows[i][3]);
                    //var fk_hotel = Convert.ToInt32(table.Rows[i][4]);
                    var use_id = (int) Convert.ToInt64(table.Rows[i][5]);
                   // var payment = Convert.ToInt64(table.Rows[i][6]);
                    
                    ReservationRoom roomRes = new ReservationRoom(id, pickup, returndate);
                    roomRes.Hotel = HotelRepository.GetHotelById(Convert.ToInt32(table.Rows[i][4]));
                    roomRes.Fk_user = use_id;

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
                    var id2 = Convert.ToInt64(table.Rows[i][0]);
                    var pickup = Convert.ToDateTime(table.Rows[i][1]);
                    var returndate = Convert.ToDateTime(table.Rows[i][2]);
                    var timestamp = Convert.ToDateTime(table.Rows[i][3]);
                    var userid = (int)Convert.ToInt64(table.Rows[i][5]);
                    var hot_fk = Convert.ToInt64(table.Rows[i][5]);

                    // var payfk = Convert.ToInt64(table.Rows[i][6]);
                    _reservationRoom = new ReservationRoom(id2, pickup, returndate);
                    _reservationRoom.Hotel = HotelRepository.GetHotelById(Convert.ToInt32(table.Rows[i][4]));
                    _reservationRoom.Fk_user = userid;
                    //  _reservation.User.Id = userid;
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

        public void Add(ReservationRoom reservation)
        {
            try
            {
                var table = PgConnection.Instance.
                    ExecuteFunction(SP_ADD_RESERVATION,
                        reservation.CheckIn,
                        reservation.CheckOut,
                        reservation.Fk_user,
                        (int)reservation.Hotel.Id);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public void Delete(Entity entity)
        {
            try
            {
                ReservationRoom reservation = (ReservationRoom) entity;
                var table = PgConnection.Instance.ExecuteFunction(
                   SP_DELETE_RESERVATION,
                   (int)reservation.Id
               );
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

    }
}
