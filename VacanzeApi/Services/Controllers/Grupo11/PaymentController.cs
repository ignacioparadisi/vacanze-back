using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.Entities.Gripo11;

namespace vacanze_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<List<Payment>> GetPayment_Method()
        {
            var oResp = new List<Payment>();

            oResp.Add(new Payment { id = 1, name = "TDC", active = true });
            oResp.Add(new Payment { id = 2, name = "TRANSFERENCIA", active = true });
            oResp.Add(new Payment { id = 3, name = "TDB", active = true });
            oResp.Add(new Payment { id = 4, name = "TPAGO", active = true });
            oResp.Add(new Payment { id = 5, name = "Efectivo", active = true });

            Response.StatusCode = 200;
            return oResp;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
