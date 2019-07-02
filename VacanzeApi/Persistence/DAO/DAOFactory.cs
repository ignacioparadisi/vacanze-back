using DefaultNamespace;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo2;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo9;

namespace vacanze_back.VacanzeApi.Persistence.DAO
{
    public abstract class DAOFactory
    {
        public enum Type
        {
            Postgres
        };

        public static DAOFactory GetFactory(Type type)
        {
            switch (type)
            {
                case Type.Postgres:
                    return new PostgresDAOFactory();
                default:
                    throw new NotValidFactoryTypeException("El tipo de fábrica de DAO no es válido");
            }
        }

        // +++++++++++++++++
        //     GRUPO 13
        // +++++++++++++++++
        public abstract ReservationRoomDAO GetReservationRoomDAO();
        // +++++++++++++++++
        //     GRUPO 7
        // +++++++++++++++++
        public abstract IRestaurantDAO GetRestaurantDAO();
        
        public abstract IClaimDao GetClaimDao();
        
        public abstract RoleDAO GetRoleDAO();

        public abstract UserDAO GetUserDAO();
    }
}