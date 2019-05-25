namespace vacanze_back.Common.Entities.Grupo3
{
    public class Route: Entity
    {
        
       public int flight { get; set; }
        public int locDeparture { get; set; }
        public int locArrival { get; set; } 
        public string arrivalDate { get; set; }
        public string departureDate { get; set; }
        public double price { get; set; }

        public Route():base(0){

        }

        public Route(long id):base(id)
        {
        }

        public Route(int flight, int locDeparture, int locArrival, string arrivalDate, string departureDate, double price):base(0) 
        {
            this.flight = flight;
            this.locDeparture = locDeparture;
            this.locArrival = locArrival;
            this.arrivalDate = arrivalDate;
            this.departureDate = departureDate;
            this.price = price;
               
        }
    }
}