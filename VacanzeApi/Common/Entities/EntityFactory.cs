using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;

namespace vacanze_back.VacanzeApi.Common.Entities{

    public class EntityFactory{

        public static Vehicle CreateVehicle(int Id, int _vehicleModelId, int _vehicleLocationId, 
            string _license, double _price, bool _status)
        {
            return new Vehicle(Id, _vehicleModelId, _vehicleLocationId, _license, _price, _status);
        }
        
        public static Brand createBrand(int id, string brandName)
        {
            return new Brand(id, brandName);
        }

        public static Model CreateModel(int id, int brandId, string modelName, int capacity,
            string picture)
        {
            return new Model(id, brandId, modelName, capacity, picture);
        }

        public static Restaurant CreateRestaurant(int id, string name, int capacity,bool isActive,decimal qualify, string specialty, 
            decimal price, string businessName, string picture, 
            string description, string phone, int location, string address)
        {
            return new Restaurant(id,name,capacity,isActive,qualify,specialty,price,businessName,picture,description,phone,location,address);
        }

        public static Hotel createHotel(int id , string name , int amountOfRooms, int roomCapacity ,
            bool isActive, string addressSpecs, decimal pricePerRoom, string website , string phone ,
            string picture, int stars , int locationId )
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

        public static Location CreateLocation(int id, string country, string city){
            return new Location(id, country, city);
        }
        
        #region Grupo 13
        public static ReservationAutomobile CreateReservationAutomobile(int id, DateTime checkIn, DateTime checkOut) =>
            new ReservationAutomobile(id, checkIn, checkOut);

        public static ReservationAutomobile CreateReservationAutomobile(int id, DateTime checkin, DateTime checkout,
            int userId, Auto automobile) =>
            new ReservationAutomobile(id, checkin, checkout, automobile, userId);

        public static ReservationRoom CreateReservationRoom(int id, DateTime chekckIn, DateTime checkOut) =>
            new ReservationRoom(id, chekckIn, checkOut);
        public static ReservationRoom CreateReservationRoom(int id, DateTime chekckIn, DateTime checkOut,
            Hotel hotel, User user) =>
            new ReservationRoom(id, chekckIn, checkOut, hotel, user);
        
        public static ReservationRoom CreateReservationRoom() =>
            new ReservationRoom();
        #endregion
    }
}