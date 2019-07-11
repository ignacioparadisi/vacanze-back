using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo13
{
    public interface IReservationVehicleDAO
    {
        /// <summary>
        /// Busca una reservación de vehículo por su ID
        /// </summary>
        /// <param name="id">ID de la reservación de vehículo</param>
        /// <returns>Reservación de vehículo buscada</returns>
        ReservationVehicle Find(int id);
        
        /// <summary>
        /// Elimina una reservación de vehículo de la base de datos
        /// </summary>
        /// <param name="id">ID de la reservación que se desea eliminar</param>
        /// <returns>ID de la reservación que fue eliminada</returns>
        int Delete(int id);
        
        /// <summary>
        /// Actualización de la información de una reservación de vehículo
        /// </summary>
        /// <param name="reservation">Reservación de la vehículo con la información que se desea actualizar</param>
        /// <returns>Reservación de la vehiculo con la información actualizada</returns>
        ReservationVehicle Update(ReservationVehicle reservation);
        
        /// <summary>
        /// Agrega una reservación de vehiculo en la base de datos
        /// </summary>
        /// <param name="reservation">Reservación que se desea agregar</param>
        /// <returns>La reservación agregada con su respectido ID</returns>
        ReservationVehicle AddReservation(ReservationVehicle reservation);
        
        /// <summary>
        /// Obtiene todas las reservaciones que le pertenecen a un usuario
        /// </summary>
        /// <param name="userId">ID del usuario de quien se desea buscar las reservaciones</param>
        /// <returns>Lista de reservaciones de vehículo del usuario</returns>
        List<ReservationVehicle> GetAllByUserId(int userId);
    }
}