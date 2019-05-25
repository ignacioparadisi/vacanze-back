using System.Collections.Generic;

namespace vacanze_back.Entities.Grupo3
{
    public class Flight : Entity
    {

        public Airplane plane { get; set; }
        public double price { get; set; }
        public string departure { get; set; }
        public string arrival { get; set; }
        public List<Route> routes { get; set; }


        public Flight(long id):base(id)
        {
            this.routes = new List<Route>();
        }

        public Flight():base(0)
        {
            this.routes = new List<Route>();            
        }

        public Flight(Airplane plane, double price, string departure, string arrival, List<Route> routes):base(0)
        {
            this.plane = plane;
            this.price = price;
            this.departure = departure;
            this.arrival = arrival;
            this.routes = routes;
        }

        public Flight(long id, Airplane plane, double price, string departure, string arrival, List<Route> routes):base(id)
        {
            this.plane = plane;
            this.price = price;
            this.departure = departure;
            this.arrival = arrival;
            this.routes = routes;
        }
    }
}