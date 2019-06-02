
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
        public int _sum_capacity{get;set;}
        public int _id_city{get;set;}
        public int _sum_pas{get;set;}
        public int _price{get;set;}
        public int _priceupdate{get;set;}
    
        public FlightRes():base(0){}
        
        public FlightRes(string seatNum,string timestamp,int numPas,int id_user,int id_pay,int id_fli ):base(0){

            _seatNum=seatNum;
            _timestamp=timestamp;
            _numPas=numPas;
            _id_user=id_user;
            _id_pay=id_pay;
            _id_fli=id_fli;
           
            
        }

        public FlightRes(int id,string seatNum,string timestamp,int numPas,int id_user,int id_pay,int id_fli ):base(id){
            _id=id;
            _seatNum=seatNum;
            _timestamp=timestamp;
            _numPas=numPas;
            _id_user=id_user;
            _id_pay=id_pay;
            _id_fli=id_fli;
           
            
        }

         public FlightRes(string seatNum,string timestamp,int numPas,int id_user,int id_fli ):base(0){

            _seatNum=seatNum;
            _timestamp=timestamp;
            _numPas=numPas;
            _id_user=id_user;
            _id_fli=id_fli;
           
            
        }


        
         /* public FlightRes(int id,string seatNum,string timestamp,int numPas,
         int id_user,int id_fli, int id_pay ):base(0){
*/
           
            
        //}




    }
}