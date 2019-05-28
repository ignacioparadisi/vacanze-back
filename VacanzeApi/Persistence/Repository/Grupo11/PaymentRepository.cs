using System;
using System.Collections.Generic;
using System.Data;
using vacanze_back.VacanzeApi.Common.Entities.Grupo11;
using vacanze_back.VacanzeApi.Common.Util;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo11
{
    public class PaymentRepository
    {


        public static List<Order> GetInfoOrder(long id, int type)
        {
            DataTable table;
            try
            {
                 table = PgConnection.Instance.ExecuteFunction("Get_Info_Order(@id,@type)", id, type);
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
            return ConvertDt.ConvertDataTable<Order>(table);
        }

        public static List<Payment> GetPaymentMethod()
        {
            var oPayment = new List<Payment>();
            oPayment.Add(new Payment(1, "TDC", true));
            oPayment.Add(new Payment(2, "EFECTIVO", true));
            oPayment.Add(new Payment(3, "TRANSFER", true));
            oPayment.Add(new Payment(4, "TDB", true));


            return oPayment;
        }





    }
}
