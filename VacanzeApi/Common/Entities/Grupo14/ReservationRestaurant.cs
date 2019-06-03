namespace vacanze_back.VacanzeApi.Common.Entities.Grupo14 {
    public class Restaurant_res: Entity{

        public string fecha_res { get; set; }
       
        public int cant_people{get; set;}

        public string date { get; set;}

        public int user_id { get; set;}

        public int rest_id { get; set;}

        public int pay_id {get; set;}
        
        public string restaurantName {get; set;}

        public string restaurantAddress {get; set;}

        public string cityName{get; set;}

        public string countryName{get; set;}

        public Restaurant_res(string fecha_reservacion, int cant_persona, string fecha,
            int user_ID, int rest_ID) : base(0)
        {
            fecha_res = fecha_reservacion; //rr_date
            cant_people = cant_persona; //rr_num_ppl
            date = fecha; //rr_timestamp
            user_id = user_ID; //rr_use_id
            rest_id = rest_ID; //rr_res_fk
        }

        public Restaurant_res(int pay_ID): base(0){
            pay_id = pay_ID;
        }

        public Restaurant_res(string locationName, string pais,  string restName,
         string address, string fecha_reservacion, int cant_persona) : base(0)
        {
            fecha_res = fecha_reservacion; //rr_date = fecha a futuro a la cual reservo el usuario
            cant_people = cant_persona; //rr_num_ppl
            restaurantName = restName;
            restaurantAddress = address;
            cityName = locationName;
            countryName = pais;
        }
        
    }
}