namespace vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo8
{
    public class LayoverDTO
    {
        public int Id { get; set; }
        public int CruiserId { get; set; }
        public string DepartureDate { get; set; }
        public string ArrivalDate { get; set; }
        public decimal Price { get; set; }
        public int LocDeparture { get; set; }
        public int LocArrival { get; set; }
    }
}