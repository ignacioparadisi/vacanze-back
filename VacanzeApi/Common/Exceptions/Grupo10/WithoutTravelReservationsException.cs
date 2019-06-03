using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo10
{
    public class WithoutTravelReservationsException : Exception{
        private int _travelId;
        private int _locationId;
        private string _message;
        private Exception _ex;

        public WithoutTravelReservationsException(string _message) :base(_message){
            this._message = _message;
        }

        public WithoutTravelReservationsException(string _message, Exception _ex) :this(_message){
            this.Ex = _ex;
        }

        public WithoutTravelReservationsException(int _travelId, int _locationId, string _message) :this(_message){
            this.TravelId = _travelId;
            this.LocationId = _locationId;
        }

        public WithoutTravelReservationsException(int _travelId, int locationId, string _message, Exception _ex) 
        :this(_message, _ex){
            this.TravelId = _travelId;
            this.LocationId = _locationId;
        }

        public Exception Ex { get{ return _ex; } set{ _ex = value; } }
        public int LocationId{ get{ return _locationId; } set{ _locationId = value; } }
        public int TravelId{ get{ return _travelId; } set{ _travelId = value; } }
    }
}