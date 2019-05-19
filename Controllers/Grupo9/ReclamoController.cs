using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.Entities.Grupo9;
using vacanze_back.DAO.Grupo9;
using Newtonsoft.Json;
using vacanze_back.Controllers.Grupo9;


namespace vacanze_back.Controllers.Grupo9
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ReclamoController : ControllerBase
    {
        

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Reclamo>> Get()
        {

            DAOReclamo conec= new DAOReclamo();
            Reclamo reclamo = new Reclamo("tituloo" , "elias y jorge" , "ABIERTO");
            conec.AgregarReclamo(reclamo);

            List<Reclamo> reclamosList = conec.ObtenerReclamo(1);

            return Ok(JsonConvert.SerializeObject(reclamosList)); 
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            DAOReclamo conec= new DAOReclamo();
            List<Reclamo> reclamosList = conec.ObtenerReclamo(id);

            return Ok(JsonConvert.SerializeObject(reclamosList)); 
        }

        // Post api/Reclamo/
        [HttpPost]
        public void Post([FromBody] Reclamito reclamoAux)
        {            
            try
            {
                /*
                Reclamo reclamos= new Reclamo();
                var jsonFile = System.IO.File.ReadAllText("jsonFile.json");
                var teams = JsonConvert.DeserializeObject<List<Reclamo>>(jsonFile);
                teams.Add(reclamos);
                System.IO.File.WriteAllText("jsonFile.json",JsonConvert.SerializeObject(reclamos));*/

                DAOReclamo conec= new DAOReclamo();
                Reclamo reclamo = new Reclamo(reclamoAux.titulo, reclamoAux.descripcion, reclamoAux.status);
                conec.AgregarReclamo(reclamo);
            }
            catch (Exception ex)
            {
            }
        }

        // DELETE api/Reclamo/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try{
                Console.WriteLine("estoy aqui");
                DAOReclamo conec= new DAOReclamo();
                
                Console.WriteLine("estoy aqui");
                conec.EliminarReclamo(id);

                Console.WriteLine("estoy aqui1");
            }
            catch (Exception ex)
            {
            } 
        }


    }
}

public class Reclamito {
    public string titulo ;
    public string descripcion;
    public string status;
}