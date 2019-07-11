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


namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo5{
    
    [Produces ("application/json")]
    [Route ("api/[controller]")]
    [EnableCors ("MyPolicy")]
    [ApiController]
    public class ModelsController : ControllerBase{

        private readonly ILogger<ModelsController> _logger;

        public ModelsController(ILogger<ModelsController> logger)
        {
            _logger = logger;
        }
        
        ///<sumary>Creación de un nuevo modelo</sumary>
        ///<param name="modelDTO">DTO de modelo</param>
        ///<returns>Success: id del modelo</returns>
        ///<returns>No Success: Mensaje de error</returns>
        [Consumes ("application/json")]
        [HttpPost]
        public IActionResult AddModel ([FromBody] ModelDTO modelDTO) {
            _logger?.LogInformation($"Inicio del servicio: [Post] https://localhost:5001/api/models ");
            try {
                ModelMapper modelMapper = MapperFactory.CreateModelMapper();
                Entity model = modelMapper.CreateEntity(modelDTO);
                _logger?.LogInformation($" Se transformó de DTO a Entity ");
                AddModelCommand command = CommandFactory.CreateAddModelCommand((Model) model);
                _logger?.LogInformation($" Ejecución del comando ");
                command.Execute();
                return Ok ("ok " + command.GetResult ());
            } catch (RequiredAttributeException ex){
                _logger?.LogWarning("Atributo requerido: " +    ex.Message);
                return StatusCode (400, ex.Message);
            } catch (BrandNotFoundException ex){
                _logger?.LogWarning("La marca con Id : " + ex.BrandId + "no encontrado");
                return StatusCode (404, ex.Message + ex.BrandId);
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

        ///<sumary>Obtener todos los modelos</sumary>
        ///<returns>Success: Lista de Modelos</returns>
        ///<returns>No Success: Mensaje de error</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Model>> GetModels(){
            _logger?.LogInformation($"Inicio del servicio: [GET] https://localhost:5001/api/models ");
             List<Model> models = new List<Model>();
            try {
                GetModelsCommand command = CommandFactory.CreateGetModelsCommand();
                _logger?.LogInformation($" Ejecución del comando ");
                command.Execute ();
                ModelMapper modelMapper = MapperFactory.CreateModelMapper();
                return Ok (modelMapper.CreateDTOList(command.GetResult()));
            } catch (WithoutExistenceOfModelsException ex){
                _logger?.LogWarning($"No existen modelos en la base de datos");
                return StatusCode (404, ex.Message);
            } catch (InternalServerErrorException ex) {
                _logger?.LogError("Error: " + ex.Ex.Message);
                return StatusCode (500, ex.Message);
            } catch (Exception) {
                _logger?.LogError("Error inesperado");
                return StatusCode (400);
            }
        }

        ///<sumary>Obtener modelos de una marca</sumary>
        ///<param name="brandId">Id de la marca</param>
        ///<returns>Success: Lista de Marcas</returns>
        ///<returns>No Success: Mensaje de error</returns>
        [HttpGet("~/api/brands/{brandId:int}/[controller]")]
        public ActionResult<IEnumerable<Model>> GetModelsByBrand(int brandId){
            _logger?.LogInformation($"Inicio del servicio: [GET] https://localhost:5001/api/brands/brandId/models ");
             List<Model> models = new List<Model>();
            try {
                GetModelsByBrandCommand command = CommandFactory.CreateGetModelsByBrandCommand(brandId);
                _logger?.LogInformation($" Ejecución del comando ");
                command.Execute ();
                ModelMapper modelMapper = MapperFactory.CreateModelMapper();
                return Ok (modelMapper.CreateDTOList(command.GetResult()));
            } catch (BrandNotFoundException ex){
                _logger?.LogWarning("La marca con Id : " + ex.BrandId + "no encontrado");
                return StatusCode (404, ex.Message + ex.BrandId);
            } catch (WithoutExistenceOfModelsException ex){
                _logger?.LogWarning($"No existen modelos de vehiculos para esta marca");
                 return StatusCode (404, ex.Message);
            } catch (InternalServerErrorException ex) {
                _logger?.LogError("Error: " + ex.Ex.Message);
                return StatusCode (500, ex.Message);
            } catch (Exception) {
                _logger?.LogError("Error inesperado");
                return StatusCode (400);
            }
        }

        ///<sumary>Obtener modelo por su Id</sumary>
        ///<param name="modelId">Id del modelo</param>
        ///<returns>Success: ModelDTO</returns>
        ///<returns>No Success: Mensaje de error</returns>
        [HttpGet("{modelId:int}")]
        public ActionResult<ModelDTO> GetModelById(int modelId){
            _logger?.LogInformation($"Inicio del servicio: [GET] https://localhost:5001/api/models/modelId ");
            try {
                GetModelByIdCommand command = CommandFactory.CreateGetModelByIdCommand(modelId);
                _logger?.LogInformation($" Ejecución del comando ");
                command.Execute ();
                ModelMapper modelMapper = MapperFactory.CreateModelMapper();
                return Ok (modelMapper.CreateDTO(command.GetResult()));
            } catch (ModelNotFoundException ex){
                 _logger?.LogWarning("Modelo con Id : " + ex.ModelId + "no encontrado");
                return StatusCode (404, ex.Message + ex.ModelId);
            } catch (InternalServerErrorException ex) {
                _logger?.LogError("Error: " + ex.Ex.Message);
                return StatusCode (500, ex.Message);
            } catch (Exception) {
                _logger?.LogError("Error inesperado");
                return StatusCode (400);
            }
        }
        
        ///<sumary>Actualizar un modelo</sumary>
        ///<param name="modelDTO">DTO de modelo</param>
        ///<returns>Success: mensaje de ok</returns>
        ///<returns>No Success: Mensaje de error</returns>
        [Consumes ("application/json")]
        [HttpPut]
        public IActionResult UpdateModel([FromBody] ModelDTO modelDTO){
            _logger?.LogInformation($"Inicio del servicio: [PUT] https://localhost:5001/api/models ");
            try{
                ModelMapper modelMapper = MapperFactory.CreateModelMapper();
                Entity entity = modelMapper.CreateEntity(modelDTO);
                _logger?.LogInformation($" Se transformó de DTO a Entity ");
                UpdateModelCommand command = CommandFactory.CreateUpdateModelCommand ((Model) entity);
                _logger?.LogInformation($" Ejecución del comando ");
                command.Execute ();
                if(command.GetResult())
                    return Ok("La modificación fue realizada exitosamente");
                else
                    return StatusCode(400);
            } catch (RequiredAttributeException ex){
                _logger?.LogWarning("Atributo requerido: " +    ex.Message);
                return StatusCode (400, ex.Message);
            } catch (ModelNotFoundException ex){
                _logger?.LogWarning("Modelo con Id : " + ex.ModelId + "no encontrado");
                return StatusCode (404, ex.Message + ex.ModelId);
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