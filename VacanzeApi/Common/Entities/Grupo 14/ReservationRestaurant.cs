namespace vacanze_back.VacanzeApi.Common.Entities.Grupo14 {
    public class Restaurant_res: Entity{
        public Restaurant_res(long id, string fecha_reservacion, string fecha, 
            long cant_persona) : base(id)
        {
            fecha_res = fecha_reservacion;
            date = fecha;
            cant_people = cant_persona;
        }

        public Restaurant_res(string fecha_reservacion, string fecha,
            long cant_persona) : base(0)
        {
            fecha_res = fecha_reservacion;
            date = fecha;
            cant_people = cant_persona;
        }

        public string fecha_res { get; set; }
       
        public long cant_people{get; set;}

        public string date { get; set;}
        
    }
}