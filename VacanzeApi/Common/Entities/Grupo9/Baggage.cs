namespace vacanze_back.VacanzeApi.Common.Entities.Grupo9
{
    public class Baggage
    {
        public Baggage(int id, string description, string status)
        {
            Id = id;
            Description = description;
            Status = status;
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}