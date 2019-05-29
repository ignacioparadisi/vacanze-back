using System;
namespace vacanze_back.VacanzeApi.Common.Entities.Grupo12{
    public class FlightRes : Entity{

        public string _seatNum{ get; set;}
        public string _timestamp{ get; set;}
        
        
        public FlightRes(long id,string seatNum,string timestamp):base(id){

            _seatNum=seatNum;
            _timestamp=timestamp;
            
        }

        public FlightRes(string seatNum,string timestamp):base(0){

            _seatNum=seatNum;
            _timestamp=timestamp;
            
        }




    }
}