using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo9;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo9
{
    public class ClaimBuilder
    {
        private readonly Claim _claim;

        private ClaimBuilder()
        {
            _claim = new Claim();
        }

        public static ClaimBuilder Create()
        {
            return new ClaimBuilder();
        }


        public ClaimBuilder WithId(int id)
        {
            _claim.Id = id;
            return this;
        }

        public ClaimBuilder WithTitle(string title)
        {
            _claim.Title = title;
            return this;
        }

        public ClaimBuilder WithDescription(string description)
        {
            _claim.Description = description;
            return this;
        }

        public ClaimBuilder WithStatus(string status)
        {
            _claim.Status = status;
            return this;
        }

        public ClaimBuilder WithBagagge(int baggageId)
        {
            _claim.BaggageId = baggageId;
            return this;
        }

        public Claim Build()
        {
            ClaimValidator.Validate(_claim);
            return _claim;
        }
    }
}