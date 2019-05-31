using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo5;
using Newtonsoft.Json;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo5
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoController:ControllerBase
    {
        /*https://localhost:5001/api/Auto/agregar/toyotas/corola/4/true/ac366df/23000/<aqui_va_la_foto>/1*/
        [HttpGet("agregar/{make}/{model}/{capacity}/{status}/{licence}/{price}/{picture}/{place}")]
        public ActionResult<IEnumerable<String>> agregar(string make,string model,int capacity,bool status, string licence, float price, string picture , int place)
        {
            try {
            Auto auto = new Auto(make,model,capacity,status,licence,price,picture,place);
            var result= ConnectAuto.Agregar(auto);
            return Ok(JsonConvert.SerializeObject(result));

            }catch (IndexOutOfRangeException)
            {
                return StatusCode(500,"no se registro carro");
            }
;
        }
        /*https://localhost:5001/api/Auto/eliminar/2*/
        [HttpGet("eliminar/{i}")]
        public IActionResult delete(int i)
        {
        
            var result= ConnectAuto.DeleteAuto(i);
              if (result.Equals(-1))
            {
                return StatusCode(500, "El automovil no existe");
            }
            return StatusCode(200, "Eliminado satisfactoriamente");
        
        }
        /*https://localhost:5001/api/Auto/consultarforall/2 
         result = isactive */ 
        [HttpGet("consultforall/{place}/{result}/{license}/{capacity}")]
        public  ActionResult<IEnumerable<Auto>> GetconsultAll(int place , string result , string license , int capacity)
        {
            try{
                
                List<Auto> AutoList =ConnectAuto.consultforall(place,result,license,capacity);
                 return Ok(JsonConvert.SerializeObject(AutoList));
            }catch (IndexOutOfRangeException)
            {
                return StatusCode(500,"No hay auto registrados");
            }
        }
         /*https://localhost:5001/api/Auto/consultforplace/1 */
        [HttpGet("consultforplace/{id}")]
        public  ActionResult<IEnumerable<Auto>> GetconsultPlace(int id)
        {
            try{
                
                List<Auto> AutoList =ConnectAuto.ConsultforPlace(id);
                 return Ok(JsonConvert.SerializeObject(AutoList));
            }catch (IndexOutOfRangeException)
            {
                return StatusCode(500,"No hay auto registrados");
            }
        }
        /*https://localhost:5001/api/Auto/consult/67 */
         [HttpGet("consult/{id}")]
        public  ActionResult<IEnumerable<Auto>> Getconsulforid(int id )
        {
            try{
                
                List<Auto> AutoList =ConnectAuto.ConsultforId(id);
                
                 return Ok(AutoList.ToList());
                 
            }catch (IndexOutOfRangeException)
            {
                return Ok("no hay autos registrados");
            }
        }
        /*https://localhost:5001/api/Auto/consultplaceStatus/1/true*/
        [HttpGet("consultplaceStatus/{place}/{status}")]
        public  ActionResult<IEnumerable<Auto>> GetConsultforPlaceandStatu(int place, bool status)
        {
            try{
                
                List<Auto> AutoList =ConnectAuto.ConsultforPlaceandStatu(place,status);
                 return Ok(JsonConvert.SerializeObject(AutoList));
            }catch (IndexOutOfRangeException)
            {
                return StatusCode(500,"No hay auto registrados");
            }
        }



		/*https://localhost:5001/api/Auto/modificar/2/cambio/cambio/532/false/cambio/12345/cambio/2 */
        [HttpGet("modificar/{id}/{make}/{model}/{capacity}/{status}/{licence}/{price}/{picture}/{place}")]
        public  IActionResult  modify(int id, string make,string model,int capacity,bool status, string licence, float price, string picture , int place)
        {
            try {
            Auto auto = new Auto(make,model,capacity,status,licence,price,picture,place);
            auto.setId(id);
            var result= ConnectAuto.ModifyAuto(auto);
            return Ok(JsonConvert.SerializeObject(result));

            }catch (IndexOutOfRangeException)
            {
                return StatusCode(500,"no se registro carro");
            }
        }
    }
}