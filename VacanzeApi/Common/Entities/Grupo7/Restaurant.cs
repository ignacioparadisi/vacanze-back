using Newtonsoft.Json;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo7
{
    public class Restaurant : Entity
    {
        // TODO: Cuando se cree la clase Lugar, implementar esta parte
        // private Lugar _direccion;

        [JsonConstructor]
        public Restaurant(long id, string name, int capacity,bool isActive, string specialty, 
                          long price, string businessName, string picture, 
                          string description, string phone, int location, string address) : base(id)
        {
            Name = name;
            Capacity = capacity;
            IsActive = isActive;
            Specialty = specialty;
            Price = price;
            BusinessName = businessName;
            Picture = picture;
            Description = description;
            Phone = phone;
            Location = location;
            Address = address;
        }

        
        public Restaurant(string name, int capacity,bool isActive, string specialty, 
                          long price, string businessName, string picture, 
                          string description, string phone, int location, string address) : base(0)
        {
            Name = name;
            Capacity = capacity;
            IsActive = isActive;
            Specialty = specialty;
            Price = price;
            BusinessName = businessName;
            Picture = picture;
            Description = description;
            Phone = phone;
            Location = location;
            Address = address;
        }

        public string Name  { get; }
        public int Capacity  { get; }
        public bool IsActive { get; }
        public string Specialty { get; }
        public long Price { get; }
        public string BusinessName { get; }
        public string Picture { get; }
        public string Description { get; }
        public string Phone { get; }
        public int Location { get; }
        public string Address { get; }

    }
}