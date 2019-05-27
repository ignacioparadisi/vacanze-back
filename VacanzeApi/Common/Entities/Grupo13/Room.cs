using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.Entities;

namespace vacanze_back.Entities.Grupo13
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
