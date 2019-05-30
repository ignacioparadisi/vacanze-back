using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo4;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.Grupo4;

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
          public string Get()
          {
               return "hola";
          }

          // GET api/values/5
          [HttpGet("{id}")]
          public ActionResult<string> Get(int id)
          {
               return "momom::" + id;
          }

          // GET api/values/5
          // Get para la tabla equipaje
          [HttpGet("{tipo}/{id}")]
          public ActionResult<string> Get(string tipo, int id)
          {
               return "tipo::" + tipo + id;
          }
          // Post api/Claim/

          [HttpPost]
          public ActionResult<string> Post([FromBody] string prueba)
          {
               return "exito " + prueba;

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