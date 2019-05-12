namespace vacanze_back.Entities.Grupo3
{
    public class Flight: Entity
    {   
        public int airplane { get; set; }
        public int route { get; set; }
        public float price { get; set; }

        public Flight(long id, int airplane, int route, float price) 
        {
            setId(id);
            this.airplane = airplane;
            this.route = route;
            this.price = price;
               
        }

        public Flight(){

        }
    }
}