using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions{

    public class UniqueAttributeException : Exception{
        private string _message;
        private Exception _ex;

        public UniqueAttributeException(string _message) :base(_message){
            this._message = _message;
        }

         public UniqueAttributeException(string _message, Exception _ex) :this(_message)
        {
            this._ex = _ex;
        }

        public Exception Ex { get{ return _ex; } set{ _ex = value; } }
    }
}