using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo9;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo9
{
    public class BaggageBuilder
    {

        private readonly Baggage _baggage;

        private BaggageBuilder()
        {
            _baggage = new Baggage();
        }

        public static BaggageBuilder Create()
        {
            return new BaggageBuilder();
        }

        public BaggageBuilder WithId(int id)
        {
            _baggage.Id = id;
            return this;
        }

        public BaggageBuilder WithDescription(string description)
        {
            _baggage.Description = description;
            return this;
        }

        public BaggageBuilder WithStatus(string status)
        {
            _baggage.Status = status;
            return this;
        }

        public Baggage Build()
        {
            //TODO: el validator de Baggage
            //BaggageValidator.validate(_baggage);
            return _baggage;
        }
    }
}
