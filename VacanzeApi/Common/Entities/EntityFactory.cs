using System;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo1;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;
using vacanze_back.VacanzeApi.Common.Entities.Grupo12;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities.Grupo14;


namespace vacanze_back.VacanzeApi.Common.Entities{

    public class EntityFactory{

        public static Vehicle CreateVehicle(int Id, int _vehicleModelId, int _vehicleLocationId, 
            string _license, double _price, bool _status)
        {
            return new Vehicle(Id, _vehicleModelId, _vehicleLocationId, _license, _price, _status);
        }

        public static Brand CreateBrand(int id, string brandName){
            return new Brand(id, brandName);
        }

        public static Model CreateModel(int id, int brandId, string modelName, int capacity, 
            string picture){
                return new Model(id, brandId, modelName, capacity, picture);
        }

        public static Restaurant CreateRestaurant(int id, string name, int capacity, bool isActive, decimal qualify, string specialty,
            decimal price, string businessName, string picture,
            string description, string phone, int location, string address)
        {
            return new Restaurant(id, name, capacity, isActive, qualify, specialty, price, businessName, picture, description, phone, location, address);
        }

        public static Hotel CreateHotel(int id, string name, int amountOfRooms, int roomCapacity,
            bool isActive, string addressSpecs, decimal pricePerRoom, string website, string phone,
            string picture, int stars, int locationId)
        {
            return HotelBuilder.Create()
                .IdentifiedBy(id)
                .WithName(name)
                .WithAmountOfRooms(amountOfRooms)
                .WithCapacityPerRoom(roomCapacity)
                .WithPricePerRoom(pricePerRoom)
                .WithPhone(phone)
                .WithWebsite(website)
                .WithStars(stars)
                .WithPictureUrl(picture)
                .LocatedAt(locationId)
                .WithStatus(isActive)
                .WithAddressDescription(addressSpecs)
                .BuildSinVaidar();
        }

        public static Location CreateLocation(int id, string country, string city)
        {
            return new Location(id, country, city);
        }


        public static User CreateUser(int id, long documentId, string name, string lastname, string email,
            string password, List<Role> roles)
        {
            return new User(id, documentId, name, lastname, email, password, roles);
        }

        public static Role CreateRole(int id, string name)
        {
            return new Role(id, name);
        }

        public static FlightRes CreateFlightRes(int id,int price,string timestamp,string seatNum,
        string name_cityI,string name_countryI, string namecityV,string namecountryV,int numPas, int id_user, int id_flight){
            return new FlightRes( id, price, timestamp, seatNum, name_cityI, name_countryI,
            namecityV, namecountryV, numPas, id_user, id_flight);

        }

        #region Grupo 13
        public static ReservationAutomobile CreateReservationAutomobile(int id, DateTime checkIn, DateTime checkOut) =>
            new ReservationAutomobile(id, checkIn, checkOut);

        public static ReservationAutomobile CreateReservationAutomobile(int id, DateTime checkin, DateTime checkout,
            int userId, Auto automobile) =>
            new ReservationAutomobile(id, checkin, checkout, automobile, userId);
        
        public static ReservationRoom CreateReservationRoom(int id, DateTime chekckIn, DateTime checkOut,
            int hotelId, int userId) =>
            new ReservationRoom(id, chekckIn, checkOut, hotelId, userId);
        
        public static ReservationRoom CreateReservationRoom() =>
            new ReservationRoom();
        #endregion

        /*Grupo14*/
        public static Restaurant_res CreateResRestaurant(int id, string locationName, string pais, string restName,
         string address, string fecha_reservacion, int cant_persona)
        {
            return new Restaurant_res(id,locationName,pais,restName,
            address,fecha_reservacion,cant_persona);
        }          
        
        //Grupo 1
        public static Login createLogin(int id, List<Role> roles, string email, string password){
            return new Login(id, roles, email, password);
        }

        public static Login createLogin(int id, string email, string password){
            return new Login(id,email, password);
        }

        public static Login createLogin(string password){
            return new Login(password);
        }
    }
}