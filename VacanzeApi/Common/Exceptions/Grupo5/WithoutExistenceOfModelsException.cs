using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo5
{
    public class WithoutExistenceOfModelsException : Exception
    {
        private string _message;
        private Exception _ex;

        public WithoutExistenceOfModelsException(string _message) :base(_message)
        {
            this._message = _message;
        }

        public WithoutExistenceOfModelsException(string _message, Exception _ex) :this(_message)
        {
            this._ex = _ex;
        }

        public Exception Ex { get{ return _ex; } set{ _ex = value; } }
    }
}