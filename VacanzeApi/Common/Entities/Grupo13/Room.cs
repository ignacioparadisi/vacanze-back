namespace vacanze_back.VacanzeApi.Common.Entities.Grupo13
{
    public class Room : Entity
    {
        public int capacity { get; set; }
        public double price { get; set; }
        public bool status { get; set; }
        // public Hotel hotel {get; set;}

        public Room(long id, double price,int capacity, bool status) : base(id)
        {
            this.capacity = capacity;
            this.price = price;
            this.status = status;
        }

    }
}
