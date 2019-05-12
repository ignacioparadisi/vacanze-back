namespace vacanze_back.Entities
{
    public abstract class Entity
    {
        private long id;

        public void setId(long id)
        {
            this.id = id;
        }

        public long getIt()
        {
            return id;
        }
    }
}