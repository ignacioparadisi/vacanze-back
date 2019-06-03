﻿using System;
using System.Collections.Generic;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo6;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo13
{
    public class ReservationRoomRepository
    {
        private const string SP_SELECT = "m13_getResRooms()";
        private const string SP_FIND = "m13_findbyroomreservationid(@_id)";
        private const string SP_AVAILABLE = "getAvailableRoomsBasedOnReservationByHotelId(@_id)";

        private const string SP_ADD_RESERVATION =
            "m13_addRoomReservation(@_checkin, @_checkout,@_use_fk,@_hot_fk)";

        private const string SP_UPDATE =
            "m13_updatehotelreservation(@_checkin,@_checkout,@_use_fk,@_hot_fk,@_id)";

        private const string SP_DELETE_RESERVATION = "m13_deleteRoomReservation(@_rooid)";
        private const string SP_ALL_BY_USER_ID = "m13_getresroobyuserandroomid(@_id)";
        private const string SP_ADD_PAYMENT = "m13_modifyReservationRoomPayment(@_pay,@_id)";

        private ReservationRoom _reservationRoom;

        public void create(Entity e)
        {
            _reservationRoom = (ReservationRoom) e;
        }

        /** <summary>
         * Trae de la BD, las reservas de habitacion
         * </summary>
         */
        public List<Entity> GetRoomReservations()
        {
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(SP_SELECT);
                var roomReservationList = new List<Entity>();

                for (var i = 0; i < table.Rows.Count; i++)
                {
                    var id = Convert.ToInt64(table.Rows[i][0]);
                    var pickup = Convert.ToDateTime(table.Rows[i][1]);
                    var returndate = Convert.ToDateTime(table.Rows[i][2]);
                    var timestamp = Convert.ToDateTime(table.Rows[i][3]);
                    var use_id = (int) Convert.ToInt64(table.Rows[i][5]);
                    // var payment = Convert.ToInt64(table.Rows[i][6]);

                    var roomRes = new ReservationRoom(id, pickup, returndate);
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
        }

        /** <summary>
         * Busca en la BD, la reserva que posee el identificador suministrado
         * </summary>
         * <param name="id">El identificador de la entidad reserva de habitacion a buscar</param>
         */
        public Entity Find(int id)
        {
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(SP_FIND, id);

                for (var i = 0; i < table.Rows.Count; i++)
                {
                    var id2 = Convert.ToInt64(table.Rows[i][0]);
                    var pickup = Convert.ToDateTime(table.Rows[i][1]);
                    var returndate = Convert.ToDateTime(table.Rows[i][2]);
                    var timestamp = Convert.ToDateTime(table.Rows[i][3]);
                    var userid = (int) Convert.ToInt64(table.Rows[i][5]);
                    var hot_fk = Convert.ToInt64(table.Rows[i][5]);

                    // var payfk = Convert.ToInt64(table.Rows[i][6]);
                    _reservationRoom = new ReservationRoom(id2, pickup, returndate);
                    _reservationRoom.Hotel =
                        HotelRepository.GetHotelById(Convert.ToInt32(table.Rows[i][4]));
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

        public static int GetAvailableRoomReservations(int id)
        {
            var available = 0;
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(SP_AVAILABLE, id);

                for (var i = 0; i < table.Rows.Count; i++)
                    available = (int) Convert.ToInt64(table.Rows[i][0]);
                return available;
            }
            catch (NpgsqlException e)
            {
                e.ToString();
            }
            catch (Exception e)
            {
                e.ToString();
            }

            return available;
        }


        /** <summary>
         * Inserta en la BD, la reservacion de habitacion que es suministrada
         * </summary> 
         * <param name="reservation">La reservacion a agregar en la BD</param>
         */

        public int Add(ReservationRoom reservation)
        {
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(SP_ADD_RESERVATION,
                    reservation.CheckIn,
                    reservation.CheckOut,
                    reservation.Fk_user,
                    reservation.Hotel.Id);

                if (table.Rows.Count > 0) return Convert.ToInt32(table.Rows[0][0]);
                return 0;
            }
            catch (DatabaseException e)
            {
                Console.WriteLine(e.ToString());
                throw new Exception();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw new Exception();
            }
        }

        /** <summary>
         * Borra de la BD, la reservacion que es suministrada
         * </summary>
         * <param name="entity">La entidad reservacion a borrar de la BD</param>
         */
        public int Delete(Entity entity)
        {
            try
            {
                var reservation = (ReservationRoom) entity;
                var table = PgConnection.Instance.ExecuteFunction(
                    SP_DELETE_RESERVATION,
                    (int) reservation.Id
                );
                if (table.Rows.Count > 0) return Convert.ToInt32(table.Rows[0][0]);
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        /** <summary>
         * Trae de la BD, las reservas de habitacion del id del usuario suministrado
         * </summary>
         * <param name="user_id">El id del usuario que posee las reservas de habitacion</param>
         */
        public List<Entity> GetAllByUserId(int user_id)
        {
            var reservationAutomobileList = new List<Entity>();
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(SP_ALL_BY_USER_ID,
                    user_id);
                for (var i = 0; i < table.Rows.Count; i++)
                {
                    var id = Convert.ToInt64(table.Rows[i][0]);
                    var pickup = Convert.ToDateTime(table.Rows[i][1]);
                    var returndate = Convert.ToDateTime(table.Rows[i][2]);
                    //current timestamp
                    //  var timestamp = Convert.ToDateTime(table.Rows[i][3]);
                    var hotfk = (int) Convert.ToInt64(table.Rows[i][3]);

                    var reservation = new ReservationRoom(id, pickup, returndate);
                    reservation.Hotel = HotelRepository.GetHotelById(hotfk);
                    reservation.Fk_user = user_id;
                    reservationAutomobileList.Add(reservation);
                }

                return reservationAutomobileList;
            }
            catch (NpgsqlException e)
            {
                e.ToString();
            }
            catch (Exception e)
            {
                e.ToString();
            }

            return reservationAutomobileList;
        }

        /** <summary>
         * Actualiza en la BD, la reservacion de habitacion
         * </summary>
         * <param name="entity">La reserva a actualizar</param>
         */
        public void Update(Entity entity)
        {
            try
            {
                var reservation = (ReservationRoom) entity;
                PgConnection.Instance.ExecuteFunction(
                    SP_UPDATE,
                    reservation.CheckIn,
                    reservation.CheckOut,
                    reservation.Fk_user,
                    reservation.Hotel.Id,
                    (int) reservation.Id
                );
            }
            catch (DatabaseException ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("Ups, a ocurrido un error al conectarse a la base de datos",
                    ex);
            }
        }
    }
}