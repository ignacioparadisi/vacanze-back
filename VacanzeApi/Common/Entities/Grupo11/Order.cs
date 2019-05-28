using Newtonsoft.Json;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo11
{
    public class Order : Entity
    {
        // TODO: Cuando se cree la clase Lugar, implementar esta parte
        // private Lugar _direccion;

        [JsonConstructor]
        public Order(long id, string descrip, string image,
            string brand, double price, double priceTotal) : base(id)
        {
            Descrip = descrip;
            Image = image;
            Brand = brand;
            Price = price;
            PriceTotal = priceTotal;
        }

        public Order(string descrip, string image,
            string brand, double price, double priceTotal) : base(0)
        {
          
        }

        public long id { get; }
        public string Descrip { get; }
        public string Image { get; }
        public string Brand { get; }
        public double Price { get; }
        public double PriceTotal { get; }
    }
}