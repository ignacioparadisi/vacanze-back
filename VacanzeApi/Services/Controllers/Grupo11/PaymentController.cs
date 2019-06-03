using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.Entities.Grupo11;
using vacanze_back.VacanzeApi.Common.Entities.Grupo11;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo11;
using vacanze_back.VacanzeApi.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;

namespace vacanze_back.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        /// <summary>
        /// Get que consulta metodos de pagos precargados estaticamente
        /// </summary>
        /// <returns>lista de metodos de pagos</returns>
        [HttpGet]
        public ActionResult<List<Payment>> GetPayment_Method()
        {

            try
            {
                var lPayments = PaymentRepository.GetPaymentMethod();

                if (lPayments != null && lPayments.Count > 0)
                {
                    //Si la lista viene llena
                    return Ok(lPayments);
                }
                else
                {
                    return NotFound("No se encontró informacón");
                }



            }
            catch (DatabaseException e)
            {
                //Aqui va una excepcion personalizada
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }




        }


        /// <summary>
        /// Get que consulta metodos de pagos precargados estaticamente
        /// </summary>
        /// <param name="idAuto">Número identificador de la resenva del auto a pagar</param>
        /// <param name="idRes">Número identificador de la resenva del restaurante a pagar</param>
        /// <param name="idRoo">Número identificador de la resenva del hotel a pagar</param>
        /// <param name="idCru">Número identificador de la resenvar del crucero a pagar</param>
        /// <returns>lista de metodos de pagos</returns>
        [HttpGet]
        [Route("id/{idAuto}/{idRoo}/{idRes}/{idCru}")]
        public ActionResult<List<Payment>> GetPayment_Order(long idAuto, long idRoo, long idRes, long idCru)
        {

            try
            {
                var lPayments = PaymentRepository.GetInfoOrder(idAuto, idRoo, idRes, idCru);

                if (lPayments != null && lPayments.Count > 0)
                {
                    return Ok(lPayments);
                }
                else
                {
                    return NotFound("No se encontró informacón");
                }



            }
            catch (DatabaseException e)
            {
                //Aqui va una excepcion personalizada
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }




        }

        /// <summary>
        /// Post que permite añadir un pago exitoso al registro
        /// tambien simula pago dependiedno de su respuesta exitosa o no
        /// </summary>
        /// <param name="bill">Obejto de tipo factura</param>
        /// <returns> mensaje de respuesta</returns>
        [HttpPost]
        [Route("")]
        public ActionResult<List<Payment>> AddPayment([FromBody] Bill bill)
        {

            try
            {
                var oResp = PaymentRepository.PayProcessResponse();

                if (oResp.Item1 == 0)
                {
                    var id = PaymentRepository.AddPayment(bill);
                    if (id > 0)
                    {
                        return Ok("Se ha registrado con exito el pago nro " + id.ToString());

                    }
                    else
                    {
                        return NotFound("Ocurrio un error insertando el pago");
                    }

                }
                else
                {
                    return StatusCode(StatusCodes.Status402PaymentRequired, oResp.Item2);
                }

            }
            catch (DatabaseException e)
            {
                //Aqui va una excepcion personalizada
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }




        }

        /// <summary>
        /// Ruta que simula procesamiento de pago
        /// </summary>
        /// <returns>aprobado o causa de rechazo</returns>
        [HttpGet]
        [Route("Procces")]
        public ActionResult<List<Payment>> GetProccesPayment()
        {

            try
            {
                var oResp = PaymentRepository.PayProcessResponse();

                if (oResp.Item1 == 0)
                {

                    return Ok(oResp);
                }
                else
                {
                    return StatusCode(StatusCodes.Status402PaymentRequired, oResp);
                }

            }
            catch (DatabaseException e)
            {
                //Aqui va una excepcion personalizada
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }




        }


        /// <summary>
        /// Devuelve habitaciones y autos no pagados
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tipo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ResHabAuto/{id}/{tipo}")]
        public ActionResult<List<PayRes>> GetProccesPayment(long id, int tipo)
        {

            try
            {
                var lPayments = PaymentRepository.GetPayRes(id, tipo);

                if (lPayments != null && lPayments.Count > 0)
                {
                    return Ok(lPayments);
                }
                else
                {
                    return NotFound("No se encontró informacón");
                }
            }
            catch (DatabaseException e)
            {
                //Aqui va una excepcion personalizada
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }

        }

        [HttpGet]
        [Route("Mypayment/{id}")]
        public ActionResult<List<Transaction>> GetpaymentTransact(long id)
        {

            try
            {
                var lPayments = PaymentRepository.GetTransaction(id);

                if (lPayments != null && lPayments.Count > 0)
                {
                    return Ok(lPayments);
                }
                else
                {
                    return NotFound("No se encontró informacón");
                }



            }
            catch (DatabaseException e)
            {
                //Aqui va una excepcion personalizada
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }




        }



    }
}
