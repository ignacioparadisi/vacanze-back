namespace vacanze_back.Entities
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