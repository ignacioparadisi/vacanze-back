using  vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo5;
using  vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo6;
using  vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo12;

namespace vacanze_back.VacanzeApi.LogicLayer.DTO{

    public class DTOFactory{

        public static BrandDTO CreateBrandDTO(string brandName){
            return new BrandDTO(brandName);
        }
    

        // +++++++++++++++++
        //     GRUPO 6
        // +++++++++++++++++
        public static HotelDTO CreateHotelDTO(int id , string name , int amountOfRooms, int roomCapacity ,
        bool isActive, string addressSpecs, decimal pricePerRoom, string website , string phone ,
        string picture, int stars , int locationId)
        {
            return new HotelDTO(id, name, amountOfRooms, roomCapacity , isActive, addressSpecs, pricePerRoom,
                                website, phone, picture, stars, locationId );
        }

        public static LocationDTO CreateLocationDTO(int id, string country, string city){
            return new LocationDTO(id, country, city);
        }

        public static FlightResDTO CreateFlightResDTO(int id,int price,string timestamp,string seatNum,
         string name_cityI,string name_countryI, string namecityV,string namecountryV,int numPas, int id_user){
            return new FlightResDTO(  id, price, timestamp, seatNum, name_cityI, name_countryI, namecityV,
            namecountryV, numPas, id_user);
        }
    }

}