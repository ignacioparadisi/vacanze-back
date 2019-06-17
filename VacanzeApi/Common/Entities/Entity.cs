namespace vacanze_back.VacanzeApi.Common.Entities
{
    public abstract class Entity
    {
        protected Entity(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}