
namespace vacanze_back.VacanzeApi.Common.Entities.Grupo5
{
    public class Vehicle : Entity{

        private Model _vehicleModel;
        public Model VehicleModel { get{ return _vehicleModel; } set{ _vehicleModel = value; } }

        private int _vehiculeModelId;
        public int VehicleModelId { get{ return _vehiculeModelId; } set{ _vehiculeModelId = value; } }

        private Location _vehicleLocation;
        public Location VehicleLocation { get{ return _vehicleLocation; } set{ _vehicleLocation  = value; } }

        private int _vehicleLocationId;
        public int VehicleLocationId { get{ return _vehicleLocationId; } set{ _vehicleLocationId = value; } }

        private string _license;
        public string License { get{ return _license; } set{ _license = value; } }

        private double _price;
        public double Price { get{ return _price; } set{ _price = value; } }

        private bool _status;
        public bool Status { get{ return _status; } set{ _status = value; } }

        public Vehicle(int _vehicleModelId, int _vehicleLocationId, string _license, double _price, bool _status) :base(0){
            VehicleModelId = _vehicleModelId;
            VehicleLocationId = _vehicleLocationId;
            License = _license;
            Price = _price;
            Status = _status;
        }

        public Vehicle(Model _vehicleModel, Location _vehicleLocation, string _license, double _price, bool _status) :base(0){
            VehicleModel = _vehicleModel;
            VehicleLocation = _vehicleLocation;
            License = _license;
            Price = _price;
            Status = _status;
        }

        public Vehicle(int Id, int _vehicleModelId, int _vehicleLocationId, string _license, double _price, bool _status) :base(Id){
            this.Id = Id;
            VehicleModelId = _vehicleModelId;
            VehicleLocationId = _vehicleLocationId;
            License = _license;
            Price = _price;
            Status = _status;
        }

        public Vehicle(int Id, Model _vehicleModel, Location _vehicleLocation, string _license, double _price, bool _status) :base(Id){
            this.Id = Id;
            VehicleModel = _vehicleModel;
            VehicleLocation = _vehicleLocation;
            License = _license;
            Price = _price;
            Status = _status;
        }
    }
}