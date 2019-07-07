using System;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo4
{
    public class SaleFlight
    {
        public int id { get; set; }
        public int ileft { get; set; }
        public string descrip { get; set; }
        public DateTime dateArrival { get; set; }
        public decimal price { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }

    }
    public class PostSaleFlight
    {
        public string seat { get; set; }
        public int numps { get; set; }
        public int user { get; set; }
        public int pay { get; set; }
        public int fli { get; set; }
    }
}
