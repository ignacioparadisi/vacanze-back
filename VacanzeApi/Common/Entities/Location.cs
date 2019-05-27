using Newtonsoft.Json;

namespace vacanze_back.VacanzeApi.Common.Entities
{
    public class Location : Entity
    {
    
        [JsonConstructor]
        public Location(long id, string city, string country) : base(id)
        {
            City = city;
            Country = country;
        }

        public Location(string city, string country) : base(0)
        {
            City = city;
            Country = country;
        }

        public string Country { get; }
        public string City { get; }
    }
}