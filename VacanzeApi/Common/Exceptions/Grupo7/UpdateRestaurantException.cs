using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class UpdateRestaurantException : Exception
    {
        string _Menssage;
        public UpdateRestaurantException(string message): base(message)
        {
        }
    }
}