using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo9;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo2;

namespace vacanze_back.VacanzeApi.Persistence.DAO
{
    public class PostgresDAOFactory : DAOFactory
    {
        public override ReservationRoomDAO GetReservationRoomDAO()
        {
            return new PostgresReservationRoomDAO();
        }

        public override RoleDAO GetRoleDAO()
        {
            return new PostgresRoleDAO(); 
        }

        public override UserDAO GetUserDAO()
        {
            return new PostgresUserDAO();
        }

        public override IClaimDao GetClaimDao()
        {
            return new PostgresClaimDao();
        }
    }
}