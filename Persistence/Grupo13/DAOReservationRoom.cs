using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.Entities.Grupo13;
using vacanze_back.Entities;

namespace vacanze_back.Persistence.Grupo13
{
    public class DAOReservationRoom:DAO
    {

        private ReservationRoom reservationRoom;

        public void create(Entity e)
        {
            reservationRoom = (ReservationRoom)e;
        }


    }
}
