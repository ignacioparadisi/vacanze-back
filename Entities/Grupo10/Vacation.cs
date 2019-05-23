namespace vacanze_back.Entities.Grupo2
{
    public class Vacation : Entity{

        private User _user;
        public User User{
            get{ return _user; }
            set{ _user = value; }
        }
        private string _name;
        public string Name{
            get{ return _name; }
            set{ _name = value; }
        }
        private string _description;
        public string Description{
            get{ return _description; }
            set{ _description = value; }
        }

        public Vacation(string _name, string _description, User _user){
            this._name = _name;
            this._description = _description;
            this._user = _user;
        }
        
    }
}