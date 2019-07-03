namespace vacanze_back.VacanzeApi.Common.Entities
{
    public class Location : Entity
    {
        public Location() : base(0)
        {
        }

        public Location(int id, string country, string city) : base(id)
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