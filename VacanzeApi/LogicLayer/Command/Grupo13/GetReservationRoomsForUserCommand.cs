using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo13
{
    public class GetReservationRoomsForUserCommand: CommandResult<List<ReservationRoom>>
    {
        /// <summary>
        /// ID del usuario al que le pertencen las reservaciones de las habitaciones que se quieren buscar
        /// </summary>
        private int _id;
        /// <summary>
        /// Reservaciones de las habitaciones del usuario especificado
        /// </summary>
        private List<ReservationRoom> _result;

        public GetReservationRoomsForUserCommand(int id)
        {
            _id = id;
        }
        
        /// <summary>
        /// Busca las reservaciones de habitaciones de un usuario mediando el DAO de reservaciones de habitaciones
        /// </summary>
        public void Execute()
        {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            IReservationRoomDAO reservationRoomDao = factory.GetReservationRoomDAO();
            _result = reservationRoomDao.GetAllByUserId(_id);
        }

        /// <summary>
        ///  Retorna las reservacioens de habitaciones del usuario especificado
        /// </summary>
        /// <returns>Reservaciones de habitaciones del usuario especificado</returns>
        public List<ReservationRoom> GetResult()
        {
            return _result;
        }
    }
}