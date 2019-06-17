
using System;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo11
{
    public class Transaction :Entity
    {
       /// <summary>
       /// Clase de trnasaccion. Se utiliza para simular la transaccion entre calcular la cuenta de las reservaciones y
       /// luego proceder a hacer el pago de las mismas.
       /// </summary>
       public string descrip { get; set; }
        public string pm { get; set; }
        public double amount { get; set; }
        public DateTime dateR { get; set; }
     

        public Transaction(int _id , string _descrip, string _pm, double _amount, DateTime _dateR ) : base(_id)
        {
            descrip = _descrip;
            pm =_pm;
            amount = _amount;
            dateR = _dateR;
        }


        public long getId()
        {
            return Id;
        }

        public void setId(int _id)
        {
            Id = _id;
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


