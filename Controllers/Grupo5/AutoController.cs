using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.Entities.Grupo5;
using vacanze_back.Connection.Grupo5;
using Newtonsoft.Json;
namespace vacanze_back.Controllers.Grupo5
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoController:ControllerBase
    {
         /*https://localhost:5001/api/Auto/agregar/toyotas/corola/4/true/ac366df/23000/aqui_va_la_foto/1*/
        [HttpGet("agregar/{make}/{model}/{capacity}/{status}/{licence}/{price}/{picture}/{place}")]
      
       
        public ActionResult<IEnumerable<String>> agregar(string make,string model,int capacity,bool status, string licence, int price, string picture , int place)
        {
            ConnectAuto conec= new ConnectAuto();
            Auto auto = new Auto(make,model,capacity,status,licence,price,picture,place);
            conec.Agregar(auto);
            return new string[] { "success"  }; 
        }
           /*https://localhost:5001/api/Auto/eliminar/2 */
          [HttpGet("eliminar/{i}")]
       public IActionResult delete(int i)
        {
            try{
            ConnectAuto conec= new ConnectAuto();
            conec.DeleteAuto(i);
			 return Ok("hola"+i.ToString());
            }catch (Exception e )
            {            
		     return Ok(e);
            }
        }
        /*https://localhost:5001/api/Auto/consultar/2 */
        [HttpGet("consultar/{i}")]
       public  ActionResult<IEnumerable<Auto>> consult(int i)
        {
            try{
                ConnectAuto conec= new ConnectAuto();
                List<Auto> AutoList = conec.ConsultforId(i);
                Auto auto = AutoList[0];
                Console.WriteLine(auto.getId());
                return Ok(AutoList); 
            }catch (Exception e )
            {            
		     return Ok(e);
            }
        }
		/*https://localhost:5001/api/Auto/modificar/2/cambio/cambio/532/false/cambio/12345/cambio/2 */
        [HttpGet("modificar/{id}/{make}/{model}/{capacity}/{status}/{licence}/{price}/{picture}/{place}")]
       public  IActionResult  modify(int id, string make,string model,int capacity,bool status, string licence, int price, string picture , int place)
        {
            try{
                
                ConnectAuto conec= new ConnectAuto();
            
                Auto auto = new Auto(make,model,capacity,status,licence,price,picture,place);
                auto.setId(id);
                conec.ModifyAuto(auto);
                return Ok("hola se modifico la broma "); 
            }catch (Exception e )
            {            
		     return Ok(e);
            }
        }
        

    }
}