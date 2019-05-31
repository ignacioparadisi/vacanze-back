using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class DeleteRestaurantException : Exception
    {
        public DeleteRestaurantException(string message): base(message)
        {
        }
    }
}