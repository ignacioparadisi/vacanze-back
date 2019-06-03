using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo5;
using Newtonsoft.Json;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo5
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class AutoController:ControllerBase
    {
        /*https://localhost:5001/api/Auto/agregar/toyotas/corola/4/true/ac366df/23000/1*/
        [HttpGet("agregar/{make}/{model}/{capacity}/{status}/{licence}/{price}/{place}")]
        public ActionResult<IEnumerable<String>> agregar(string make,string model,int capacity,bool status,string licence,float price,int place)
        {
            try 
            {
                Auto auto = new Auto(make,model,capacity,status,licence,price,place);
                var result= ConnectAuto.Agregar(auto);
                return Ok("auto registrado exitosamente");
            }
            catch (DatabaseException )
			{            
				return StatusCode(500,"no se pudo registrar el automovil {DB}");
			}
			catch (InvalidStoredProcedureSignatureException )
			{
				return StatusCode(500,"no se pudo registrar el automovil {ISPS}");
			}
            catch (IndexOutOfRangeException)
            {
                return StatusCode(500,"no se pudo registrar el automovil {IOoR}");
            }
            
        }
        /*https://localhost:5001/api/Auto/eliminar/2*/
        [HttpGet("eliminar/{i}")]
        public IActionResult delete(int i)
        {
            try 
            {
                var result= ConnectAuto.DeleteAuto(i);
                return Ok(new { Message = "eliminado exitosamente " });
            } 
            catch (DatabaseException )
            {
                /*List<Auto> AutoList =ConnectAuto.ConsultforId(i);
                if (AutoList.Rows.Count < 1){return StatusCode(500,"no se pudo eliminar el automovil {DB}");}
                else{*/return StatusCode(500,"no se pudo eliminar el automovil {DB}");//}
            }
            catch (InvalidStoredProcedureSignatureException )
			{
				return StatusCode(500,"no se pudo eliminar el automovil {ISPS}");
            }

        }
        /*https://localhost:5001/api/Auto/getforall/2 
         result = isactive */ 
        [HttpGet("getforall/{place}/{result}/{license}/{capacity}")]
        public  ActionResult<IEnumerable<Auto>> GetconsultAll(int place , string result , string license , int capacity)
        {
            try
            { 
                List<Auto> AutoList =ConnectAuto.getforall(place,result,license,capacity);
                return Ok(AutoList.ToList());
            }
            catch (IndexOutOfRangeException )
            {
                return StatusCode(500,"No hay automoviles registrados {cfa-ISPS}");
            }
            catch (DatabaseException )
            {
                return StatusCode(500,"No hay automoviles registrados {cfa-ISPS}");
            }
            catch (InvalidStoredProcedureSignatureException )
            {
                return StatusCode(500,"No hay automoviles registrados {cfa-ISPS}");
            }
        }
        /*https://localhost:5001/api/Auto/consultforplace/1 */
        [HttpGet("consultforplace/{id}")]
        public  ActionResult<IEnumerable<Auto>> GetconsultPlace(int id)
        {
            try
            {    
                List<Auto> AutoList =ConnectAuto.ConsultforPlace(id);
                return Ok(AutoList.ToList());
            }
            catch (IndexOutOfRangeException )
            {
                return StatusCode(500,"No hay automoviles registrados {cfp-ISPS}");
            }
            catch (DatabaseException )
            {
                return StatusCode(500,"No hay automoviles registrados {cfp-ISPS}");
            }
            catch (InvalidStoredProcedureSignatureException )
            {
                return StatusCode(500,"No hay automoviles registrados {cfp-ISPS}");
            }
        }
        /*https://localhost:5001/api/Auto/consult/67 */
         [HttpGet("consult/{id}")]
        public  ActionResult<IEnumerable<Auto>> Getconsulforid(int id )
        {
            try
            {    
                List<Auto> AutoList =ConnectAuto.ConsultforId(id);
                return Ok(AutoList.ToList());    
            }
            catch (IndexOutOfRangeException )
            {
                return StatusCode(500,"No hay automoviles registrados {c-DB}");
            }
            catch (DatabaseException )
            {
                return StatusCode(500,"No hay automoviles registrados {c-DB}");
            }
            catch (InvalidStoredProcedureSignatureException )
            {
                return StatusCode(500,"No hay automoviles registrados {c-ISPS}");
            }
        }
        /*https://localhost:5001/api/Auto/consultplaceStatus/1/true*/
        [HttpGet("consultplaceStatus/{place}/{status}")]
        public  ActionResult<IEnumerable<Auto>> GetConsultforPlaceandStatu(int place, bool status)
        {
            try
            {    
                List<Auto> AutoList =ConnectAuto.ConsultforPlaceandStatu(place,status);
                return Ok(AutoList.ToList());
            }
            catch (IndexOutOfRangeException )
            {
                return StatusCode(500,"No hay automoviles registrados {cps-IOoR}");
            }
            catch (DatabaseException )
            {
                return StatusCode(500,"No hay automoviles registrados {cps-DB}");
            }
            catch (InvalidStoredProcedureSignatureException )
            {
                return StatusCode(500,"No hay automoviles registrados {cps-ISPS}");
            }
        }
		/*https://localhost:5001/api/Auto/modificar/2/cambio/cambio/532/false/cambio/12345/2 */
        [HttpGet("modificar/{id}/{make}/{model}/{capacity}/{status}/{licence}/{price}/{place}")]
        public  IActionResult  modify(int id,string make,string model,int capacity,bool status,string licence,float price,int place)
        {
            try 
            {
                Auto auto = new Auto(make,model,capacity,status,licence,price,place);
                auto.setId(id);
                var result= ConnectAuto.ModifyAuto(auto);
                return Ok();
            }
            catch (IndexOutOfRangeException )
            {
                return StatusCode(500,"no se pudo modificar el automovil {IOoR}");
            }
            catch (DatabaseException )
            {
                return StatusCode(500,"no se pudo modificar el automovil {DB}");
            }
			catch (InvalidStoredProcedureSignatureException )
			{
				return StatusCode(500,"no se pudo modificar el automovil {ISPS}");
			}
        }


        [HttpGet("getcity")]
        public  ActionResult<IEnumerable<Location>> GetCity( )
        {
            try
            {    
                List<Location> locationList =ConnectAuto.GetCity();
                return Ok(locationList.ToList());    
            }
            catch (IndexOutOfRangeException )
            {
                return StatusCode(500,"No hay ciudades guardadas - error de conexion con las ciudades {IOoR}");
            }
            catch (DatabaseException )
            {
                return StatusCode(500,"No hay ciudades guardadas - error de conexion con las ciudades {DB}");
            }
        }
    }
}