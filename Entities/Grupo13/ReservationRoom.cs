using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vacanze_back.Entities.Grupo13
{
    public class ReservationRoom : Entity
    {
        private bool status { get; set; }
        private DateTime CheckIn { get; set; }
        private DateTime CheckOut { get; set; }

        /*
        public void setEstatus(bool estatus)
        {
            this.estatus = estatus;
        }

        public void setFechaCheckIn(DateTime fecha)
        {
            this.fechaCheckIn = fecha;
        }
        public void setFechaCheckOut(DateTime fecha)
        {
            this.fechaCheckOut = fecha;
        }

        public bool getEstatus()
        {
            return this.estatus;
        }

        public DateTime getFechaCheckIn()
        {
            return this.fechaCheckIn;
        }

        public DateTime getFechaCheckOut()
        {
            return this.fechaCheckOut;
        }

    */
        public ReservationRoom()
        {
        }

        public ReservationRoom(long id, bool status, DateTime CheckIn, DateTime CheckOut)
        {
            setId(id);
            this.status = status;
            this.CheckIn = CheckIn;
            this.CheckOut = CheckOut;
        }
    }
}
