namespace vacanze_back.VacanzeApi.Common.Entities.Grupo9
{
    public class Baggage 
    {
        public int _id { get; set; }
        public string _description { get; set; }
        public string _status { get; set; }

        public Baggage(int id, string description, string status)
        {
            _id = id;
            _description = description;
            _status = status;
        }
    }
}