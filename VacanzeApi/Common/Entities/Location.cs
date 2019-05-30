namespace vacanze_back.VacanzeApi.Common.Entities
{
    public class Location
    {
        public Location()
        {
        }

        public Location(int id, string country, string city)
        {
            Id = id;
            Country = country;
            City = city;
        }

        public Location(string country, string city)
        {
            Id = 0;
            Country = country;
            City = city;
        }

        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}