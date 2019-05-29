using System;
using System.Data;
using System.Collections.Generic;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities.Grupo14;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo14{
    public class ResRestaurantRepository{

        /// <summary>
        ///     Metodo para agregar un clalamo
        /// </summary>
        /// <param name="reserva"></param>
        public void addReservation(Restaurant_res reserva){
            Console.WriteLine(reserva.fecha_res); 
            Console.WriteLine(reserva.cant_people);
            Console.WriteLine(reserva.date);
            Console.WriteLine(reserva.user_id);
            Console.WriteLine(reserva.rest_id);
            var resRest = PgConnection.Instance.ExecuteFunction(
            "addreservationrestaurant(@fecha, @people, @fecha_reservacion, @userId, @restaurantId)",
            reserva.fecha_res,reserva.cant_people, reserva.date, reserva.user_id, reserva.rest_id);
            
            Console.WriteLine("ID del insert: " , resRest);
        }
    }
}