using System;
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
        public int LocDeparture { get;}
        public int LocArrival { get;}


        public Layover(int id, int cruiserId, string departureDate,string arrivalDate, double price, int locDeparture,
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
        public Layover(int cruiserId,string departureDate,string arrivalDate, double price, int locDeparture,
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
            if (string.IsNullOrEmpty(ArrivalDate))
            {
                throw new InvalidAttributeException("Arrival Date is required");
            }
            if (string.IsNullOrEmpty(DepartureDate))
            {
                throw new InvalidAttributeException("Departure Date is required");
            }
            if (LocDeparture == 0)
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