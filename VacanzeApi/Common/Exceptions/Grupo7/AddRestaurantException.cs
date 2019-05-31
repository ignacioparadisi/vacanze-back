using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class AddRestaurantException : Exception
    {
        string _Menssage;
        public AddRestaurantException(string message): base(message)
        {
        }
    }
}