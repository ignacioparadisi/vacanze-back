using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo13
{
    public interface IReservationRoomDAO
    {
        /// <summary>
        /// Busca una reservación de habitación por su ID
        /// </summary>
        /// <param name="id">ID de la reservación de habitación</param>
        /// <returns>Reservación de habitación buscada</returns>
        ReservationRoom Find(int id);
        
        /// <summary>
        /// Busca la cantidad de habitaciones disponibles en un hotel
        /// </summary>
        /// <param name="id">ID del hotel</param>
        /// <returns>Cantidad en número de habitaciones disponibles</returns>
        int GetAvailableRoomReservations(int id);
        
        /// <summary>
        /// Agrega una reservación de habitación en la base de datos
        /// </summary>
        /// <param name="reservation">Reservación que se desea agregar</param>
        /// <returns>La reservación agregada con su respectido ID</returns>
        ReservationRoom Add(ReservationRoom reservation);
       
        /// <summary>
        /// Elimina una reservación de habitación de la base de datos
        /// </summary>
        /// <param name="id">ID de la reservación que se desea eliminar</param>
        /// <returns>ID de la reservación que fue eliminada</returns>
        int Delete(int id);
        
        /// <summary>
        /// Obtiene todas las reservaciones que le pertenecen a un usuario
        /// </summary>
        /// <param name="userId">ID del usuario de quien se desea buscar las reservaciones</param>
        /// <returns>Lista de reservaciones de habitación del usuario</returns>
        List<ReservationRoom> GetAllByUserId(int userId);
        
        /// <summary>
        /// Actualización de la información de una reservación de habitación
        /// </summary>
        /// <param name="reservation">Reservación de la habitación con la información que se desea actualizar</param>
        /// <returns>Reservación de la habitación con la información actualizada</returns>
        ReservationRoom Update(ReservationRoom reservation);
    }
}