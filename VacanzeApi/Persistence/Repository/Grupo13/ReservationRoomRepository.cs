//using Npgsql;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;
//using vacanze_back.VacanzeApi.Common.Entities;
//using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
//using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
//using vacanze_back.VacanzeApi.Common.Exceptions;
//using vacanze_back.VacanzeApi.Persistence.Repository.Grupo6;
//
//namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo13
//{
//    public class ReservationRoomRepository
//    {
//        const String SP_SELECT = "m13_getResRooms()";
//        const String SP_FIND = "m13_findbyroomreservationid(@_id)";
//        const String SP_AVAILABLE = "getAvailableRoomsBasedOnReservationByHotelId(@_id)";
//        const String SP_ADD_RESERVATION = "m13_addRoomReservation(@_checkin, @_checkout,@_use_fk,@_hot_fk)";
//        const String SP_UPDATE = "m13_updatehotelreservation(@_checkin,@_checkout,@_use_fk,@_hot_fk,@_id)";
//        const String SP_DELETE_RESERVATION = "m13_deleteRoomReservation(@_rooid)";
//        const String SP_ALL_BY_USER_ID = "m13_getresroobyuserandroomid(@_id)";
//        const String SP_ADD_PAYMENT = "m13_modifyReservationRoomPayment(@_pay,@_id)";
//
//        private ReservationRoom _reservationRoom;
//
//        /** <summary>
//         * Busca en la BD, la reserva que posee el identificador suministrado
//         * </summary>
//         * <param name="id">El identificador de la entidad reserva de habitacion a buscar</param>
//         */
//        public Entity Find(int id)
//        {
////            try
////            {
////                var table = PgConnection.Instance.ExecuteFunction(SP_FIND, id);
////
////                for (int i = 0; i < table.Rows.Count; i++)
////                {
////                    var id2 = Convert.ToInt32(table.Rows[i][0]);
////                    var pickup = Convert.ToDateTime(table.Rows[i][1]);
////                    var returndate = Convert.ToDateTime(table.Rows[i][2]);
////                    var timestamp = Convert.ToDateTime(table.Rows[i][3]);
////                    var userid = Convert.ToInt32(table.Rows[i][5]);
////                    var hot_fk = Convert.ToInt64(table.Rows[i][5]);
////
////                    // var payfk = Convert.ToInt64(table.Rows[i][6]);
////                    _reservationRoom = new ReservationRoom(id2, pickup, returndate);
////                    _reservationRoom.Hotel = HotelRepository.GetHotelById(Convert.ToInt32(table.Rows[i][4]));
////                    _reservationRoom.Fk_user = userid;
////                    //  _reservation.User.Id = userid;
////                    //Falta Payment
////                }
//            return _reservationRoom;
//        }
//    
//
//        /** Method GetAvailableRoomReservations()
//         * Returns all room reservations from the system which are available within the range of dates that were passed.
//         */
//
//        public static int GetAvailableRoomReservations(int id)
//        {
////            int available = 0;
////            try
////            {
////                var table = PgConnection.Instance.ExecuteFunction(SP_AVAILABLE, id);
////                
////                for (int i = 0; i < table.Rows.Count; i++)
////                {
////                    available = (int)Convert.ToInt64(table.Rows[i][0]);
////                }
////                return available;
////            }
////            catch (NpgsqlException e)
////            {
////                e.ToString();
////            }
////            catch (Exception e)
////            {
////                e.ToString();
////            }
////            return available;
//        return 0;
//        }
//
//        /** <summary>
//         * Borra de la BD, la reservacion que es suministrada
//         * </summary>
//         * <param name="entity">La entidad reservacion a borrar de la BD</param>
//         */
//        public int Delete(Entity entity)
//        {
//            try
//            {
//                ReservationRoom reservation = (ReservationRoom)entity;
//                var table = PgConnection.Instance.ExecuteFunction(
//                   SP_DELETE_RESERVATION,
//                   (int)reservation.Id
//               );
//                if (table.Rows.Count > 0)
//                {
//                    return Convert.ToInt32(table.Rows[0][0]);
//                }
//                return 0;
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.ToString());
//                throw;
//            }
//        }
//
//        /** <summary>
//         * Actualiza en la BD, la reservacion de habitacion
//         * </summary>
//         * <param name="entity">La reserva a actualizar</param>
//         */
//        public void Update(Entity entity)
//        {
////            try
////            {
////                ReservationRoom reservation = (ReservationRoom)entity;
////                PgConnection.Instance.ExecuteFunction(
////                    SP_UPDATE,
////                    reservation.CheckIn,
////                    reservation.CheckOut,
////                    reservation.Fk_user,
////                    (int)reservation.Hotel.Id,
////                   (int)reservation.Id
////                );
////            }
////            catch (DatabaseException ex)
////            {
////
////                Console.WriteLine(ex.ToString());
////                throw new Exception("Ups, a ocurrido un error al conectarse a la base de datos", ex);
////            }
////            finally
////            {
////            }
//        }
//
//    }
//}
