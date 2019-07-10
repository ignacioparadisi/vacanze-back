using Newtonsoft.Json;
using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo13
{
    public class ReservationRoom : Entity
    {
        //Checkin: Initial date of the reservation
        public DateTime CheckIn { get; set; }
        //Checkout: Last date of the reservation
        public DateTime CheckOut { get; set; }
        //Hotel of the reservation
        public int HotelId { get; set; }

        public int UserId { get; set; }

        public int PaymentId { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase ReservationRoom.
        /// </summary>
        /// 
        public ReservationRoom() : base(0)
        {
        }
        
        [JsonConstructor]
        public ReservationRoom(int id, DateTime checkIn, DateTime checkOut, int hotelId, int userId) : base(id)
        {
            if (IsValid(id, checkIn, checkOut, hotelId, userId))
            {
                CheckIn = checkIn;
                CheckOut = checkOut;
                HotelId = hotelId;
                UserId = userId;
            }
        }

        /// <summary>
        /// Valida los atributos de la reservacion antes de crearla
        /// </summary>
        /// <param name="id">Id de la reservación</param>
        /// <param name="checkIn">Fecha de checkin</param>
        /// <param name="checkOut">Fecha de checkout</param>
        /// <param name="hotelId">Id del hotel en donde se desea hacer la reservacion</param>
        /// <param name="userId">Id del usuario qeu está haciendo la reservación</param>
        /// <returns></returns>
        /// <exception cref="NotValidIdException">Se lanza cuando el id es menor a 0</exception>
        /// <exception cref="GeneralException">Se lanza si la fecha de checkin es mayor que la de checkout</exception>
        private bool IsValid(int id, DateTime checkIn, DateTime checkOut, int hotelId, int userId)
        {
            if (id < 0)
            {
                throw new NotValidIdException("El ID de la reservación no es válido.");
            }

            if (hotelId < 0)
            {
                throw new NotValidIdException("El ID del hotel no es válido.");
            }

            if (userId < 0)
            {
                throw new NotValidIdException("El ID del usuario no es válido.");
            }

            if (checkIn > checkOut)
            {
                throw new GeneralException("La fecha de salida no puede ser menor a la fecha de ingreso");
            }
            return true;
        }
    }
}
