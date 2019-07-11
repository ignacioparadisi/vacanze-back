using System;
using System.Collections.Generic;
namespace vacanze_back.VacanzeApi.Common.Entities.Grupo10
{
    public class Restautantetravel
    {
        /*clase axuiliar que permite obtener las reservaciones de restaurantes */
           private List<Restautantetravel> _restaurantReservations = new List<Restautantetravel>();
        private int _id;
        public int id{ get{ return _id; } set{ _id = value; } }
         private int _numpers;
        public int numpers{ get{ return _numpers; } set{ _numpers = value; } }
         private int _forkeyuser;
        public int forkeyuser{ get{ return _forkeyuser; } set{ _forkeyuser = value; } }
         private int _forkeyrest;
        public int forkyerest{ get{ return _forkeyrest; } set{ _forkeyrest = value; } }
 private DateTime _init;
        public DateTime fecha{ get{ return _init; } set{ _init = value; } }

           public Restautantetravel(int id , DateTime fecha , int numpers , int forkeyuser , int forkyerest){
               this.id=id;
               this.fecha=fecha;
               this.numpers=numpers;
               this.forkeyuser=forkeyuser;
               this._forkeyrest=forkyerest;

        }
    }
}