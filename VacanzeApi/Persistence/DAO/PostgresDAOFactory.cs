
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo3;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo9;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo2;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo7;
using vacanze_back.VacanzeApi.Persistence.DAO.Locations;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo6;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo4;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo12;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo5;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo8;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo10;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo14;
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
           public override Traveldao GetTravelDAO()
        {
            return new Traveldao();
        }

        public override Commentdao  GeCommentDAO ()
        {
            return new Commentdao();
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

        public override ICruiserDAO GetCruiserDAO()
        {
            return new PostgresCruiserDAO();
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

        public override IReservationRestaurantDAO GetReservationRestaurantDAO()
        {
            return new PostgresReservationRestaurantDAO();
        }
        
        public override LoginDAO GetLoginDAO(){
            return new PostgresLoginDAO();

        }
    }
}