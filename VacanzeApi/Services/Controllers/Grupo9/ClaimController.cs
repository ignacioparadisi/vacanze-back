using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.Connection.Grupo9;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo9
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
			try{ 
				ClaimConnection conec= new ClaimConnection();
				List<Claim> claimList= conec.GetClaim(2);
				return claimList; 
			}catch (DatabaseException )
			{            
				return StatusCode(500);
			}
			catch (GeneralException )
			{
				return StatusCode(500);
			}
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public ActionResult<IEnumerable<Claim>> Get(int id)
		{
			try{
				ClaimConnection conec= new ClaimConnection();
				List<Claim> claimList = conec.GetClaim(id);

				return claimList; 
			}catch (DatabaseException )
			{            
				return StatusCode(500);
			}
			catch (GeneralException )
			{
				return StatusCode(500);
			}
		}

		// GET api/values/5
		// Get para la tabla equipaje
		[HttpGet("{tipo}/{id}")]
		public ActionResult<IEnumerable<Claim>> Get(string tipo, int id)
		{
			try{
				if(tipo == "Baggage"){
					ClaimConnection conec= new ClaimConnection();
					List<Claim> claimList = conec.GetClaimBaggage(id);
					return Ok(claimList); 
				}else{
					if(tipo == "documentPasaport"){                   
						ClaimConnection conec= new ClaimConnection();
						List<Claim> claimList = conec.GetClaimDocumentPasaport(id);
						return Ok(claimList); 
					}else{
						if(tipo == "documentCedula"){                   
							ClaimConnection conec= new ClaimConnection();
							List<Claim> claimList = conec.GetClaimDocumentCedula(id);
							return Ok(claimList); 
						}else                
							return StatusCode(404);
					}                            
				}
			}catch (DatabaseException )
			{            
				return StatusCode(500);
			}
			catch (GeneralException )
			{
				return StatusCode(500);
			}
		}
		// Post api/Claim/
	
		[HttpPost]
		public ActionResult<string> Post([FromBody] ClaimSecundary ClaimAux)
		{            
			try
			{ 				
				ClaimConnection conec= new ClaimConnection();
				Claim claim= new Claim(ClaimAux.title, ClaimAux.description, ClaimAux.status);
				conec.AddClaim(claim);
				return Ok("Agregado correctamente");
			}catch (DatabaseException )
			{            
				return StatusCode(500);
			}
			catch (GeneralException )
			{
				return StatusCode(500);
			}

		}
		
		// DELETE api/Claim/5
		[HttpDelete("{id}")]
		public ActionResult<string> Delete(int id)
		{
			try{
				ClaimConnection conec= new ClaimConnection();  
				conec.DeleteClaim(id);
				return Ok("Eliminado exitosamente");
			}catch (DatabaseException )
			{            
				return StatusCode(500);
			}
			catch (GeneralException )
			{
				return StatusCode(500);
			}

		}
		
		//api/Clain/status/5
		[HttpPut("{id}")]
		public ActionResult<string> Put(int id,[FromBody] ClaimSecundary ClaimAux)
		{
			try{
				ClaimConnection conec = new ClaimConnection();
				Claim claim = new Claim(ClaimAux.title, ClaimAux.description, ClaimAux.status);
				Console.WriteLine("estoy aqui");
				if (ClaimAux.status != null)
					conec.ModifyClaimStatus(id, claim);
				else if (ClaimAux.title != null && ClaimAux.description  != null)
					conec.ModifyClaimTitle(id, claim);
				return Ok("Modificado exitosamente");
			}catch (DatabaseException )
			{            
				return StatusCode(500);
			}
			catch (GeneralException )
			{
				return StatusCode(500);
			} 
		}


	}

	public class ClaimSecundary {
		public string title {get; set;} 
		public string description{get; set;}
		public string status{get; set;}
		public string getStatus(){
			return this.status;
		}
	}
}