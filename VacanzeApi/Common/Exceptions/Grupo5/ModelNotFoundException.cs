using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo5
{
    public class ModelNotFoundException : Exception
    {
        private string _message;
        private Exception _ex;
        private int _modelId;

        public ModelNotFoundException(string _message) :base(_message)
        {
            this._message = _message;
        }

        public ModelNotFoundException(int _modelId, string _message) :this(_message)
        {
            this._modelId = _modelId;
        }

        public ModelNotFoundException(string _message, Exception _ex) :this(_message)
        {
            this._ex = _ex;
        }

        public ModelNotFoundException(int _modelId, string _message, Exception _ex) :this(_message, _ex)
        {
            this._modelId = _modelId;
        }


        public Exception Ex { get{ return _ex; } set{ _ex = value; } }
        public int ModelId { get{ return _modelId; } set{ _modelId = value; } }
    }
}