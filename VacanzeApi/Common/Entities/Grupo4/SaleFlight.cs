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
}
