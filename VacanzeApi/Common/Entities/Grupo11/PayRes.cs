
using System;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo11
{
    /// <summary>
    /// Clase utilizada para realizar el pago de las reservas que haga el usuario
    /// </summary>
    public class PayRes :Entity
    {
        public string name { get; set; }
        public DateTime dateR { get; set; }
     

        public PayRes(int _id , string _name, DateTime _dateR ) : base(_id)
        {
            name = _name;
            dateR =_dateR;
        }


        public long getId()
        {
            return Id;
        }

        public void setId(int _id)
        {
            Id = _id;
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


