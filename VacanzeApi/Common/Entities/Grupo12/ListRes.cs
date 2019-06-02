
namespace vacanze_back.VacanzeApi.Common.Entities.Grupo12{
    public class ListRes : Entity{

        public int _id{ get; set;}
        public int _price{ get; set;}
        public int _priceupdate{ get; set;}
        public string _dateI{get; set;}
        public string _dateV{get; set;}
        public string _name_country_i{get;set;}
        public string _name_country_V{get;set;}

        public string _name_city_i{get;set;}
        public string _name_city_V{get;set;}
         public int _sum_capacity{get;set;}
        public int _id_city{get;set;}
        public int _sum_pas{get;set;}
        public int _seatavailable{get;set;}
    
        public ListRes():base(0){}
        
        public ListRes(int id,int price,int priceupdate,string dateI,
        string name_country_i ,string name_country_V,int seatavailable,string name_city_i,
        string name_city_V ):base(id){

            _id=id;
            _price=price;
            _priceupdate=priceupdate;
            _dateI=dateI;
            _name_country_i=name_country_i;
            _name_country_V=name_country_V;
            _seatavailable=seatavailable;
            _name_city_i=name_city_i;
            _name_city_V=name_city_V;
        }
    }}