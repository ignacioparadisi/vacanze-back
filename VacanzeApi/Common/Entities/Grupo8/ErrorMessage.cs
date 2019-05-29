using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo8
{
    public class ErrorMessage
    {
        public int StatusCode;
        public string Error;
        
        public ErrorMessage(int statusCode, string value)
        {
            StatusCode = statusCode;
            Error = value;
        }
    }
}