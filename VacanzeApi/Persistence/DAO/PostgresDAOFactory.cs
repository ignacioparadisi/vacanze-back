using DefaultNamespace;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo3;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo9;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo2;
using vacanze_back.VacanzeApi.Persistence.DAO.Locations;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo6;

namespace vacanze_back.VacanzeApi.Persistence.DAO
{
    public class PostgresDAOFactory : DAOFactory
    {
        public override ReservationRoomDAO GetReservationRoomDAO()
        {
            return new PostgresReservationRoomDAO();
        }

        public override IRestaurantDAO GetRestaurantDAO()
        {
            return new PostgresRestaurantDAO();
        }

        public override RoleDAO GetRoleDAO()
        {
            return new PostgresRoleDAO();
        }

        public override UserDAO GetUserDAO()
        {
            return new PostgresUserDAO();
        }

        public override IBaggageDao GetBaggageDao()
        {
            return  new PostgresBaggageDao();
        }

        public override IClaimDao GetClaimDao()
        {
            return new PostgresClaimDao();
        }

        public override HotelDAO GetHotelDAO()
        {
            return new PostgresHotelDAO();
        }

         public override IAirplaneDAO GetAirplane()
        {
            return new AirplanesRepositoryDAO();
        }

         public override IFlightDAO GetFlight()
        {
            return new FlightRepositoryDAO();
        }

        
        public override LocationDAO GetLocationDAO()
        {
            return new PostgresLocationDAO();
        }
    }
}