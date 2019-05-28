using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo4;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.Connection.Grupo4;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo4
{
     [Produces("application/json")]
     [Route("api/[controller]")]
     [EnableCors("MyPolicy")]
     [ApiController]
     public class BaggageController : ControllerBase
     {


          // GET api/values
          //se usara para consultar por pasaporte
          [HttpGet]
          public ActionResult<IEnumerable<Baggage>> GetBaggage()
          {

               var baggage = new List<Baggage>();

               try
               {
                    baggage = BaggageConnection.GetBaggage();
               }
               catch (DatabaseException)
               {
                    return BadRequest("Error obteniendo los empleados");
               }
               return baggage;

          }




     }

     public class BaggageSecundary
     {

          private string MaletaId;
          private string MaletaFkVuelo;
          private string MaletaFkCrucero;
          private string MaletaStatus;
          private string CantidadPasajeros;
          private string Descripcion;


     }
}