using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.Entities.Grupo5;
using vacanze_back.DAO.Grupo5;
using Newtonsoft.Json;
namespace vacanze_back.Controllers.Grupo5
{
    [Route("api/Grupo5")]
    [ApiController]
    public class AutoController:ControllerBase
    {
        
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<String>> Get()
        {
            DAOAuto conec= new DAOAuto();
            Auto auto = new Auto("hola","model",123,true,"licence",543);
            conec.Agregar(auto);
            return new string[] { "success"  }; 
        }

    }
}