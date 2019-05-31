using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class AddRestaurantException : Exception
    {
        public AddRestaurantException(string message): base(message)
        {
        }
    }
}