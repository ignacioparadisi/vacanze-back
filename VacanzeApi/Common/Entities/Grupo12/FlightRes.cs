
namespace vacanze_back.VacanzeApi.Common.Entities.Grupo12{
    public class FlightRes : Entity{

        public int _id{ get; set;}
        public string _seatNum{ get; set;}
        public string _timestamp{ get; set;}
        public int _numPas{get; set;}
        public int _id_user{get;set;}
        public int _id_fli{get;set;}
        public int _id_pay{get;set;}
        public int _num_capacity{get;set;}
        public int _id_city{get;set;}
        public int _sum_pas{get;set;}
        public int _price{get;set;}
        public string _nameplane{get;set;}
        public string _dateI{get;set;}
        public string _dateV{get;set;}
        public string _namecityI{get;set;}
        public string _namecountryI{get;set;}
        public string _namecityV{get;set;}
        public string _namecountryV{get;set;}
        public int _idres{get;set;}

    
        public FlightRes():base(0){}
        

        public FlightRes(string seatNum,string timestamp,int numPas,int id_user,int id_fli ):base(0){

            _seatNum=seatNum;
            _timestamp=timestamp;
            _numPas=numPas;
            _id_user=id_user;
            _id_fli=id_fli;
           
            
        }


        
         public FlightRes(int id,int price,string timestamp,string seatNum,
         string name_cityI,string name_countryI, string namecityV,string namecountryV,int numPas):base(id){

             _id=id;
             _price=price;
             _timestamp=timestamp;
             _seatNum=seatNum;
             _numPas=numPas;
             _namecityI=name_cityI;
             _namecountryI=name_countryI;
             _namecityV=namecityV;
             _namecountryV=namecountryV;
             
         }




    }
}