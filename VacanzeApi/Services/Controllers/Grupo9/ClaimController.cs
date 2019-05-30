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
        private ResponseError mensaje;
        /// <summary>
		// GET api/Claim
		//se usara para consultar la cantidad de reclamos en la base de datos
        /// </summary>
		[HttpGet]
		public int Get()
		{
			try
			{ 
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
        /// <summary>
		// GET api/claim/id
		// usado para consultar un claim segun id
		/// </summary>
		[HttpGet("{id}")]
		public ActionResult<IEnumerable<Claim>> Get(int id)
		{
			try
			{
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
		/// <summary>
		// GET api/claim/admin/status
		// usado para que el administrador consulte los reclamos por estatus
		/// </summary>
		[HttpGet("admin/{status}")]
		public ActionResult<IEnumerable<Claim>> GetStatus(string status)
		{
			try
			{
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
		/// <summary>
		// Post api/Claim/id
		//utilizado para crear un reclamo con una id del equipaje
		/// </summary>	
		[HttpPost("{id}")]
		public ActionResult<string> Post(int id, [FromBody] ClaimSecundary ClaimAux)
		{            
			try
			{ 				
				ClaimRepository conec= new ClaimRepository();
				Claim claim= new Claim(ClaimAux.title, ClaimAux.description, ClaimAux.status);
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
		}		
		/// <summary>
		// DELETE api/Claim/id
		//eliminar un reclamo
		/// </summary>
		[HttpDelete("{id}")]
		public ActionResult<string> Delete(int id)
		{
			try
			{
				ClaimRepository conec= new ClaimRepository();  
				int rows = conec.DeleteClaim(id);				 
				return Ok("eliminado exitosamente");
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
				mensaje.error="No existe el elemento que quiere Eliminar";
				return StatusCode(500,mensaje);
			}

		}		
		/// <summary>
		//api/Clain/status/id
		// modificar un reclamo , tanto por status o por titulo y descripcion
		/// </summary>
		[HttpPut("{id}")]
		public ActionResult<string> Put(int id,[FromBody] ClaimSecundary ClaimAux)
		{
			try{
				ClaimRepository conec = new ClaimRepository();
				Claim claim = new Claim(ClaimAux.title, ClaimAux.description, ClaimAux.status);
				int rows= 0;
				if (ClaimAux.status != null)
					rows = conec.ModifyClaimStatus(id, claim);
				else if (ClaimAux.title != null && ClaimAux.description  != null)
						rows = conec.ModifyClaimTitle(id, claim);	
					else throw new 	NullClaimException("claim vacio");				
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