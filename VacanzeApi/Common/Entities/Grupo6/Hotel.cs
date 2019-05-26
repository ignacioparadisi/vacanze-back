namespace vacanze_back.VacanzeApi.Common.Entities.Grupo6
{
    public class Hotel : Entity
    {
        public string Name { get; }
        public int AmountOfRooms { get; }
        public bool IsActive { get; }
        public string Phone { get; }
        public string Website { get; }
        // TODO: Cuando se cree la clase Lugar, implementar esta parte
        // private Lugar _direccion;

        public Hotel(long id, string name, int amountOfRooms, bool isActive,
            string phone, string website) : base(id)
        {
            Name = name;
            AmountOfRooms = amountOfRooms;
            IsActive = isActive;
            Phone = phone;
            Website = website;
        }

        public Hotel(string name, int amountOfRooms, bool isActive,
            string phone, string website) : base(0)
        {
            Name = name;
            AmountOfRooms = amountOfRooms;
            IsActive = isActive;
            Phone = phone;
            Website = website;
        }
    }
}