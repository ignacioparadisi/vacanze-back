namespace vacanze_back.VacanzeApi.Common.Entities.Grupo9
{
    public class Claim : Entity
    {
        public Claim() : base(0)
        {
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } = "ABIERTO";
        public int BaggageId { get; set; }
    }
}