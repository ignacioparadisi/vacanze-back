using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo6;

namespace vacanze_back.VacanzeApi.Persistence.DAO
{
    public class PostgresDAOFactory: DAOFactory
    {
        public override ReservationRoomDAO GetReservationRoomDAO()
        {
            return new PostgresReservationRoomDAO();
        }
        
        public override HotelDAO GetHotelDAO()
        {
            return new PostgresHotelDAO();
        }
    }
}