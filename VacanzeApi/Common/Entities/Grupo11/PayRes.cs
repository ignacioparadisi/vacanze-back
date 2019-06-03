
using System;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo11
{
    public class PayRes :Entity
    {
       

        public long id { get; set; }
        public string name { get; set; }
        public DateTime dateR { get; set; }
     

        public PayRes(long _id , string _name, DateTime _dateR ) : base(_id)
        {
            id = _id;
            name = _name;
            dateR =_dateR;
        }


        public long getId()
        {
            return id;
        }

        public void setId(long _id)
        {
            id = _id;
        }


        public string getName()
        {
            return name;
        }

        public void setName(string _name)
        {
            name = _name;
        }


        public DateTime getdateR()
        {
            return dateR;
        }

        public void setdateR(DateTime _dateR)
        {
            dateR = _dateR;
        }
    }
}


