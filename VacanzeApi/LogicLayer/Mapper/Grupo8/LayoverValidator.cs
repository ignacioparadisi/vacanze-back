using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo8
{
    public class LayoverValidator
    {
        public static void Validate(Layover layover)
        {
            if (string.IsNullOrEmpty(layover.ArrivalDate))
            {
                throw new InvalidAttributeException("Arrival Date is required");
            }
            if (string.IsNullOrEmpty(layover.DepartureDate))
            {
                throw new InvalidAttributeException("Departure Date is required");
            }
            if (layover.LocDeparture == 0)
            {
                throw  new InvalidAttributeException("Departure location id is required");
            }
            if (layover.LocArrival == 0)
            {
                throw new InvalidAttributeException("Arrival Location id is required");
            }
        }
    }
}