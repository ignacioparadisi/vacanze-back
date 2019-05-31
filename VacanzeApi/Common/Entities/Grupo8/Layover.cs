using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo8
{
    public class Layover
    {
        public int Id { get; }
        public int CruiserId { get;}
        public string ArrivalDate { get;}
        public string DepartureDate { get;}
        public double Price { get;}
        public int LocDeparture { get;}
        public int LocArrival { get;}


        public Layover(int id, int cruiserId, string arrivalDate, string departureDate, double price, int locDeparture,
            int locArrival)
        {
            Id = id;
            CruiserId = cruiserId;
            ArrivalDate = arrivalDate;
            DepartureDate = departureDate;
            Price = price;
            LocDeparture = locDeparture;
            LocArrival = locArrival;
        }
        public void Validate()
        {
            if (string.IsNullOrEmpty(ArrivalDate))
            {
                throw new NotValidAttributeException("Arrival Date is required");
            }
            if (string.IsNullOrEmpty(DepartureDate))
            {
                throw new NotValidAttributeException("Departure Date is required");
            }
            if (LocDeparture == 0)
            {
                throw  new NotValidAttributeException("Departure location id is required");
            }
            if (LocArrival == 0)
            {
                throw new NotValidAttributeException("Arrival Location id is required");
            }
        }
    }
}