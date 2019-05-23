namespace vacanze_back.Entities.Grupo3
{
    public class Route: Entity
    {
        public int flight { get; set; }
        public int locDeparture { get; set; }
        public int locArrival { get; set; } 
        public string arrivalDate { get; set; }
        public string departureDate { get; set; }
        public double price { get; set; }
    }
}