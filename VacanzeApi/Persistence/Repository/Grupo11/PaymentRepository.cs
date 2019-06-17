using System;
using System.Collections.Generic;
using System.Data;
using vacanze_back.Entities.Grupo11;
using vacanze_back.VacanzeApi.Common.Entities.Grupo11;
using vacanze_back.VacanzeApi.Common.Exceptions;
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


        public static List<Order> GetInfoOrder(long idAuto, long idRoo, long idRes, long idCru)
        {
            DataTable _table;
             var _oListOrder = new List<Order>();
            try
            {
                 _table = PgConnection.Instance.ExecuteFunction("getinfoorderAll(@idAuto,@idRoo,@idRes,@idCru)", idAuto,idRoo,idRes,idCru);

                for (int i = 0; i < _table.Rows.Count; i++)
                {
                    _oListOrder.Add(
                        new Order(
                        Convert.ToInt32( _table.Rows[i][0]),
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
            oPayment.Add(new Payment(1, "CREDITO", true));
            oPayment.Add(new Payment(2, "DEBITO", true));
            oPayment.Add(new Payment(3, "TRANSFERENCIA", true));
            oPayment.Add(new Payment(4, "EFECTIVO", true));
          


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

               if (_iResp > 0)
               {
                    _iResp = UpdatePaymentId(_iResp, bill.reference);

                }
                else
                {
                    throw new NotValidIdException("El id no es valido añadiendo el pago");
                }


            }
            catch (GeneralException ex)
            {

                throw ex;
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
            catch (GeneralException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        /// <summary>
        /// Devuelve reservas de habitacion 1 y auto 0 no pagadas
        /// </summary>
        /// <param name="id">Auto 1 o Hab 0</param>
        /// <param name="type"></param>
        /// <returns>Lis</returns>
        public static List<PayRes> GetPayRes(long id,int type)
        {
            DataTable _table;
            var _oListOrder = new List<PayRes>();
            try
            {
                _table = PgConnection.Instance.ExecuteFunction("getNoPaysResAutHab(@id,@type)", id, type);

                for (int i = 0; i < _table.Rows.Count; i++)
                {
                    _oListOrder.Add(
                        new PayRes(
                        Convert.ToInt32(_table.Rows[i][0]),
                        Convert.ToString(_table.Rows[i][1]),
                        Convert.ToDateTime(_table.Rows[i][2])) 
                        );
                }


            }
            catch (GeneralException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return _oListOrder;
        }


        /// <summary>
        /// Funcion que actualiza id del pago a cada tabla de reserva
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        public static long UpdatePaymentId(long id, string reference)
        {
            /// <summary>
            /// Función que asocia un pago con una factura. 
            /// </summary>
            /// <param name="bill">Objeto del tipo bill o factura</param>
            /// <returns>Retorna la factura ya pagada.</returns>

            long _iResp = 0;
          
            try
            {
                var _paramReference = reference.Split("|");
                //Si no es null
                if (_paramReference != null)
                {
                    //Si posee la data adecuadad y completa
                    if (_paramReference.Length == 4)
                    {
                        var _table = PgConnection.Instance.ExecuteFunction("PutPayProccess(@idpay,@idAuto,@idRoo,@idRes,@idCru)",
                            id,
                           Convert.ToInt64( _paramReference[0]),
                           Convert.ToInt64(_paramReference[1]),
                           Convert.ToInt64( _paramReference[2]),
                           Convert.ToInt64( _paramReference[3])
                            );
                        _iResp = Convert.ToInt32(_table.Rows[0][0]);
                    }
                    else
                    {
                        _iResp = -3;
                    }
                }
                else
                {
                    _iResp = -5;
                }
            }
            catch (GeneralException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return _iResp;
        }

        public static List<Transaction> GetTransaction(long id)
        {
            DataTable _table;
            var _oList = new List<Transaction>();
            try
            {
                _table = PgConnection.Instance.ExecuteFunction("getMyPayments(@id)", id);

                for (int i = 0; i < _table.Rows.Count; i++)
                {
                    _oList.Add(
                        new Transaction(
                        Convert.ToInt32(_table.Rows[i][0]),
                        Convert.ToString(_table.Rows[i][1]),
                        Convert.ToString(_table.Rows[i][2]),
                        Convert.ToDouble(_table.Rows[i][3]),
                        Convert.ToDateTime(_table.Rows[i][4]))
                        );
                }


            }
            catch (GeneralException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return _oList;
        }
    }
}
