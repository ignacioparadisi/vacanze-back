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
        ResponseError mensaje;

		// GET api/values/5
		[HttpGet("{id}")]
		public ActionResult<IEnumerable<Baggage>> Get(int id)
		{
			try{
				BaggageRepository conec= new BaggageRepository();
				List<Baggage> BaggageList = conec.GetBaggage(id);

				return BaggageList; 
			}catch (DatabaseException )
			{            
				return StatusCode(500);
			}
			catch (InvalidStoredProcedureSignatureException )
			{
				return StatusCode(500);
			}
		}

		// GET api/values/5
		// Get para la tabla equipaje
		[HttpGet("{tipo}/{id}")]
		public ActionResult<IEnumerable<Baggage>> Get(string tipo, int id)
		{
			try{
				if(tipo == "documentPasaport"){                   
					BaggageRepository conec= new BaggageRepository();
					List<Baggage> BaggageList = conec.GetBaggageDocumentPasaport(id);
					return Ok(BaggageList); 
				}else{
					if(tipo == "documentCedula"){                   
					BaggageRepository conec= new BaggageRepository();
					List<Baggage> BaggageList = conec.GetBaggageDocumentCedula(id);
					return Ok(BaggageList); 
				}else                
					return StatusCode(404);
				}                            
			}catch (DatabaseException )
			{            
				return StatusCode(500);
			}
			catch (InvalidStoredProcedureSignatureException )
			{
				return StatusCode(500);
			}
		}
		[HttpGet("admin/getStatus/{status}")]
		public ActionResult<IEnumerable<Baggage>> GetStatus(string status)
		{
			try{
				BaggageRepository conec= new BaggageRepository();
				List<Baggage> BaggageList = conec.GetBaggageStatus(status);

				return BaggageList; 
			}catch (DatabaseException )
			{            
				return StatusCode(500);
			}
			catch (InvalidStoredProcedureSignatureException )
			{
				return StatusCode(500);
			}
		}

		//api/Clain/status/5
		[HttpPut("{id}")]
		public ActionResult<string> Put(int id,[FromBody] Baggage Baggage)
		{
			try{
				BaggageRepository conec = new BaggageRepository();
				int rows= 0;
				if (Baggage._status != null)
					rows = conec.ModifyBaggageStatus(id, Baggage);		
				else
					throw new NullBaggageException("no contiene un status");				
				return Ok("Modificado exitosamente");
			}catch (DatabaseException )
			{            
				return StatusCode(500);
			}
			catch (InvalidStoredProcedureSignatureException )
			{
				return StatusCode(500);
			}catch (NullBaggageException )
			{
				mensaje= new ResponseError();
				mensaje.error="No existe el elemento que quiere Modificar";
				return StatusCode(500,mensaje);
			}
		}


	}
}