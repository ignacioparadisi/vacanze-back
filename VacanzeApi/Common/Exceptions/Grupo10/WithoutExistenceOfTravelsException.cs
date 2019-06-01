using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;

namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo10
{
    public class WithoutExistenceOfTravelsException : Exception
    {
        private long _userId;
        private string _message;
        private Exception _ex;

        public WithoutExistenceOfTravelsException(string _message) :base(_message)
        {
            this._message = _message;
        }

        public WithoutExistenceOfTravelsException(long _userId, string _message) :this(_message)
        {
            this._userId = _userId;
        }

        public WithoutExistenceOfTravelsException(string _message, Exception _ex) :this(_message)
        {
            this._ex = _ex;
        }

        public WithoutExistenceOfTravelsException(long _userId, string _message, Exception _ex) :this(_message, _ex)
        {
            this._userId = _userId;
        }
    }
}