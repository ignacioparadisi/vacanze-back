namespace vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo8
{
    public class LayoverDTO
    {
        public int Id { get; set; }
        public int CruiserId { get;}
        public string DepartureDate { get;}
        public string ArrivalDate { get;}
        public decimal Price { get;}
        public int LocDeparture { get;}
        public int LocArrival { get;}
    }
}