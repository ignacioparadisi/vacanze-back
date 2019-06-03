
using System;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo11
{
    public class Transaction :Entity
    {
       

        public long id { get; set; }
        public string descrip { get; set; }
        public string pm { get; set; }
        public double amount { get; set; }
        public DateTime dateR { get; set; }
     

        public Transaction(long _id , string _descrip, string _pm, double _amount, DateTime _dateR ) : base(_id)
        {
            id = _id;
            descrip = _descrip;
            pm =_pm;
            amount = _amount;
            dateR = _dateR;
        }


        public long getId()
        {
            return id;
        }

        public void setId(long _id)
        {
            id = _id;
        }


        public string getDescrip()
        {
            return descrip;
        }

        public void setName(string _descrip)
        {
            descrip = _descrip;
        }

        public string getPm()
        {
            return pm;
        }
        public void setPm(string _pm)
        {
            descrip = _pm;
        }

        public double getAmount()
        {
            return amount;
        }
        public void setAmount(double _amount)
        {
            amount = _amount;
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


