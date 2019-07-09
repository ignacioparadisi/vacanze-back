using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo5
{
    public class BrandNotFoundException : Exception
    {
        private string _message;
        private Exception _ex;
        private int _brandId;

        public BrandNotFoundException(string _message) :base(_message)
        {
            this._message = _message;
        }

        public BrandNotFoundException(int _brandId, string _message) :this(_message)
        {
            this._brandId = _brandId;
        }

        public BrandNotFoundException(string _message, Exception _ex) :this(_message)
        {
            this._ex = _ex;
        }

        public BrandNotFoundException(int _brandId, string _message, Exception _ex) :this(_message, _ex)
        {
            this._brandId = _brandId;
        }


        public Exception Ex { get{ return _ex; } set{ _ex = value; } }
        public int BrandId { get{ return _brandId; } set{ _brandId = value; } }
    }
}