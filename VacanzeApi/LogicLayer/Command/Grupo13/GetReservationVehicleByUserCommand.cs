using System.Collections.Generic;
using System.Linq;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo5;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo13
{
    public class GetReservationVehicleByUserCommand : CommandResult<List<ReservationVehicle>>
    {

        /// <summary>
        ///  Reservaciones de vehículos del usuario especificado
        /// </summary>
        private List<ReservationVehicle> _reservations;
        /// <summary>
        /// Id del usuario al que le pertencen las reservaciones de los vehículos que se desean buscar
        /// </summary>
        private int userId;

        public GetReservationVehicleByUserCommand(int id)
        {
            userId = id;
        }
        
        /// <summary>
        /// Retorna las reservaciones de vehículos del usuario especificado
        /// </summary>
        /// <returns>Lista de reservaciones de vehículos del usuario especificado</returns>
        public List<ReservationVehicle> GetResult()
        {
            return _reservations;
        }

        /// <summary>
        /// Busca las reservaciones de los vehículos del usuario especificado mediante el DAO de reservacion de vehículos
        /// </summary>
        /// <exception cref="UserDoesntHaveReservationsException">Se retorna cuando el usuario no tiene reservaciones</exception>
        public void Execute()
        {
            var dao = DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetReservationVehicleDAO();
            _reservations = dao.GetAllByUserId(userId);
            if (!_reservations.Any())
            {
                throw new UserDoesntHaveReservationsException();
            }

        }
    }
}