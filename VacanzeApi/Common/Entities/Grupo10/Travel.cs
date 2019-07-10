using System;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities.Grupo12;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities.Grupo14;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo10   
{
    public class Travel{

        private List<Location> locations = new List<Location>();

        private List<ReservationRoom> _roomReservations = new List<ReservationRoom>();
         
         private List<ReservationVehicle> _carReservations = new List<ReservationVehicle>() ; 

        /*private List<> _flightReservations = new (); */

        private int _id;
        public int Id{ get{ return _id; } set{ _id = value; } }

        private User _user;
        public User User{ get{ return _user; } set{ _user = value; } }

        private int _userId;
        public int UserId{ get { return _userId; } set{ _userId = value; } }

        private string _name;
        public string Name{ get{ return _name; } set{ _name = value; } }

        private DateTime _init;
        public DateTime Init{ get{ return _init; } set{ _init = value; } }

        private DateTime _end;
        public DateTime End{ get{ return _end; } set{ _end = value; } }

        private string _description;
        public string Description{ get{ return _description; } set{ _description = value; } }

        

        public Travel(int _id, string _name, DateTime _init, DateTime _end, string _description, int _userId){
            this.Id = _id;
            this.Name = _name;
            this.Init = _init;
            this.End = _end;
            this.Description = _description;
            this.UserId = _userId;
        }

        public Travel(string _name, DateTime _init, DateTime _end, string _description, int _userId){
            this.Name = _name;
            this.Init = _init;
            this.End = _end;
            this.Description = _description;
            this.UserId = _userId;
        }

        public Travel(string _name, DateTime _init, DateTime _end, string _description){
            this.Name = _name;
            this.Init = _init;
            this.End = _end;
            this.Description = _description;
        }

        public Travel(){}

        public void AddLocation(Location location){
            this.locations.Add(location);
        }

    }
}