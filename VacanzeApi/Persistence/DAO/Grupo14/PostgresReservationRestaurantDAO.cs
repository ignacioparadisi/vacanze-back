using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities.Grupo14;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo14;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo14
{
    public class PostgresReservationRestaurantDAO : IReservationRestaurantDAO
    {

        // VARIABLES PARA LA DEFINICION DE LOS SP



        /// <summary>
        ///     Metodo para agregar reservaciones en restaurantes asociado a un usuario.
        /// </summary>
        /// <param name="reserva">Lista con los datos que se introdujeron para la reserva</param>
        /// <returns>
        ///     Devuelve un mensaje confirmando si la reservacion pudo ser realizada o no.   
        /// </returns>
        /// <exception cref="DatabaseException">Lanzada si ocurre un fallo al ejecutar la funcion en la base de datos </exception>
        /// <exception cref="InvalidOperationException">Lanzada si ocurre un fallo al ejecutar la funcion en la base de datos </exception>
        public int addReservation(Restaurant_res reserva)
        {

            var table = PgConnection.Instance.ExecuteFunction("getAvailability(@res_id, @res_date)",
            reserva.rest_id, reserva.fecha_res);
            var capacidad = Convert.ToInt32(table.Rows[0][0]);

            var tableRest = PgConnection.Instance.ExecuteFunction("getAvailabilityRest(@res_id)",
            reserva.rest_id);
            var capacidadRest = Convert.ToInt32(tableRest.Rows[0][0]);

            if (capacidad == 0 && reserva.cant_people < capacidadRest)
            {
                //Eso indica que puede reservar porque no hay reservas para esa hora y el numero no excede de lo disponible
                var resRest = PgConnection.Instance.ExecuteFunction(
                "addreservationrestaurant(@fecha, @people, @fecha_reservacion, @userId, @restaurantId)",
                reserva.fecha_res, reserva.cant_people, reserva.date, reserva.user_id, reserva.rest_id);

                var idRest = Convert.ToInt32(resRest.Rows[0][0]);
                Console.WriteLine("Id: ");
                Console.WriteLine(idRest);
                return idRest;
            }
            else
            {
                if (capacidad < reserva.cant_people)
                {
                    //Este if indica que se quiere reservar una cantidad mayor a la que hay disponible para ese restaurant para esa fecha
                    throw new AvailabilityException("Sorry, not availability at this hour");
                }
                else
                {
                    //if capacidad != 0 && capacidad >= reserva.cant_people
                    var resRest = PgConnection.Instance.ExecuteFunction(
                    "addreservationrestaurant(@fecha, @people, @fecha_reservacion, @userId, @restaurantId)",
                    reserva.fecha_res, reserva.cant_people, reserva.date, reserva.user_id, reserva.rest_id);

                    var idRest = Convert.ToInt32(resRest.Rows[0][0]);
                    Console.WriteLine("Id: ");
                    Console.WriteLine(idRest);
                    return idRest;
                }
            }
        }

        /// <summary>
        ///     Metodo para buscar reservaciones en restaurantes asociado a un usuario.
        /// </summary>
        /// <param name="reserva">Lista con los datos que se introdujeron para la reserva</param>
        /// <returns>
        ///     Devuelve una lista con las reservaciones hechas por un usuario.
        /// </returns>
        public List<Restaurant_res> getResRestaurant(int user) //operative with design patterns
        {
            var ReservationList = new List<Restaurant_res>();
            var table = PgConnection.Instance.ExecuteFunction("getResRestaurant(@usuario)", user); //realiza la conexion a la base de datos e implementa la busqueda con un Stored Procedure
            for (var i = 0; i < table.Rows.Count; i++)
            {
                //Orden del SP id, ciudad, pais, restaurant, direccion, fecha_res, cant_persona
                var id = Convert.ToInt32(table.Rows[i][0].ToString());
                var locationName = table.Rows[i][1].ToString();
                var pais = table.Rows[i][2].ToString();
                var restName = table.Rows[i][3].ToString();
                var address = table.Rows[i][4].ToString();
                var fecha_reservacion = table.Rows[i][5].ToString();
                var cant_persona = Convert.ToInt32(table.Rows[i][6].ToString());

                var Restaurant_res = new Restaurant_res(id, locationName, pais, restName, address, fecha_reservacion, cant_persona);
                ReservationList.Add(Restaurant_res);
            };
            return ReservationList;
        }

        /// <summary>
        ///     Metodo para buscar reservaciones no pagadas en restaurantes asociado a un usuario.
        /// </summary>
        /// <param name="user">Id con el que el usuario se encuentra registrado en el sistema</param>
        /// <returns>
        ///     Devuelve una lista con las reservaciones no pagadas por los usuarios.
        /// </returns>
        
        public List<Restaurant_res> getReservationNotPay(int user)
        {

            var ReservationList = new List<Restaurant_res>();
            var table = PgConnection.Instance.ExecuteFunction("getReservationNotPay(@usuario)", user); //realiza la conexion a la base de datos e implementa la busqueda con un Stored Procedure
            for (var i = 0; i < table.Rows.Count; i++)
            {
                //Orden del SP id, ciudad, pais, restaurant, direccion, fecha_res, cant_persona
                var id = Convert.ToInt32(table.Rows[i][0].ToString());
                var fecha_reservacion = table.Rows[i][1].ToString();
                var tipo = "Restaurantes";

                var Restaurant_res = new Restaurant_res(id, fecha_reservacion, tipo);
                ReservationList.Add(Restaurant_res);
            };
            return ReservationList;
        }

        /// <summary>
        ///     Metodo para eliminar reservaciones en restaurantes asociado a un usuario.
        /// </summary>
        /// <param name="resRestId">Id de la reserva que se realizao</param>
        /// <returns>
        ///     Devuelve un mensaje confirmando si la eliminacion de la reservacion pudo ser realizada con exito o no.   
        /// </returns>
        /// <exception cref="DatabaseException">Lanzada si ocurre un fallo al ejecutar la funcion en la base de datos </exception>
        /// <exception cref="InvalidOperationException">Lanzada si es que el valor no existe </exception>
        public int deleteResRestaurant(int resRestId)
        {
            try
            {
                var table = PgConnection.Instance.ExecuteFunction("deleteReservation(@reservationID)", resRestId); //realiza la conexion a la base de datos e implementa la busqueda con un Stored Procedure
                var deletedid = Convert.ToInt32(table.Rows[0][0]);
                Console.WriteLine(deletedid);
                return deletedid;
            }
            catch (InvalidCastException)
            {
                throw new DeleteReservationException("La reserva no existe.");
            }
            catch (DatabaseException)
            {
                throw new DeleteReservationException("Error con la base de datos.");
            }
        }

        public string updateResRestaurant(int payID, int resRestID)
        {
            try
            {
                Console.WriteLine(payID); Console.WriteLine(resRestID);
                var table = PgConnection.Instance.ExecuteFunction("modifyReservationPayment(@pay, @reservation)", payID, resRestID); //realiza la conexion a la base de datos e implementa la busqueda con un Stored Procedure
                var modifyId = Convert.ToInt32(table.Rows[0][0]);
                Console.WriteLine(modifyId);
                return modifyId.ToString();
            }
            catch (InvalidOperationException)
            {
                throw new UpdateReservationException("Error, no se pudo actualizar el pago de reserva");
            }
            catch (InvalidCastException)
            {
                throw new UpdateReservationException("Error, no se pudo encontrar y actualizar el pago de reserva");
            }
            catch (DatabaseException)
            {
                throw new UpdateReservationException("Error, no se pudo conectar con la base de datos");
            }
        }
    }
}
