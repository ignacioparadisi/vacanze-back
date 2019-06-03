using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo4;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo4;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo4
{
     [Produces("application/json")]
     [Route("api/[controller]")]
     [EnableCors("MyPolicy")]
     [ApiController]
     public class BaggageController : ControllerBase
     {


          //Metodo para obtener todos los equipajes
          [HttpGet]
          public ActionResult<IEnumerable<Baggage>> Get()
          {
               var Baggages = new List<Baggage>();
               try
               {
                    Baggages = BaggageRepository.GetBaggage();
               }
               catch (DatabaseException e)
               {

                    return BadRequest("Error al Obtener los equipajes" + e);
               }

               return Baggages;
          }


          // Metodo para eliminar equipajes
          [HttpGet("{tipo}/{id}")]
          public ActionResult<string> Get(string tipo, int id)
          {

               try
               {
                    BaggageRepository.DeleteBaggage(id);
               }
               catch (DatabaseException e)
               {

                    return BadRequest("Error al Eliminar equipaje" + e);
               }

               return "exito  en eliminar equipaje :" + id;

          }


     }

}