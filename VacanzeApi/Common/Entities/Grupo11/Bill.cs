
using vacanze_back.VacanzeApi.Common.Entities;

namespace vacanze_back.Entities.Grupo11
{
    public class Bill : Entity
    {

        public int id { get; set; }
        public int paymentMethod { get; set; }
        public string reference { get; private set; }
        public long total{get; set;}


        public Bill(int _id, int _paymentMethod, string _reference, long _total) : base(_id)
        {
            id = _id;
            paymentMethod = _paymentMethod;
            reference = _reference;
            total = _total;
        }


        public long getId()
        {
            return id;
        }

        public void setId(int _id)
        {
            id = _id;
        }


        public int getPaymentMethod()
        {
            return paymentMethod;
        }

        public void setPaymentMethod(int _paymentMethod)
        {
            paymentMethod = _paymentMethod;
        }


        public string getReference()
        {
            return reference;
        }

        public void setReference(string _reference)
        {
            reference = _reference;
        }

        public long getTotal()
        {
            return total;
        }

        public void setTotal(long _total)
        {
            total = _total;
        }

    }
}
