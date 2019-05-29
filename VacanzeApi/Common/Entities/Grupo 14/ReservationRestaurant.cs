namespace vacanze_back.VacanzeApi.Common.Entities.Grupo14 {
    public class Restaurant_res: Entity{

        public string fecha_res { get; set; }
       
        public int cant_people{get; set;}

        public string date { get; set;}

        public int user_id { get; set;}

        public int rest_id { get; set;}
        
        public Restaurant_res(int id, string fecha_reservacion, int cant_persona, string fecha,
            int user_ID, int rest_ID) : base(id)
        {
            fecha_res = fecha_reservacion; //rr_date = fecha a futuro a la cual reservo el usuario
            cant_people = cant_persona; //rr_num_ppl
            date = fecha; //rr_timestamp = fecha actual que es el dia que se metio en la pagina e hizo la reserva
            user_id = user_ID; //rr_use_id
            rest_id = rest_ID; //rr_res_fk
        }

        public Restaurant_res(string fecha_reservacion, int cant_persona, string fecha,
            int user_ID, int rest_ID) : base(0)
        {
            fecha_res = fecha_reservacion; //rr_date
            cant_people = cant_persona; //rr_num_ppl
            date = fecha; //rr_timestamp
            user_id = user_ID; //rr_use_id
            rest_id = rest_ID; //rr_res_fk
        }
        
    }
}