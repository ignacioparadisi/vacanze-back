namespace vacanze_back.Entities
{
    public abstract class Entity
    {
        private long _id;

        public void setId(long id)
        {
            this._id = id;
        }

        public long getIt()
        {
            return this._id;
        }
    }
}