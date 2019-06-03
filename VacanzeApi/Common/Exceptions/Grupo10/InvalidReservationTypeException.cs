using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo10
{
    public class InvalidReservationTypeException : Exception{
        private string _message;
        private Exception _ex;
        private string _type;

        public InvalidReservationTypeException(string _message) :base(_message){
            this._message = _message;
        }

        public InvalidReservationTypeException(string _type, string _message) :this(_message){
            this._type = _type;
        }

        public InvalidReservationTypeException(string _message, Exception _ex) :this(_message){
            this._ex = _ex;
        }

        public InvalidReservationTypeException(string _type, string _message, Exception _ex) :this(_message, _ex){
            this._type = _type;
        }
    }

}