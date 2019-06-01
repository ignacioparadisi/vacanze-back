using System;
using Newtonsoft.Json;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo8
{
    public class Layover
    {
        public int Id { get; }
        public int CruiserId { get;}
        public string DepartureDate { get;}
        public string ArrivalDate { get;}
        public double Price { get;}
        public string LocDeparture { get;}
        public string LocArrival { get;}

        [JsonConstructor]
        public Layover(int id, int cruiserId, string departureDate,string arrivalDate, double price, string locDeparture,
            string locArrival)
        {
            Id = id;
            CruiserId = cruiserId;
            DepartureDate = departureDate;
            ArrivalDate = arrivalDate;
            Price = price;
            LocDeparture = locDeparture;
            LocArrival = locArrival;
        }
        public Layover(int cruiserId,string departureDate,string arrivalDate, double price, string locDeparture,
            string locArrival)
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
            if (string.IsNullOrEmpty(ArrivalDate))
            {
                throw new InvalidAttributeException("Arrival Date is required");
            }
            if (string.IsNullOrEmpty(DepartureDate))
            {
                throw new InvalidAttributeException("Departure Date is required");
            }
            if (LocDeparture == null)
            {
                throw  new InvalidAttributeException("Departure location is required");
            }
            if (LocArrival == null)
            {
                throw new InvalidAttributeException("Arrival Location is required");
            }
        }
    }
}