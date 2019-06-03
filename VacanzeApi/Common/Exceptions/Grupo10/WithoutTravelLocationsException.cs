using System;
namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo10
{
    public class WithoutTravelLocationsException : Exception{

        private string _message;
        private Exception _ex;


        public WithoutTravelLocationsException(string _message) :base(_message){
            this._message = _message;
        }

        public WithoutTravelLocationsException(string _message, Exception _ex) :this(_message){
            this.Ex = _ex;
        }

        public Exception Ex { get{ return _ex; } set{ _ex = value; } }
    }
}