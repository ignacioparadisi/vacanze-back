using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;
using vacanze_back.VacanzeApi.Common.Entities.Grupo12;

namespace vacanze_back.VacanzeApi.Common.Entities{

    public class EntityFactory{

        public static Brand createBrand(string brandName){
            return new Brand(brandName);
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

        public static FlightRes CreateFlightRes(int id,int price,string timestamp,string seatNum,
        string name_cityI,string name_countryI, string namecityV,string namecountryV,int numPas, int id_user, int id_flight){
            return new FlightRes( id, price, timestamp, seatNum, name_cityI, name_countryI,
            namecityV, namecountryV, numPas, id_user, id_flight);
        }
    }
}