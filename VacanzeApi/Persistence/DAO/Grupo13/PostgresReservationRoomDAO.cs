using System;
using System.Collections.Generic;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo6;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo13
{
    public class PostgresReservationRoomDAO: IReservationRoomDAO
    {
        private const string SP_SELECT = "m13_getResRooms()";
        private const string SP_FIND = "m13_findbyroomreservationid(@_id)";
        private const string SP_AVAILABLE = "getAvailableRoomsBasedOnReservationByHotelId(@_id)";
        private const string SP_ADD_RESERVATION = "m13_addRoomReservation(@_checkin, @_checkout,@_use_fk,@_hot_fk)";
        private const string SP_UPDATE = "m13_updatehotelreservation(@_checkin,@_checkout,@_use_fk,@_hot_fk,@_id)";
        private const string SP_DELETE_RESERVATION = "m13_deleteRoomReservation(@_rooid)";
        private const string SP_ALL_BY_USER_ID = "m13_getresroobyuserandroomid(@_id)";
        
        /** <summary>
         * Trae de la BD, las reservas de habitacion
         * </summary>
         */
        public List<ReservationRoom> GetRoomReservations()
        {
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(SP_SELECT);
                List<ReservationRoom> roomReservationList = new List<ReservationRoom>();

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var id = Convert.ToInt32(table.Rows[i][0]);
                    var pickup = Convert.ToDateTime(table.Rows[i][1]);
                    var returndate = Convert.ToDateTime(table.Rows[i][2]);
                    var userId = Convert.ToInt32(table.Rows[i][5]);

                    ReservationRoom roomRes = EntityFactory.CreateReservationRoom(id, pickup, returndate);

                    DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                    HotelDAO hotelDao = factory.GetHotelDAO();
                    roomRes.Hotel = hotelDao.GetHotelById(Convert.ToInt32(table.Rows[i][4]));
                    roomRes.Fk_user = userId;

                    roomReservationList.Add(roomRes);
                }

                return roomReservationList;
            }
            catch (Exception e)
            {
                throw new GeneralException("Error Obteniendo las Reservas de Habitación");
            }
        }
        
        /** <summary>
         * Busca en la BD, la reserva que posee el identificador suministrado
         * </summary>
         * <param name="id">El identificador de la entidad reserva de habitacion a buscar</param>
         */
        public ReservationRoom Find(int id)
        {
            var reservationRoom = EntityFactory.CreateReservationRoom();
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(SP_FIND, id);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var id2 = Convert.ToInt32(table.Rows[i][0]);
                    var pickup = Convert.ToDateTime(table.Rows[i][1]);
                    var returndate = Convert.ToDateTime(table.Rows[i][2]);
                    var userid = Convert.ToInt32(table.Rows[i][5]);
                    var hot_fk = Convert.ToInt64(table.Rows[i][5]);

                    
                    reservationRoom = EntityFactory.CreateReservationRoom(id2, pickup, returndate);
                    
                    DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                    HotelDAO hotelDao = factory.GetHotelDAO();
                    reservationRoom.Hotel = hotelDao.GetHotelById(Convert.ToInt32(table.Rows[i][4]));
                    reservationRoom.Fk_user = userid;
                    //Falta Payment
                }
                if (table.Rows.Count == 0)
                {
                    throw new RoomReservationNotFoundException();
                }
                return reservationRoom;
            }
            catch (Exception e)
            {
                throw new GeneralException("Error Obteniendo la Reserva de Habitación");
            }
            return reservationRoom;
        }

        /** Method GetAvailableRoomReservations()
         * Returns all room reservations from the system which are available within the range of dates that were passed.
         */

        public int GetAvailableRoomReservations(int id)
        {
            int available = 0;
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(SP_AVAILABLE, id);
                
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    available = (int)Convert.ToInt64(table.Rows[i][0]);
                }
                
                return available;
            }
            catch (Exception e)
            {
                throw new GeneralException("Error Obteniendo las Habitaciones Disponibles");
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
                 var table=   PgConnection.Instance.
                    ExecuteFunction(SP_ADD_RESERVATION,
                        reservation.CheckIn,
                        reservation.CheckOut,
                        reservation.Fk_user,
                        (int)reservation.Hotel.Id);

                if (table.Rows.Count > 0)
                {
                    return Convert.ToInt32(table.Rows[0][0]);
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new GeneralException("Error Agregando la Reserva de Habitación");
            }
        }

        /// <summary>
        /// Borra de la BD, la reservacion que es suministrada
        /// </summary>
        /// <param name="reservation">La entidad reservacion a borrar de la BD</param>
        public int Delete(int id)
        {
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(
                   SP_DELETE_RESERVATION, id);
                if (table.Rows.Count > 0)
                {
                    return Convert.ToInt32(table.Rows[0][0]);
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new GeneralException("Error Eliminando la Reserva de Habitación");
            }
        }

        /// <summary>
        /// Trae de la BD, las reservas de habitación del id del usuario suministrado
        /// </summary>
        /// <param name="user_id">El id del usuario que posee las reservas de habitación</param>
        /// <returns> Una lista de reservas de habitaciones </returns>
        public List<ReservationRoom> GetAllByUserId(int userId)
        {
            List<ReservationRoom> reservationAutomobileList = new List<ReservationRoom>();
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(SP_ALL_BY_USER_ID,
                    userId);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var id = Convert.ToInt32(table.Rows[i][0]);
                    var pickup = Convert.ToDateTime(table.Rows[i][1]);
                    var returndate = Convert.ToDateTime(table.Rows[i][2]);
                    //current timestamp
                    //  var timestamp = Convert.ToDateTime(table.Rows[i][3]);
                    var hotfk = (int)Convert.ToInt64(table.Rows[i][3]);

                    ReservationRoom reservation = EntityFactory.CreateReservationRoom(id, pickup, returndate);
                    DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                    HotelDAO hotelDao = factory.GetHotelDAO();
                    reservation.Hotel = hotelDao.GetHotelById(hotfk);
                    reservation.Fk_user = userId;
                    reservationAutomobileList.Add(reservation);
                }
                return reservationAutomobileList;
            }
            catch (Exception e)
            {
                throw new GeneralException("Error Obteniendo las Reservas de Habitación");
            }
            return reservationAutomobileList;
        }

        /** <summary>
         * Actualiza en la BD, la reservacion de habitacion
         * </summary>
         * <param name="entity">La reserva a actualizar</param>
         */
        public void Update(ReservationRoom reservation)
        {
            try
            {
                PgConnection.Instance.ExecuteFunction(
                    SP_UPDATE,
                    reservation.CheckIn,
                    reservation.CheckOut,
                    reservation.Fk_user,
                    reservation.Hotel.Id,
                   reservation.Id
                );
            }
            catch (Exception e)
            {
                throw new GeneralException("Error Actulizando la Reserva de Habitación");
            }
        }
    }
}