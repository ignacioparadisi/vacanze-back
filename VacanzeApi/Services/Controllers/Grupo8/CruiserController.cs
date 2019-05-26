using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using vacanze_back.Entities.Grupo8;

namespace vacanze_back.Controllers.Grupo8
{
    [Route("api/[controller]")]
    [ApiController]
    public class CruiserController : Controller
    {
        // GET/Cruisers
        [HttpGet]
        public String[] GetCruisers()
        {
            return new [] {"Cruiser1", "Cruiser2"};
        }
        // GET/Cruiser
        [HttpGet("{id}")]
        public String GetCruiser(int id)
        {
            Cruiser c = new Cruiser("concordia", 5,5 );
            
            return JsonConvert.SerializeObject(c);
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