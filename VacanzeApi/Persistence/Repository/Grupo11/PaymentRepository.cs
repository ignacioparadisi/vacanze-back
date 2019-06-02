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
        /// <summary>
        /// Función que retorna la información de una reserva u orden
        /// </summary>
        /// <param name="id">Número de identificación.</param>
        /// <param name="type">Tipo de orden.</param>
        /// <returns>Retorna una lista de ordenes.</returns>

        public static List<Order> GetInfoOrder(long id, int type)
        {
            DataTable _table;
             var _oListOrder = new List<Order>();
            try
            {
              //Se hace la llamada al store procedure.
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
            /// <summary>
            /// Función que retorna los métodos de pago disponibles.
            /// </summary>
            /// <returns>Retorna la lista de los tipos de pago.</returns>

            var oPayment = new List<Payment>();
            oPayment.Add(new Payment(1, "TDC", true));
            oPayment.Add(new Payment(2, "EFECTIVO", true));
            oPayment.Add(new Payment(3, "TRANSFER", true));
            oPayment.Add(new Payment(4, "TDB", true));


            return oPayment;
        }

        public static long  AddPayment(Bill bill)
        { 
            /// <summary>
            /// Función que asocia un pago con una factura. 
            /// </summary>
            /// <param name="bill">Objeto del tipo bill o factura</param>
            /// <returns>Retorna la factura ya pagada.</returns>

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

            /// <summary>
            /// Función que da un mensaje aleatorio al usuario acerca de su transaccion
            /// </summary>
            /// <returns>Retorna una respuesta basada en el random.</returns>

            Random random = new Random();
            try
            {
                //Aqui inicia el random
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
