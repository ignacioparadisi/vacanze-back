using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.Entities.Grupo9;
using vacanze_back.DAO.Grupo9;
using Newtonsoft.Json;

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
            conec.Agregar(reclamo);

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
        public void Post([FromBody] string  titulo, string descripcion, string status)
        {
        Console.WriteLine("World!");
            try
            {
                DAOReclamo conec= new DAOReclamo();
                Reclamo reclamo = new Reclamo(titulo, descripcion,status );
                conec.Agregar(reclamo);
            }
            catch (Exception ex)
            {
            }
        }


    }
}