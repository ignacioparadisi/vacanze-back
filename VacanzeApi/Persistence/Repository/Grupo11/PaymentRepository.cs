using System;
using System.Collections.Generic;
using System.Data;
using vacanze_back.Entities.Grupo11;
using vacanze_back.VacanzeApi.Common.Entities.Grupo11;
using vacanze_back.VacanzeApi.Common.Util;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo11
{
    public class PaymentRepository
    {


        public static List<Order> GetInfoOrder(long id, int type)
        {
            DataTable _table;
             var _oListOrder = new List<Order>();
            try
            {
                 _table = PgConnection.Instance.ExecuteFunction("getinfoorder(@id,@type)", id, type);

                for (int i = 0; i < _table.Rows.Count; i++)
                {
                    _oListOrder.Add(
                        new Order(
                        Convert.ToInt64( _table.Rows[i][0]),
                        Convert.ToString(_table.Rows[i][1]),
                        Convert.ToString(_table.Rows[i][2]),
                        Convert.ToString(_table.Rows[i][3]),
                        Convert.ToDouble(_table.Rows[i][4]),
                        Convert.ToDouble(_table.Rows[i][5]),
                        Convert.ToDouble(_table.Rows[i][6]))
                       
                        );
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
           
            return _oListOrder;
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

        public static long  AddPayment(Bill bill)
        {
            long _iResp; 
            
            try
            {
               var _table = PgConnection.Instance.ExecuteFunction(
                "addPayment(@payMethod,@payTotal)",
                 bill.paymentMethod,
                 bill.total);
                _iResp = Convert.ToInt32(_table.Rows[0][0]);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return _iResp;
        } 

        public static (int,string) PayProcessResponse()
        {

            Random random = new Random();
            try
            {
                var _genResp = random.Next(1, 10);

                switch (_genResp)
                {
                    case 2: 
                        return (-2, "Crédito no aprobado");
                    case 3:
                        return (-3, "Transacción Rechazada");
                    case 4:
                        return (-4, "No procesado");
                    case 5:
                        return (-5, "Banco no responde");

                    default:
                        return (0, "Transacción Aprobada");
                }


            }
            catch (Exception ex)
            {

                throw  ex;
            }
        }

    }
}
