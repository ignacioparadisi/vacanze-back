using System;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo10   
{
    public class Travel : Entity{

        private List<Location> locations = new List<Location>();

        private User _user;
        public User User{ get{ return _user; } set{ _user = value; } }

        private long _userId;
        public long UserId{ get { return _userId; } set{ _userId = value; } }

        private string _name;
        public string Name{ get{ return _name; } set{ _name = value; } }

        private DateTime _init;
        public DateTime Init{ get{ return _init; } set{ _init = value; } }

        private DateTime _end;
        public DateTime End{ get{ return _end; } set{ _end = value; } }

        private string _description;
        public string Description{ get{ return _description; } set{ _description = value; } }

        public Travel(long id, string _name, DateTime _init, DateTime _end, string _description, long _userId) :base(id){
            this.Id = id;
            this.Name = _name;
            this.Init = _init;
            this.End = _end;
            this.Description = _description;
            this.UserId = _userId;
        }

        public Travel(string _name, DateTime _init, DateTime _end, string _description, long _userId) :base(0){
            this.Name = _name;
            this.Init = _init;
            this.End = _end;
            this.Description = _description;
            this.UserId = _userId;
        }

        public Travel(string _name, DateTime _init, DateTime _end, string _descriptio) :base(0){
            this.Name = _name;
            this.Init = _init;
            this.End = _end;
            this.Description = _description;
        }

        public Travel() :base(0){}

        public void AddLocation(Location location){
            this.locations.Add(location);
        }

    }
}