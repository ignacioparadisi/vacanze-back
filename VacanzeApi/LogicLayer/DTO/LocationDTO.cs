using Newtonsoft.Json;

namespace vacanze_back.VacanzeApi.LogicLayer.DTO
{
    public class LocationDTO : DTO
    {
        [JsonConstructor]
        public LocationDTO(int id, string country, string city)
        {
            Id = id;
            Country = country;
            City = city;
        }
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}