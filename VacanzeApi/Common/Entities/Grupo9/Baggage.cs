namespace vacanze_back.VacanzeApi.Common.Entities.Grupo9
{
    public class Baggage : Entity
    {
        public Baggage(int id, string description, string status) : base(id)
        {
            Description = description;
            Status = status;
        }

        public Baggage(): base(0)
        {
        }

        public string Description { get; set; }
        public string Status { get; set; }
    }
}