using Newtonsoft.Json;
using vacanze_back.VacanzeApi.Common.Entities; //para poder usar location 
using vacanze_back.VacanzeApi.Persistence.Repository;  //con el tiempo quitar esta directiva cuando se resuelva lo de location 

namespace vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo6{

    public class HotelDTO : DTO {

        public int Id { get; set; }
        public string Name { get; set; }
        public int AmountOfRooms { get; set; }
        public int RoomCapacity { get; set; }
        public bool IsActive { get; set; }
        public string AddressSpecification { get; set; }
        public decimal PricePerRoom { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Picture { get; set; }
        public int Stars { get; set; }
        public Location Location { get; set; }
        public int AvailableRooms { get; set; } = -1;

        public bool ShouldSerializeAvailableRooms()
        {
            return AvailableRooms > -1;
        }
        
        [JsonConstructor]
        public HotelDTO(string name , int amountOfRooms, int roomCapacity ,
        bool isActive, string addressSpecs, decimal pricePerRoom, string website , string phone ,
        string picture, int stars , Location location){
            this.Name = name;
            this.AmountOfRooms = amountOfRooms;
            this.RoomCapacity=roomCapacity;
            this.IsActive= isActive;
            this.AddressSpecification =addressSpecs;
            this.PricePerRoom = pricePerRoom;
            this.Website = website;
            this.Phone = phone ;
            this.Picture = picture ;
            this.Stars = stars;
            this.Location = location;
        }



        public HotelDTO(int id , string name , int amountOfRooms, int roomCapacity ,
        bool isActive, string addressSpecs, decimal pricePerRoom, string website , string phone ,
        string picture, int stars , int locationId){
            this.Id= id;
            this.Name = name;
            this.AmountOfRooms = amountOfRooms;
            this.RoomCapacity=roomCapacity;
            this.IsActive= isActive;
            this.AddressSpecification =addressSpecs;
            this.PricePerRoom = pricePerRoom;
            this.Website = website;
            this.Phone = phone ;
            this.Picture = picture ;
            this.Stars = stars;
            this.Location = LocationRepository.GetLocationById(locationId);
        }

        public HotelDTO(string name , int amountOfRooms, int roomCapacity ,
        bool isActive, string addressSpecs, decimal pricePerRoom, string website , string phone ,
        string picture, int stars , int locationId ){

            this.Name = name;
            this.AmountOfRooms = amountOfRooms;
            this.RoomCapacity=roomCapacity;
            this.IsActive= isActive;
            this.AddressSpecification =addressSpecs;
            this.PricePerRoom = pricePerRoom;
            this.Website = website;
            this.Phone = phone ;
            this.Picture = picture ;
            this.Stars = stars;
            this.Location = LocationRepository.GetLocationById(locationId);
        }
    }
}