namespace vacanze_back.VacanzeApi.Common.Entities.Grupo6
{
    public class Hotel : Entity
    {

        public Hotel() : base(0)
        {
        }

        public Hotel(int id) : base(id)
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int AmountOfRooms { get; set; }
        public int RoomCapacity { get; set; }
        public bool IsActive { get; set; }
        public string AddressSpecification { get; set; }
        public decimal PricePerRoom { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Picture { get; set; }
        public int Stars { get; set; }
        public Location Location { get; set; }
        public int AvailableRooms { get; set; } = -1;

        public bool ShouldSerializeAvailableRooms()
        {
            return AvailableRooms > -1;
        }
    }
}