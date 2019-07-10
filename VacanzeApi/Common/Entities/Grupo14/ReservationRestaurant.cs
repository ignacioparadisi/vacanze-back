namespace vacanze_back.VacanzeApi.Common.Entities.Grupo14 {
    public class Restaurant_res: Entity{

        public string fecha_res { get; set; } //Fecha de reserva
       
        public int cant_people{get; set;} //Comensales

        public string date { get; set;} //Current date

        public int user_id { get; set;} //ID del usuario registrado

        public int rest_id { get; set;} //ID del restaurante seleciconado

        public int pay_id {get; set;} //ID del pago
        
        //Atributos para el getReservation
        public string restaurantName {get; set;} //Nombre del restaurante

        public string restaurantAddress {get; set;} //Direccion del restaurante

        public string cityName{get; set;} //Ciudad seleccionada

        public string countryName{get; set;} //Pais donde esta esa ciudad

        //Atributo para que el equipo de pago sepa que tipo de reservacion es
        public string typeReservation{get; set;} //Atributo para que el equipo de pago sepa que tipo de reservacion es
       
        //Constructor para el POST
        public Restaurant_res(string fecha_reservacion, int cant_persona, string fecha,
            int user_ID, int rest_ID) : base(0)
        {
            fecha_res = fecha_reservacion;  //rr_date
            cant_people = cant_persona;     //rr_num_ppl
            date = fecha;                   //rr_timestamp
            user_id = user_ID;              //rr_use_id
            rest_id = rest_ID;              //rr_res_fk
        }

        //Contructor para el PUT
        public Restaurant_res(int pay_ID): base(0){
            pay_id = pay_ID;
        }

        //Constructor para el GET de todas las reservas del usuario
        public Restaurant_res(int id,string locationName, string pais,  string restName,
         string address, string fecha_reservacion, int cant_persona) : base(id)
        {
            rest_id = id;
            fecha_res = fecha_reservacion; //rr_date = fecha a futuro a la cual reservo el usuario
            cant_people = cant_persona; //rr_num_ppl
            restaurantName = restName;
            restaurantAddress = address;
            cityName = locationName;
            countryName = pais;
        }

        //Constructor para el GET reservas sin pagar
        public Restaurant_res(int id, string fecha_reservacion, string tipo): base(id){
            fecha_res = fecha_reservacion; //Fecha a futuro a la cual reservo el usuario
            typeReservation = tipo;
        }
    }
}