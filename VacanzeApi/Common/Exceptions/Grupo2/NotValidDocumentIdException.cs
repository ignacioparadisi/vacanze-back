using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class NotValidDocumentIdException : GeneralException
    {
        public NotValidDocumentIdException() : base()
        {
        }

        public NotValidDocumentIdException(string message) : base(message)
        {
        }
    }
}