using System;
using System.Collections.Generic;
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
	public class BaggageController : ControllerBase
	{
		/// <summary>
		// GET api/values/id
		// consultar los equipajes segun su id
		/// </summary>
		[HttpGet("{id}")]
		public ActionResult<IEnumerable<Baggage>> Get(int id)
		{
			try
			{
				BaggageRepository conec= new BaggageRepository();
				List<Baggage> BaggageList = conec.GetBaggage(id);
				return BaggageList; 
			}catch (DatabaseException ex)
			{            
				return StatusCode(500,ex.Message);
			}
			catch (InvalidStoredProcedureSignatureException ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		/// <summary>
		// GET api/values/5
		// Get para la tabla equipaje segun su documento de identidad
		/// </summary>
		[HttpGet("documentPasaport/{id}")]
		public ActionResult<IEnumerable<Baggage>> Get(string id)
		{
			try
			{                 
				BaggageRepository conec= new BaggageRepository();
				List<Baggage> BaggageList = conec.GetBaggageDocumentPasaport(id);
				return Ok(BaggageList);                          
			}catch (DatabaseException ex)
			{            
				return StatusCode(500, ex.Message);
			}
			catch (InvalidStoredProcedureSignatureException ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		/// <summary>
		//consultar los equipajes segun su estatus
		/// </summary>
		[HttpGet("admin/getStatus/{status}")]
		public ActionResult<IEnumerable<Baggage>> GetStatus(string status)
		{
			try
			{
				BaggageRepository conec= new BaggageRepository();
				List<Baggage> BaggageList = conec.GetBaggageStatus(status);
				return BaggageList; 
			}catch (DatabaseException ex)
			{            
				return StatusCode(500,ex.Message);
			}
			catch (InvalidStoredProcedureSignatureException ex)
			{
				return StatusCode(500,ex.Message);
			}
		}
		/// <summary>
		//api/Clain/status/5
		// modificar el estatus del equipaje
		/// </summary>
		[HttpPut("{id}")]
		public ActionResult<string> Put(int id,[FromBody] Baggage Baggage)
		{
			try
			{
				BaggageRepository conec = new BaggageRepository();
				if (Baggage._status != null)
					conec.ModifyBaggageStatus(id, Baggage);		
				else
					throw new NullBaggageException("no contiene un status");				
				return Ok("Modificado exitosamente");
			}catch (DatabaseException ex )
			{            
				return StatusCode(500, ex.Message);
			}
			catch (InvalidStoredProcedureSignatureException ex )
			{
				return StatusCode(500, ex.Message);
			}catch (NullBaggageException ex)
			{
				return StatusCode(500,ex.Message);
			}
		}
	}
}