using System;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo5
{
    public class Vehicle : Entity{

        private Model _vehicleModel;
        public Model VehicleModel { get{ return _vehicleModel; } set{ _vehicleModel = value; } }

        private int _vehiculeModelId;
        public int VehicleModelId 
        { 
            get{ return _vehiculeModelId; } 
            set
            { 
                if(value == 0)
                    throw new RequiredAttributeException("El vehiculo debe tener un modelo");
                else
                    _vehiculeModelId = value; 
            } 
        }

        private Location _vehicleLocation;
        public Location VehicleLocation { get{ return _vehicleLocation; } set{ _vehicleLocation  = value; } }

        private int _vehicleLocationId;
        public int VehicleLocationId 
        { 
            get{ return _vehicleLocationId; } 
            set
            { 
                if(value == 0)
                    throw new RequiredAttributeException("El vehiculo debe tener un lugar");
                else
                    _vehicleLocationId = value; 
            } 
        }

        private string _license;
        public string License 
        { 
            get{ return _license; } 
            set
            { 
                if(value == null || value.Equals(""))
                    throw new RequiredAttributeException("El vehiculo debe tener una matricula");
                else
                    _license = value; 
            } 
        }

        private double _price;
        public double Price 
        { 
            get{ return _price; } 
            set
            { 
                if(value == 0)
                    throw new RequiredAttributeException("El vehiculo debe tener un precio");
                else
                    _price = value; 
            } 
        }

        private bool _status;
        public bool Status 
        { 
            get{ return _status; } 
            set
            { 
                if(value == null)
                    throw new RequiredAttributeException("El vehiculo debe crearse con un status");
                else
                    _status = value; 
            } 
        }

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