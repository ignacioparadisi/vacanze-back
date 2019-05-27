using System.Collections.Generic;

namespace vacanze_back.Entities.Grupo2
{
    public class Travel : Entity{

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

        private List<Entity> _carReservations;
        private List<Entity> _hotelReservations; 
        private List<Entity> _restaurantReservations;
        private List<Entity> _flightReservations;

        public Travel(long id, string _name, string _description, User _user) :base(id){
            this._name = _name;
            this._description = _description;
            this._user = _user;
            
            _carReservations = new List<Entity>();
            _hotelReservations = new List<Entity>();
            _restaurantReservations = new List<Entity>();
            _flightReservations = new List<Entity>();
        }

        public void AddCar(Entity car){
            _carReservations.Add(car);
        }

        public void AddHotel(Entity hotel){
            _hotelReservations.Add(hotel);
        }

        public void AddRestaurant(Entity restaurant){
            _hotelReservations.Add(restaurant);
        }
        
        public void AddFlight(Entity flight){
            _hotelReservations.Add(flight);
        }
    }
}