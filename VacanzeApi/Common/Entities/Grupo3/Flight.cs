using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo3
{
    public class Flight : Entity
    {

        public Airplane plane { get; set; }
        public double price { get; set; }
        public string departure { get; set; }
        public string arrival { get; set; }
        public Location loc_departure { get; set;}
        public Location loc_arrival { get; set; }

        /* public Flight(long id):base(id)
        {
        }*/

        public Flight(int id):base(id)
        {
        }

        public Flight():base(0)
        {      
        }

        public Flight(Airplane plane, double price, string departure, string arrival, Location loc_departure, Location loc_arrival):base(0)
        {
            this.plane = plane;
            this.price = price;
            this.departure = departure;
            this.arrival = arrival;
            this.loc_departure = loc_departure;
            this.loc_arrival = loc_arrival;
        }

        public Flight(int id, Airplane plane, double price, string departure, string arrival, Location loc_departure, Location loc_arrival):base(id)
        {
            this.plane = plane;
            this.price = price;
            this.departure = departure;
            this.arrival = arrival;
            this.loc_departure = loc_departure;
            this.loc_arrival = loc_arrival;
        }

    }
}