using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo5
{
    public class WithoutExistenceOfBrandsException : Exception
    {
        private string _message;
        private Exception _ex;

        public WithoutExistenceOfBrandsException(string _message) :base(_message)
        {
            this._message = _message;
        }

        public WithoutExistenceOfBrandsException(string _message, Exception _ex) :this(_message)
        {
            this._ex = _ex;
        }

        public Exception Ex { get{ return _ex; } set{ _ex = value; } }
    }
}