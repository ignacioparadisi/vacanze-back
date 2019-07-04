namespace vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo7
{
    public class RestaurantDTO
    {
        public int Id { get; set; }
        public string Name  { get; set; }
        public int Capacity  { get; set; }
        public bool IsActive { get; set; }
        public decimal Qualify { get; set;  }
        public string Specialty { get; set;  }
        public decimal Price { get; set; }
        public string BusinessName { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public int Location { get; set; }
        public string Address { get; set; }
    }
}