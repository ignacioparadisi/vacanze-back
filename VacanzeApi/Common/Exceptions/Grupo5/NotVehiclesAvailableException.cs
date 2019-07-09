using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo5
{
    public class NotVehiclesAvailableException : Exception
    {
        private string _message;
        private Exception _ex;
        private int _locationId;

        public NotVehiclesAvailableException(string _message) :base(_message)
        {
            this._message = _message;
        }

        public NotVehiclesAvailableException(int _locationId, string _message) :this(_message)
        {
            this._locationId = _locationId;
        }

        public NotVehiclesAvailableException(string _message, Exception _ex) :this(_message)
        {
            this._ex = _ex;
        }

        public NotVehiclesAvailableException(int _locationId, string _message, Exception _ex) :this(_message, _ex)
        {
            this._locationId = _locationId;
        }


        public Exception Ex { get{ return _ex; } set{ _ex = value; } }
        public int LocationId { get{ return _locationId; } set{ _locationId = value; } }
    }
}