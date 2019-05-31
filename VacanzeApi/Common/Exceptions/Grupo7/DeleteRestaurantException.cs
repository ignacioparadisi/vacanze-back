using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class DeleteRestaurantException : Exception
    {
        string _Menssage;
        public DeleteRestaurantException(string message): base(message)
        {
        }
    }
}