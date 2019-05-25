using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.Entities.Grupo9;
using vacanze_back.Connection.Grupo9;
using vacanze_back.Controllers.Grupo9;
using System.Net;
using System.Web.Http;
using Microsoft.AspNetCore.Cors;

namespace vacanze_back.Controllers.Grupo9
{
    [Produces("application/json")] 
    [Route("api/[controller]")]
	 [EnableCors("MyPolicy")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        

        // GET api/values
		//se usara para consultar por pasaporte
        [HttpGet]
        public ActionResult<IEnumerable<Claim>> Get()
        {

			ClaimConnection conec= new ClaimConnection();
            List<Claim> claimList= conec.GetClaim(79);
            return Ok(claimList); 
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Claim>> Get(int id)
        {
           ClaimConnection conec= new ClaimConnection();
            List<Claim> claimList = conec.GetClaim(id);

            return Ok(claimList); 
        }

        // Post api/Claim/
	
        [HttpPost]
        public ActionResult<string> Post([FromBody] Reclamito ClaimAux)
        {            
            try
            { 
				
                ClaimConnection conec= new ClaimConnection();
				Claim claim= new Claim(ClaimAux.titulo, ClaimAux.descripcion, ClaimAux.status);
				conec.AddClaim(claim);
				return Ok("Agregado correctamente");
            }
            catch (Exception ex)
            { 
				return StatusCode(500);
            }
        }
		
        // DELETE api/Claim/5
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            try{
                Console.WriteLine("estoy aqui");
            ClaimConnection conec= new ClaimConnection();
                
                Console.WriteLine("estoy aqui");
                conec.DeleteClaim(id);

                Console.WriteLine("estoy aqui1");
				return Ok("Eliminado exitosamente");
            }
            catch (Exception ex)
            {
				return StatusCode(500);
            } 
        }
		
		//api/Claim/status/5
		[HttpPut("{tipo}/{id}")]
		public ActionResult<string> Put(string tipo,int id,[FromBody] Reclamito ClaimAux)
        {
            try{
            ClaimConnection conec= new ClaimConnection();
				Claim claim = new Claim(ClaimAux.titulo, ClaimAux.descripcion,ClaimAux.status);
                Console.WriteLine("estoy aqui");
				if(ClaimAux.status != null)
                conec.ModifyClaimStatus(id,claim);
				else if(ClaimAux.titulo != null)
					conec.ModifyClaimTitle(id,claim);
				return Ok("Modificado exitosamente");
            }
            catch (Exception ex)
            {
				return StatusCode(500);
            } 
        }


    }
}

public class Reclamito {
    public string titulo ;
    public string descripcion;
    public string status;
	public string getStatus(){
		return this.status;
		}
}