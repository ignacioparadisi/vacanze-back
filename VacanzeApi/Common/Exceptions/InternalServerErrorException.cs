using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions{

    public class InternalServerErrorException : Exception{

        private string _message;
        private Exception _ex;

         public InternalServerErrorException(string _message) :base(_message)
        {
            this._message = _message;
        }

        public InternalServerErrorException(string _message, Exception _ex) :this(_message)
        {
            this._ex = _ex;
        }

        public Exception Ex { get{ return _ex; } set{ _ex = value; } }

    }

}