using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vacanze_back.Entities.Grupo13
{
    public class Room : Entity
    {
        public int capacity { get; set; }
        public double price { get; set; }
        public bool status { get; set; }

        /*
        public void setCapacidad(int capacidad)
        {
            this.capacidad = capacidad;
        }
        public void setPrecio(float precio)
        {
            this.precio = precio;
        }

        public void setEstatus(bool estatus)
        {
            this.estatus = estatus;
        }

        public int getCapacidad()
        {
            return capacidad;
        }

        public float getPrecio()
        {
            return precio;
        }

        public bool getEstatus()
        {
            return estatus;
        }
        */
        public Room()
        {
        }

        public Room(long id, int capacidad, double precio, bool estatus)
        {
            setId(id);
            this.capacity = capacidad;
            this.price = precio;
            this.status = estatus;
        }
    }
}
