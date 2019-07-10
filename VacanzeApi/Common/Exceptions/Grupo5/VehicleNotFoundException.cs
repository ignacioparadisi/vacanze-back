using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo5
{
    public class VehicleNotFoundException : Exception
    {
        private string _message;
        private Exception _ex;
        private int _vehicleId;

        public VehicleNotFoundException(string _message) :base(_message)
        {
            this._message = _message;
        }

        public VehicleNotFoundException(int _vehicleId, string _message) :this(_message)
        {
            this._vehicleId = _vehicleId;
        }

        public VehicleNotFoundException(string _message, Exception _ex) :this(_message)
        {
            this._ex = _ex;
        }

        public VehicleNotFoundException(int _vehicleId, string _message, Exception _ex) :this(_message, _ex)
        {
            this._vehicleId = _vehicleId;
        }


        public Exception Ex { get{ return _ex; } set{ _ex = value; } }
        public int VehicleId { get{ return _vehicleId; } set{ _vehicleId = value; } }
    }
}