using System;
using Newtonsoft.Json;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo8
{
    public class Layover
    {
        public int Id { get; set; }
        public int CruiserId { get;}
        public DateTime DepartureDate { get;}
        public DateTime ArrivalDate { get;}
        public decimal Price { get;}
        public int LocDeparture { get;}
        public int LocArrival { get;}

        [JsonConstructor]
        public Layover(int id, int cruiserId, DateTime departureDate,DateTime arrivalDate, decimal price, int locDeparture,
            int locArrival)
        {
            Id = id;
            CruiserId = cruiserId;
            DepartureDate = departureDate;
            ArrivalDate = arrivalDate;
            Price = price;
            LocDeparture = locDeparture;
            LocArrival = locArrival;
        }
        public Layover(int cruiserId,DateTime departureDate,DateTime arrivalDate, decimal price, int locDeparture,
            int locArrival)
        {   
            CruiserId = cruiserId;
            DepartureDate = departureDate;
            ArrivalDate = arrivalDate;
            Price = price;
            LocDeparture = locDeparture;
            LocArrival = locArrival;
        }
        public void Validate()
        {
            if (CruiserId <= 0)
            {
                throw  new InvalidAttributeException("Cruiser id is required");
            }
            if (LocDeparture <= 0)
            {
                throw  new InvalidAttributeException("Departure location id is required");
            }
            if (LocArrival == 0)
            {
                throw new InvalidAttributeException("Arrival Location id is required");
            }
        }
    }
}