using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo3;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo2;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo7;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo9;
using vacanze_back.VacanzeApi.Persistence.DAO.Locations;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo6;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo12;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo5;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo4;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo8;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo10;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo14;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo1;

namespace vacanze_back.VacanzeApi.Persistence.DAO
{
    public abstract class DAOFactory
    {
        public enum Type
        {
            Postgres
        }

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
        //     GRUPO 5
        // +++++++++++++++++
        public abstract IBrandDAO GetBrandDAO();
        public abstract IModelDAO GetModelDAO();
        public abstract IVehicleDAO GetVehicleDAO();
        // +++++++++++++++++
        //     GRUPO 6
        // +++++++++++++++++
        public abstract HotelDAO GetHotelDAO();
        
        public abstract LocationDAO GetLocationDAO();
        // +++++++++++++++++
        //     GRUPO 13
        // +++++++++++++++++
        public abstract IReservationRoomDAO GetReservationRoomDAO();

        public abstract IReservationVehicleDAO GetReservationVehicleDAO();
        // +++++++++++++++++
        //     GRUPO 7
        // +++++++++++++++++
        public abstract IRestaurantDAO GetRestaurantDAO();
        
        public abstract IClaimDao GetClaimDao();
        
        public abstract RoleDAO GetRoleDAO();

        public abstract UserDAO GetUserDAO();
        // +++++++++++++++++
        //     GRUPO 8
        // +++++++++++++++++
        public abstract ICruiserDAO GetCruiserDAO();
        //----------------------Grupo3--------------------------------//
        
        public abstract IAirplaneDAO GetAirplane();

        public abstract IFlightDAO GetFlight();
        //------------------------Fin---------------------------------//

        public abstract IBaggageDao GetBaggageDao();

        // +++++++++++++++++
        //     GRUPO 12
        // +++++++++++++++++
        public abstract ReservationFlightDAO GetReservationFlightDAO();

        public abstract ISaleFlightDAO GetSaleFlightDAO();

        public abstract ISaleFlightDAO PostSaleFlightDAO();

        public abstract ICheckinBaggageDAO PostCheckBaggage();
         public abstract Traveldao GetTravelDAO();
         public abstract Commentdao GeCommentDAO();

        // +++++++++++++++++
        //     GRUPO 14
        // +++++++++++++++++
        public abstract IReservationRestaurantDAO GetReservationRestaurantDAO();

        public abstract LoginDAO GetLoginDAO();

    }
}