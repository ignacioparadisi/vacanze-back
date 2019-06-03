namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class NotValidFieldException: GeneralException
    {
        public NotValidFieldException(string message) : base(message)
        {
        }
    }
}