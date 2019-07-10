using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo9;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo2;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo7;
using vacanze_back.VacanzeApi.Persistence.DAO.Locations;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo6;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo4;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo12;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo5;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo1;

namespace vacanze_back.VacanzeApi.Persistence.DAO
{
    public class PostgresDAOFactory : DAOFactory
    {
        public override IBrandDAO GetBrandDAO(){
            return new PostgresBrandDAO();
        }

        public override IModelDAO GetModelDAO(){
            return new PostgresModelDAO();
        }

        public override IVehicleDAO GetVehicleDAO(){
            return new PostgresVehicleDAO();
        }
        
        public override IReservationRoomDAO GetReservationRoomDAO()
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

        public override LocationDAO GetLocationDAO()
        {
            return new PostgresLocationDAO();
        }

        public override IReservationVehicleDAO GetReservationVehicleDAO()
        {
            return new PostgresReservationVehicleDAO();
        }

        public override ReservationFlightDAO GetReservationFlightDAO()
        {
            return new PostgresReservationFlightDAO();
        }
        public override ISaleFlightDAO GetSaleFlightDAO()
        {
            return new PostgresSaleFlightDAO();
        }
        public override ISaleFlightDAO PostSaleFlightDAO()
        {
            return new PostgresSaleFlightDAO();
        }
        public override ICheckinBaggageDAO PostCheckBaggage()
        {
            return new PostgresCheckBaggagetDAO();
        }
        
        public override LoginDAO GetLoginDAO(){
            return new PostgresLoginDAO();
        }
    }
}