using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;

namespace vacanze_back.VacanzeApi.Common.Entities
{
    public class EntityFactory
    {
        public static Brand createBrand(string brandName)
        {
            return new Brand(brandName);
        }

        public static Restaurant CreateRestaurant(int id, string name, int capacity, bool isActive, decimal qualify,
            string specialty,
            decimal price, string businessName, string picture,
            string description, string phone, int location, string address)
        {
            return new Restaurant(id, name, capacity, isActive, qualify, specialty, price, businessName, picture,
                description, phone, location, address);
        }

        public static Hotel createHotel(int id, string name, int amountOfRooms, int roomCapacity,
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

        #region Grupo 13
        public static ReservationAutomobile CreateReservationAutomobile(int id, DateTime checkIn, DateTime checkOut) =>
            new ReservationAutomobile(id, checkIn, checkOut);


        public static ReservationRoom CreateReservationRoom(int id, DateTime chekckIn, DateTime checkOut) =>
            new ReservationRoom(id, chekckIn, checkOut);
        
        public static ReservationRoom CreateReservationRoom() =>
            new ReservationRoom();
        #endregion
    }
}