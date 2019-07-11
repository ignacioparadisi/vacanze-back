using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo13;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo13
{
    public class PostgresReservationRoomDAO : IReservationRoomDAO
    {
        private const string SP_SELECT = "m13_getResRooms()";
        private const string SP_FIND = "m13_findbyroomreservationid(@_id)";
        private const string SP_AVAILABLE = "getAvailableRoomsBasedOnReservationByHotelId(@_id)";
        private const string SP_ADD_RESERVATION = "m13_addRoomReservation(@_checkin, @_checkout,@_use_fk,@_hot_fk)";
        private const string SP_UPDATE = "m13_updatehotelreservation(@_checkin,@_checkout,@_id)";
        private const string SP_DELETE_RESERVATION = "m13_deleteRoomReservation(@_rooid)";
        private const string SP_ALL_BY_USER_ID = "m13_getresroobyuserandroomid(@_id)";

        /// <summary>
        /// Busca una reservación de habitación por su ID
        /// </summary>
        /// <param name="id">ID de la reservación de habitación</param>
        /// <returns>Reservación de habitación buscada</returns>
        public ReservationRoom Find(int id)
        {
            var reservationRoom = EntityFactory.CreateReservationRoom();
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(SP_FIND, id);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    reservationRoom = GetReservationRoomFromTable(table, i);
                    // Falta Payment
                }

                if (table.Rows.Count == 0)
                {
                    throw new RoomReservationNotFoundException("No se ha encontrado la reservación de la habitación.");
                }

                return reservationRoom;
            }
            catch (Exception)
            {
                throw new GeneralException("Error Obteniendo la Reserva de Habitación");
            }
        }

        /// <summary>
        /// Busca la cantidad de habitaciones disponibles en un hotel
        /// </summary>
        /// <param name="id">ID del hotel</param>
        /// <returns>Cantidad en número de habitaciones disponibles</returns>
        public int GetAvailableRoomReservations(int id)
        {
            int available = 0;
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(SP_AVAILABLE, id);

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    available = Convert.ToInt32(table.Rows[i][0]);
                }

                return available;
            }
            catch (Exception e)
            {
                throw new GeneralException("Error Obteniendo las Habitaciones Disponibles");
            }
        }


        /// <summary>
        /// Agrega una reservación de habitación en la base de datos
        /// </summary>
        /// <param name="reservation">Reservación que se desea agregar</param>
        /// <returns>La reservación agregada con su respectido ID</returns>
        public ReservationRoom Add(ReservationRoom reservation)
        {
            try
            {
                var table = PgConnection.Instance.ExecuteFunction(SP_ADD_RESERVATION,
                    reservation.CheckIn,
                    reservation.CheckOut,
                    reservation.UserId,
                    reservation.HotelId);

                ReservationRoom savedRoom = reservation;
                if (table.Rows.Count > 0)
                {
                    for (var i = 0; i < table.Rows.Count; i++)
                    {
                        savedRoom.Id = Convert.ToInt32((table.Rows[i][0]));
                    }
                    return savedRoom;
                }
                throw new GeneralException("Error agregando la reservación de habitación");
            }
            catch (NpgsqlException)
            {
                throw new GeneralException("Error Agregando la Reserva de Habitación");
            }
        }

        /// <summary>
        /// Elimina una reservación de habitación de la base de datos
        /// </summary>
        /// <param name="id">ID de la reservación que se desea eliminar</param>
        /// <returns>ID de la reservación que fue eliminada</returns>
        public int Delete(int id)
        {
            var table = PgConnection.Instance.ExecuteFunction(SP_DELETE_RESERVATION, id);
            return Convert.ToInt32(table.Rows[0][0]);
        }

        /// <summary>
        /// Obtiene todas las reservaciones que le pertenecen a un usuario
        /// </summary>
        /// <param name="userId">ID del usuario de quien se desea buscar las reservaciones</param>
        /// <returns>Lista de reservaciones de habitación del usuario</returns>
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
                    var checkInDate = Convert.ToDateTime(table.Rows[i][1]);
                    var checkOutDate = Convert.ToDateTime(table.Rows[i][2]);
                    var hotelId = Convert.ToInt32(table.Rows[i][3]);

                    ReservationRoom reservation =
                        EntityFactory.CreateReservationRoom(id, checkInDate, checkOutDate, hotelId, userId);
                    reservationAutomobileList.Add(reservation);
                }

                return reservationAutomobileList;
            }
            catch (Exception e)
            {
                throw new GeneralException("Error Obteniendo las Reservas de Habitación");
            }
        }

        /// <summary>
        /// Actualización de la información de una reservación de habitación
        /// </summary>
        /// <param name="reservation">Reservación de la habitación con la información que se desea actualizar</param>
        /// <returns>Reservación de la habitación con la información actualizada</returns>
        public ReservationRoom Update(ReservationRoom reservation)
        {
            try
            {
                var updatedReservation = EntityFactory.CreateReservationRoom();
                var table = PgConnection.Instance.ExecuteFunction(
                    SP_UPDATE,
                    reservation.CheckIn,
                    reservation.CheckOut,
                    reservation.Id
                );
                for (var i = 0; i < table.Rows.Count; i++)
                {
                    var id = Convert.ToInt32(table.Rows[i][0]);
                    var checkInDate = Convert.ToDateTime(table.Rows[i][1]);
                    var checkOutDate = Convert.ToDateTime(table.Rows[i][2]);
                    var hotelId = Convert.ToInt32(table.Rows[i][4]);
                    var userId = Convert.ToInt32(table.Rows[i][3]);
                    updatedReservation = EntityFactory.CreateReservationRoom(id, checkInDate, checkOutDate, hotelId, userId);
                }
                return updatedReservation;
            }
            catch (Exception e)
            {
                throw new GeneralException(e.Message);
                // throw new GeneralException("Error Actualizando la Reserva de Habitación");
            }
        }
        
        private ReservationRoom GetReservationRoomFromTable(DataTable table, int row) {
            var id = Convert.ToInt32(table.Rows[row][0]);
            var checkInDate = Convert.ToDateTime(table.Rows[row][1]);
            var checkOutDate = Convert.ToDateTime(table.Rows[row][2]);
            var hotelId = Convert.ToInt32(table.Rows[row][4]);
            var userId = Convert.ToInt32(table.Rows[row][5]);
            return EntityFactory.CreateReservationRoom(id, checkInDate, checkOutDate, hotelId, userId);
        }
    }
}