using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo13
{
    public class Automobile : Entity
    {
        public String Make { get; set; }
        public String Model { get; set; }
        public int Capacity { get; set; }
        public bool IsActive { get; set; }
        public decimal Price { get; set; }
        public String License { get; set; }
        public String Picture { get; set; }

        public Automobile(long id, string make, string model, int capacity, bool isActive, decimal price, string license, string picture) : base(id)
        {
            this.Make = make;
            this.Model = model;
            this.Capacity = capacity;
            this.IsActive = isActive;
            this.Price = price;
            this.License = license;
            this.Picture = picture;
        }
    }
}
