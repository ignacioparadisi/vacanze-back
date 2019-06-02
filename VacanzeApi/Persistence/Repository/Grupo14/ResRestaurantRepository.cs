using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities.Grupo14;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo14;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo14{
    public class ResRestaurantRepository{

        /// <summary>
        ///     Metodo para agregar una reserva de restaurant
        /// </summary>
        /// <param name="reserva"></param>
        public void addReservation(Restaurant_res reserva){
            Console.WriteLine(reserva.fecha_res); 
            Console.WriteLine(reserva.cant_people);
            Console.WriteLine(reserva.date);
            Console.WriteLine(reserva.user_id);
            Console.WriteLine(reserva.rest_id);
            var availability = PgConnection.Instance.ExecuteFunction("getAvailability(@res_id, @res_date)",
            reserva.rest_id, reserva.fecha_res);
            var id = Convert.ToInt32(availability.Rows[0][0].ToString());

            Console.WriteLine(id);

            if(id <=0){
                throw new AvailabilityException("Sorry, not availability at this hour");
            }
            var resRest = PgConnection.Instance.ExecuteFunction(
            "addreservationrestaurant(@fecha, @people, @fecha_reservacion, @userId, @restaurantId)",
            reserva.fecha_res,reserva.cant_people, reserva.date, reserva.user_id, reserva.rest_id);
            
            Console.WriteLine("ID del insert: " , resRest);
        }
    
         
        public List<Restaurant_res> getResRestaurant(int user){

            var ReservationList = new List<Restaurant_res>();
            var table = PgConnection.Instance.ExecuteFunction("getResRestaurant(@userId)",user);
            for (var i = 0; i < table.Rows.Count; i++){

                var id = Convert.ToInt32(table.Rows[i][0].ToString());
                var fecha_for_res = table.Rows[i][1].ToString();
                var number_people =  Convert.ToInt32(table.Rows[i][2].ToString());
                var fecha_que_reservo = table.Rows[i][3].ToString();
                var userID =  Convert.ToInt32(table.Rows[i][4].ToString());
                var restaurantID =  Convert.ToInt32(table.Rows[i][5].ToString());
                var Restaurant_res = new Restaurant_res(id, fecha_for_res,number_people,fecha_que_reservo,userID,restaurantID);
                ReservationList.Add(Restaurant_res);
            };
            return ReservationList;
        }

        public int deleteResRestaurant(int resRestId){
            try{
                var table = PgConnection.Instance.ExecuteFunction("deleteReservation(@reservationID)",resRestId);
                var deletedid = Convert.ToInt32(table.Rows[0][0]);
                Console.WriteLine(deletedid);
                return deletedid;
            }
            catch(InvalidCastException){
                return -1;
            }
        }

        public int updateResRestaurant(int payID, int resRestID){
            try{
                Console.WriteLine(payID); Console.WriteLine(resRestID);
                var table = PgConnection.Instance.ExecuteFunction("modifyReservationPayment(@pay, @reservation)", payID, resRestID);
                var deletedid = Convert.ToInt32(table.Rows[0][0]);
                Console.WriteLine(deletedid);
                return deletedid;
            }
            catch(InvalidCastException){
                return -1;
            }
        }
    }
}