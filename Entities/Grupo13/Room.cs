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


        public void setCapacity(int capacity)
        {
            this.capacity = capacity;
        }
        public void setPrice(double price)
        {
            this.price = price;
        }

        public void setStatus(bool status)
        {
            this.status = status;
        }

        public int getCapacity()
        {
            return capacity;
        }

        public double getPrice()
        {
            return price;
        }

        public bool getStatus()
        {
            return status;
        }

        public Room()
        {
        }

        public Room(long id, int capacity, double price, bool status)
        {
            setId(id);
            this.capacity = capacity;
            this.price = price;
            this.status = status;
        }
    }
}
