using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class UpdateRestaurantException : Exception
    {
        public UpdateRestaurantException(string message): base(message)
        {
        }
    }
}