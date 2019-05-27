namespace vacanze_back.VacanzeApi.Common.Entities
{
    public abstract class Entity
    {
        protected Entity(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}