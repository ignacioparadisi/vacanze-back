using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo1;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;

namespace vacanze_back.VacanzeApi.Common.Entities{

    public class EntityFactory{

        public static Brand createBrand(string brandName){
            return new Brand(brandName);
        }

        public static Restaurant CreateRestaurant(int id, string name, int capacity, bool isActive, decimal qualify, string specialty,
            decimal price, string businessName, string picture,
            string description, string phone, int location, string address)
        {
            return new Restaurant(id, name, capacity, isActive, qualify, specialty, price, businessName, picture, description, phone, location, address);
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

        public static User CreateUser(int id, long documentId, string name, string lastname, string email,
            string password, List<Role> roles)
        {
            return new User(id, documentId, name, lastname, email, password, roles);
        }

        public static Role CreateRol(int id, string name)
        {
            return new Role(id, name);
        }

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