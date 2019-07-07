

namespace vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo5
{
    public class VehicleDTO : DTO
    {
        private int _id;
        public int Id { get{ return _id; } set{ _id = value; } }
        
        private int _vehiculeModelId;
        public int VehicleModelId { get{ return _vehiculeModelId; } set{ _vehiculeModelId = value; } }

        private int _vehicleLocationId;
        public int VehicleLocationId { get{ return _vehicleLocationId; } set{ _vehicleLocationId = value; } }

        private string _license;
        public string License { get{ return _license; } set{ _license = value; } }

        private double _price;
        public double Price { get{ return _price; } set{ _price = value; } }

        private bool _status;
        public bool Status { get{ return _status; } set{ _status = value; } }

        public VehicleDTO(){}

        public VehicleDTO(int _vehicleModelId, int _vehicleLocationId, string _license, double _price, bool _status){
            VehicleModelId = _vehicleModelId;
            VehicleLocationId = _vehicleLocationId;
            License = _license;
            Price = _price;
            Status = _status;
        }

        public VehicleDTO(int Id, int _vehicleModelId, int _vehicleLocationId, string _license, double _price, bool _status){
            this.Id = Id;
            VehicleModelId = _vehicleModelId;
            VehicleLocationId = _vehicleLocationId;
            License = _license;
            Price = _price;
            Status = _status;
        }
    }
}