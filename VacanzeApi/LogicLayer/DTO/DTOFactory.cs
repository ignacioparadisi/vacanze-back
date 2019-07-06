using vacanze_back.VacanzeApi.Common.Entities;
using  vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo3;
using  vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo5;
using  vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo6;

namespace vacanze_back.VacanzeApi.LogicLayer.DTO{

    public class DTOFactory{

        //-----------------------------------------Grupo 3---------------------------------------------------//  
        public static FlightDTO CreateFlightDTO(AirplaneDTO plane, double price, string departure, string arrival,
         Location loc_departure, Location loc_arrival){
            return new FlightDTO(plane,price,departure,arrival,loc_departure,loc_arrival);
        }
        //-----------------------------------------Fin-------------------------------------------------------//

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
    }

}