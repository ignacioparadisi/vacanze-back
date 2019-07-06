using System.Collections.Generic;
using  vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo5;
using  vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo6;
using  vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;

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

        public static UserDTO CreateUserDTO(int Id, long DocumentId, string Email, string Lastname, string Name, string Password, List<Role> Roles)
        {
            return new UserDTO(Id, DocumentId, Email, Lastname, Name, Password, Roles);
        }

        public static RoleDTO CreateRoleDTO(int id, string name)
        {
            return new RoleDTO(id, name);
}
    }

}