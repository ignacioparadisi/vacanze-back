namespace vacanze_back.VacanzeApi.Common.Entities.Grupo9
{
    public class Claim : Entity
    {
        public string _title { get; set; }
        public string _description { get; set; }
        public string _status { get; set; }
        
        public Claim(string title, string description, string status) : base(0)
        {
            _title = title;
            _description = description;
            _status = status;
        }

        public Claim(int id, string title, string description, string status) : base(id)
        {
            _title = title;
            _description = description;
            _status = status;
        }
    }
}