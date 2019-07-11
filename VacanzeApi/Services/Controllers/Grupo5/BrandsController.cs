using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo5;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo5;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo5;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo5;
using Microsoft.Extensions.Logging;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo5 {

    [Produces ("application/json")]
    [Route ("api/[controller]")]
    [EnableCors ("MyPolicy")]
    [ApiController]
    public class BrandsController : ControllerBase {

        private readonly ILogger<BrandsController> _logger;

        public BrandsController(ILogger<BrandsController> logger)
        {
            _logger = logger;
        }

        ///<sumary>Creación de una nueva marca</sumary>
        ///<param name="brandDTO">DTO de marca</param>
        ///<returns>Success: id de la marca</returns>
        ///<returns>No Success: Mensaje de error</returns>
        [Consumes ("application/json")]
        [HttpPost]
        public IActionResult AddBrand ([FromBody] BrandDTO brandDTO) {
            _logger?.LogInformation($"Inicio del servicio: [Post] https://localhost:5001/api/brands ");
            try {
                BrandMapper brandMapper = MapperFactory.CreateBrandMapper();
                Entity entity = brandMapper.CreateEntity(brandDTO);
                _logger?.LogInformation($" Se transformó de DTO a Entity ");
                AddBrandCommand command = CommandFactory.CreateAddBrandCommand ((Brand) entity);
                _logger?.LogInformation($" Ejecución del comando ");
                command.Execute ();
                return Ok ("ok " + command.GetResult ());
            } catch(RequiredAttributeException ex){
                _logger?.LogWarning("Atributo requerido: " +    ex.Message);
                return StatusCode (400, ex.Message);
            } catch (UniqueAttributeException ex){
                _logger?.LogWarning(ex.Message);
                return StatusCode (500, ex.Message);
            } catch (InternalServerErrorException ex) {
                _logger?.LogError("Error: " + ex.Ex.Message);
                return StatusCode (500, ex.Message);
            } catch (Exception) {
                _logger?.LogError("Error inesperado");
                return StatusCode (400);
            }
        }

        ///<sumary>Obtener todas las marcas de vehiculos</sumary>
        ///<returns>Success: Lista de BrandDTOs</returns>
        ///<returns>No Success: Mensaje de error</returns>
        [HttpGet]
        public ActionResult<IEnumerable<BrandDTO>> GetBrands(){
            _logger?.LogInformation($"Inicio del servicio: [GET] https://localhost:5001/api/brands ");
            List<Brand> brands = new List<Brand>();
            try {
                BrandMapper brandMapper = MapperFactory.CreateBrandMapper();
                GetBrandsCommand command = CommandFactory.CreateGetBrandsCommand();
                _logger?.LogInformation($" Ejecución del comando ");
                command.Execute ();
                return Ok (brandMapper.CreateDTOList(command.GetResult()));
            } catch(WithoutExistenceOfBrandsException ex){
                _logger?.LogWarning($"No existen marcas en la base de datos");
                return StatusCode (404, ex.Message);
            } catch (InternalServerErrorException ex) {
                _logger?.LogError("Error: " + ex.Ex.Message);
                return StatusCode (500, ex.Message);
            } catch (Exception) {
                _logger?.LogError("Error inesperado");
                return StatusCode (400);
            }
        }

        ///<sumary>Obtener marcas de vehiculo por id</sumary>
        ///<param name="brandId">Id de la marca</param>
        ///<returns>Success: BrandDTO</returns>
        ///<returns>No Success: Mensaje de error</returns>
        [HttpGet("{brandId:int}")]
        public ActionResult<BrandDTO> GetBrandById(int brandId){
            _logger?.LogInformation($"Inicio del servicio: [GET] https://localhost:5001/api/brands/brandId ");
            try {
                GetBrandByIdCommand command = CommandFactory.CreateGetBrandByIdCommand(brandId);
                _logger?.LogInformation($" Ejecución del comando ");
                command.Execute ();
                BrandMapper brandMapper = MapperFactory.CreateBrandMapper();
                return Ok (brandMapper.CreateDTO(command.GetResult()));
            } catch (BrandNotFoundException ex){
                _logger?.LogWarning("La marca con Id : " + ex.BrandId + "no encontrado");
                return StatusCode(404,ex.Message + ex.BrandId);
            } catch (InternalServerErrorException ex) {
                _logger?.LogError("Error: " + ex.Ex.Message);
                return StatusCode (500, ex.Message);
            } catch (Exception) {
                _logger?.LogError("Error inesperado");
                return StatusCode (400);
            }
        }

        ///<sumary>Actualizar marca</sumary>
        ///<param name="brandDTO">DTO de la marca</param>
        ///<returns>Success: mensaje de ok</returns>
        ///<returns>No Success: Mensaje de error</returns>
        [Consumes ("application/json")]
        [HttpPut]
        public IActionResult UpdateBrand([FromBody] BrandDTO brandDTO){
            _logger?.LogInformation($"Inicio del servicio: [PUT] https://localhost:5001/api/brands/brandId ");
            try{
                BrandMapper brandMapper = MapperFactory.CreateBrandMapper();
                Entity entity = brandMapper.CreateEntity(brandDTO);
                _logger?.LogInformation($" Se transformó de DTO a Entity ");
                UpdateBrandCommand command = CommandFactory.CreateUpdateBrandCommand ((Brand) entity);
                _logger?.LogInformation($" Ejecución del comando ");
                command.Execute ();
                if(command.GetResult())
                    return Ok("La modificación fue realizada exitosamente");
                else
                    return StatusCode(400);
            } catch (RequiredAttributeException ex){
                _logger?.LogWarning("Atributo requerido: " +    ex.Message);
                return StatusCode (400, ex.Message);
            } catch (UniqueAttributeException ex){
                _logger?.LogWarning(ex.Message);
                return StatusCode (500, ex.Message);
            } catch (BrandNotFoundException ex){
                _logger?.LogWarning("La marca con Id : " + ex.BrandId + "no encontrado");
                return StatusCode (404, ex.Message + ex.BrandId);
            } catch(InternalServerErrorException ex){
                _logger?.LogError("Error: " + ex.Ex.Message);
                return StatusCode(500, ex.Message);
            } catch(Exception){
                _logger?.LogError("Error inesperado");
                return StatusCode(400);
            }
        }
    }
}