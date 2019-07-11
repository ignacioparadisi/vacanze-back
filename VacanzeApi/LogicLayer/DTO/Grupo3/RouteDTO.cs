namespace vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo3
{
    public class RouteDTO: DTO
    {
        
       public int flight { get; set; }
        public int locDeparture { get; set; }
        public int locArrival { get; set; } 
        public string arrivalDate { get; set; }
        public string departureDate { get; set; }
        public double price { get; set; }

        public RouteDTO(){

        }

        public RouteDTO(int id)
        {
        }

        public RouteDTO(int flight, int locDeparture, int locArrival, string arrivalDate, string departureDate, double price) 
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