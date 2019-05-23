using System.Collections.Generic;

namespace vacanze_back.Entities.Grupo3
{
    public class Flight : Entity
    {

        public int plane { get; set; }
        public double price { get; set; }
        public string departure { get; set; }
        public string arrival { get; set; }
        public List<Route> routes { get; set; }


        public Flight()
        {

        }

        public Flight(double price, string departure, string arrival, List<Route> routes)
        {
            this.price = price;
            this.departure = departure;
            this.arrival = arrival;
            this.routes = routes;
        }
    }
}