using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;

namespace vacanze_back.VacanzeApi.Persistence.DAO
{
    public class PostgresDAOFactory: DAOFactory
    {
        public override ReservationRoomDAO GetReservationRoomDAO()
        {
            return new PostgresReservationRoomDAO();
        }
    }
}