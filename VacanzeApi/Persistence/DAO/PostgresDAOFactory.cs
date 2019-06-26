using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo2;

namespace vacanze_back.VacanzeApi.Persistence.DAO
{
    public class PostgresDAOFactory: DAOFactory
    {
        public override ReservationRoomDAO GetReservationRoomDAO()
        {
            return new PostgresReservationRoomDAO();
        }

        public override RoleDAO GetRoleDAO()
        {
            throw new System.NotImplementedException(); //FALTA CREAR LA EXCEPTION.
        }
    }
}