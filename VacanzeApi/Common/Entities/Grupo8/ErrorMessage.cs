using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo8
{
    public class ErrorMessage
    {
        public string Error;
        
        public ErrorMessage( string value)
        {
            Error = value;
        }
    }
}