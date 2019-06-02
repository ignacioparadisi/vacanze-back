namespace vacanze_back.VacanzeApi.Common.Entities.Grupo8
{
    public class ErrorMessage
    {
        public ErrorMessage(string value)
        {
            Error = value;
        }

        public string Error { get; set; }
    }
}