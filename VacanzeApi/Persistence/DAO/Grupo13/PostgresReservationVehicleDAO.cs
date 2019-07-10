using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo5;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo13;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo13
{
    public class PostgresReservationVehicleDAO : IReservationVehicleDAO
    {
        const String SP_SELECT = "m13_getResAutos()";

        //   const String SP_AVAILABLE = "m13_getavailableautomobilereservations('01/03/2019', '01/05/2019')";
        const String SP_FIND = "m13_findByResAutId(@_id)";
        const String SP_ADD = "m13_addautomobilereservation(@_checkin,@_checkout,@_use_fk,@_ra_aut_fk)";
        const String SP_UPDATE = "m13_updateautomobilereservation(@_checkin,@_checkout,@_use_fk,@_ra_aut_fk,@_ra_id)";
        const String SP_DELETE = "m13_deleteautomobilereservation(@_id)";

        const String SP_ALL_BY_USER_ID = "m13_getresautomobilebyuserid(@_id)";

        //   const String SP_ADD_PAYMENT = "m13_modifyReservationRoomPayment(@_pay,@_id)";
        private Auto _automobile;
        private ReservationVehicle _reservation;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">El id del auto que se va a buscar</param>
        /// <returns name = "_reservation">La reserva para el vehiculo con el id introducido</returns>
        public ReservationVehicle Find(int id)
        {
            ReservationVehicle reservation = new ReservationVehicle();
            var table = PgConnection.Instance.ExecuteFunction(SP_FIND, id);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var reservationId = Convert.ToInt32(table.Rows[i][0]);
                var pickup = Convert.ToDateTime(table.Rows[i][1]);
                var returndate = Convert.ToDateTime(table.Rows[i][2]);
                var userId = (int) Convert.ToInt64(table.Rows[i][4]);
                var vehicleId = (int) Convert.ToInt64(table.Rows[i][5]);
                // var payfk = Convert.ToInt64(table.Rows[i][5]);
                reservation = EntityFactory.CreateReservationVehicle(reservationId, pickup, 
                    returndate, vehicleId, userId);

                //  _reservation.User.Id = userid;
                //  _reservation.Automobile.Id = autfk;
                // Falta Payment
            }

            return reservation;
        }
        
        public ReservationVehicle AddReservation(ReservationVehicle reservation)
        {
            var table = PgConnection.Instance.ExecuteFunction(SP_ADD,
                reservation.CheckIn,
                reservation.CheckOut,
                reservation.UserId,
                reservation.VehicleId);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                int id = (int) Convert.ToInt64(table.Rows[i][0]);
                reservation.Id = id;
            }

            return reservation;
        }

        /** <summary>Borra de la BD, la reservacion que es suministrada</summary>
         * <param name="entity">La entidad reservacion a borrar de la BD</param>
         */
        public int Delete(int id)
        {
            var table = PgConnection.Instance.ExecuteFunction(SP_DELETE, id);
            if (table.Rows.Count > 0)
            {
                return Convert.ToInt32(table.Rows[0][0]);
            }
            throw new NotValidIdException("El ID de la reservación no es válido.");
        }

        /** <summary>Trae de la BD, las reservas de automoviles de un usuario</summary>
         * <param name="user_id">El id del usuario que posee las reservas</param>
         */
        public List<ReservationVehicle> GetAllByUserId(int userId)
        {
            List<ReservationVehicle> reservationAutomobileList = new List<ReservationVehicle>();

            var table = PgConnection.Instance.ExecuteFunction(SP_ALL_BY_USER_ID,
                userId);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var id = Convert.ToInt32(table.Rows[i][0]);
                var pickup = Convert.ToDateTime(table.Rows[i][1]);
                var returndate = Convert.ToDateTime(table.Rows[i][2]);
                //current timestamp
                var timestamp = Convert.ToDateTime(table.Rows[i][3]);
                var vehicleId = (int) Convert.ToInt64(table.Rows[i][4]);
                var userid = Convert.ToInt64(table.Rows[i][5]);

                ReservationVehicle reservation = EntityFactory.CreateReservationVehicle(id, pickup, returndate);
                reservation.VehicleId = vehicleId;
                reservation.UserId = userId;
                reservationAutomobileList.Add(reservation);
            }

            return reservationAutomobileList;
        }

        /** <summary>Actualiza en la BD, la reservacion de automovil</summary>
         * <param name="entity">La reserva a actualizar</param>
         */
        public ReservationVehicle Update(ReservationVehicle reservation)
        {
            var table = PgConnection.Instance.ExecuteFunction(SP_UPDATE,
                reservation.CheckIn,
                reservation.CheckOut,
                reservation.UserId,
                reservation.VehicleId,
                reservation.Id);
            foreach (DataRow row in table.Rows)
            {
                reservation.Id = Convert.ToInt32(row[0]);
                reservation.CheckIn = Convert.ToDateTime(row[1]);
                reservation.CheckOut = Convert.ToDateTime(row[2]);
                reservation.UserId = Convert.ToInt32(row[3]);
                reservation.VehicleId = Convert.ToInt32(row[4]);

            }
            return reservation;
        }
    }
}