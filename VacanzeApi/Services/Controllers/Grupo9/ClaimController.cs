using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo9;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo9
{
[Produces("application/json")] 
	[Route("api/[controller]")]
	[EnableCors("MyPolicy")]
	[ApiController]
	public class ClaimController : ControllerBase
	{
        ResponseError mensaje;

		// GET api/values
		//se usara para consultar la cantidad de reclamos
		[HttpGet]
		public int Get()
		{
			try{ 
				ClaimRepository conec= new ClaimRepository();
				int rows= conec.GetClaim();
				return rows; 
			}catch (DatabaseException )
			{            
				return -1;
			}
			catch (InvalidStoredProcedureSignatureException )
			{
				return -1;
			}
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public ActionResult<IEnumerable<Claim>> Get(int id)
		{
			try{
				ClaimRepository conec= new ClaimRepository();
				List<Claim> claimList = conec.GetClaim(id);

				return claimList; 
			}catch (DatabaseException )
			{            
				return StatusCode(500);
			}
			catch (InvalidStoredProcedureSignatureException )
			{
				return StatusCode(500);
			}
		}
		
		[HttpGet("admin/{status}")]
		public ActionResult<IEnumerable<Claim>> GetStatus(string status)
		{
			try{
				ClaimRepository conec= new ClaimRepository();
				List<Claim> claimList = conec.GetClaimStatus(status);

				return claimList; 
			}catch (DatabaseException )
			{            
				return StatusCode(500);
			}
			catch (InvalidStoredProcedureSignatureException )
			{
				return StatusCode(500);
			}
		}


		// Post api/Claim/	
		[HttpPost("{id}")]
		public ActionResult<string> Post(int id, [FromBody] ClaimSecundary ClaimAux)
		{            
			try
			{ 				
				ClaimRepository conec= new ClaimRepository();
				Claim claim= new Claim(ClaimAux.title, ClaimAux.description);
				claim.Validate();
				claim.ValidatePost();
				conec.AddClaim(claim,id);
				return Ok("Agregado correctamente");
			}catch (DatabaseException )
			{            
				return StatusCode(500);
			}
			catch (InvalidStoredProcedureSignatureException )
			{
				return StatusCode(500);
			}
			catch (AttributeSizeException exc)
			{
				return StatusCode(500,exc.Message);
			}
			catch (AttributeValueException exc)
			{
				return StatusCode(500,exc.Message);
			}
		}
		
		// DELETE api/Claim/5
		[HttpDelete("{id}")]
		public ActionResult<string> Delete(int id)
		{
			try
			{
				ClaimRepository conec = new ClaimRepository();
				int rows = conec.DeleteClaim(id);
				return Ok("eliminado exitosamente");
			}
			catch (DatabaseException)
			{
				return StatusCode(500);
			}
			catch (InvalidStoredProcedureSignatureException)
			{
				return StatusCode(500);
			}
			catch (NullClaimException)
			{
				mensaje = new ResponseError();
				mensaje.error = "No existe el elemento que quiere Eliminar";
				return StatusCode(500, mensaje);
			}
		}
		
		//api/Clain/status/5
		[HttpPut("{id}")]
		public ActionResult<string> Put(int id,[FromBody] ClaimSecundary ClaimAux)
		{
			try{
				ClaimRepository conec = new ClaimRepository();
				Claim claim = new Claim(ClaimAux.title, ClaimAux.description, ClaimAux.status);
				claim.Validate();
				claim.ValidatePut();
				int rows= 0;
				if (ClaimAux.status != null)
					rows = conec.ModifyClaimStatus(id, claim);
				else if (ClaimAux.title != null && ClaimAux.description  != null)
					rows = conec.ModifyClaimTitle(id, claim);						
				return Ok("Modificado exitosamente");
			}catch (DatabaseException )
			{            
				return StatusCode(500);
			}
			catch (InvalidStoredProcedureSignatureException )
			{
				return StatusCode(500);
			}catch (NullClaimException )
			{
				mensaje= new ResponseError();
				mensaje.error="No existe el elemento que quiere Modificar";
				return StatusCode(500,mensaje);
			}
			catch (AttributeSizeException exc)
			{
				return StatusCode(500,exc.Message);
			}
			catch (AttributeValueException exc)
			{
				return StatusCode(500,exc.Message);
			}
		}


	}
public class ResponseError {
	public string error{get; set;}
	
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