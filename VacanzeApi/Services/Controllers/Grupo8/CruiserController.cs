using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo8;

namespace vacanze_back.Controllers.Grupo8
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class CruiserController : Controller
    {
        // GET/Cruisers
        [HttpGet]
//        public String[] GetCruisers()
//        {
//            return new [] {"Cruiser1", "Cruiser2"};
//        }
        // GET/Cruiser
        [HttpGet("{id}")]
        public IActionResult GetCruiser(int id)
        {
            try
            {
                 Cruiser cruiser=  CruiserConnection.GetCruiser(id);
                 return Ok(JsonConvert.SerializeObject(cruiser));
            }
            catch (IndexOutOfRangeException)
            {
                return StatusCode(500,"El Crusero no fue encontrado");
            }
        }

//        [HttpPost]
//        public Cruiser PostCruiser(Cruiser cruiser)
//        {
//    
//                
//        }
//        [HttpPut]
//        public Cruiser PutCruiser(Cruiser cruiser)
//        {
//            
////        }
//        [HttpDelete]
//        public Cruiser DeleteCruiser(int id)
//        {
//            
//        }
        
    }
}