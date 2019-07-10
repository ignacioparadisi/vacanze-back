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


namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo5{
    
    [Produces ("application/json")]
    [Route ("api/[controller]")]
    [EnableCors ("MyPolicy")]
    [ApiController]
    public class ModelsController : ControllerBase{
        
        [Consumes ("application/json")]
        [HttpPost]
        public IActionResult AddModel ([FromBody] ModelDTO modelDTO) {
            try {
                ModelMapper modelMapper = MapperFactory.CreateModelMapper();
                Entity model = modelMapper.CreateEntity(modelDTO);
                AddModelCommand command = CommandFactory.CreateAddModelCommand((Model) model);
                command.Execute();
                return Ok ("ok " + command.GetResult ());
            } catch (RequiredAttributeException ex){
                return StatusCode (400, ex.Message);
            } catch (BrandNotFoundException ex){
                return StatusCode (404, ex.Message + ex.BrandId);
            } catch (UniqueAttributeException ex){
                return StatusCode (500, ex.Message);
            } catch (InternalServerErrorException ex) {
                return StatusCode (500, ex.Message);
            } catch (Exception) {
                return StatusCode (400);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Model>> GetModels(){
             List<Model> models = new List<Model>();
            try {
                GetModelsCommand command = CommandFactory.CreateGetModelsCommand();
                command.Execute ();
                ModelMapper modelMapper = MapperFactory.CreateModelMapper();
                return Ok (modelMapper.CreateDTOList(command.GetResult()));
            } catch (WithoutExistenceOfModelsException ex){
                return StatusCode (404, ex.Message);
            } catch (InternalServerErrorException ex) {
                return StatusCode (500, ex.Message);
            } catch (Exception) {
                return StatusCode (400);
            }
        }

        [HttpGet("~/api/brands/{brandId:int}/[controller]")]
        public ActionResult<IEnumerable<Model>> GetModelsByBrand(int brandId){
             List<Model> models = new List<Model>();
            try {
                GetModelsByBrandCommand command = CommandFactory.CreateGetModelsByBrandCommand(brandId);
                command.Execute ();
                ModelMapper modelMapper = MapperFactory.CreateModelMapper();
                return Ok (modelMapper.CreateDTOList(command.GetResult()));
            } catch (BrandNotFoundException ex){
                return StatusCode (404, ex.Message + ex.BrandId);
            } catch (WithoutExistenceOfModelsException ex){
                 return StatusCode (404, ex.Message);
            } catch (InternalServerErrorException ex) {
                return StatusCode (500, ex.Message);
            } catch (Exception) {
                return StatusCode (400);
            }
        }

        [HttpGet("{modelId:int}")]
        public ActionResult<ModelDTO> GetModelById(int modelId){
            try {
                GetModelByIdCommand command = CommandFactory.CreateGetModelByIdCommand(modelId);
                command.Execute ();
                ModelMapper modelMapper = MapperFactory.CreateModelMapper();
                return Ok (modelMapper.CreateDTO(command.GetResult()));
            } catch (ModelNotFoundException ex){
                return StatusCode (404, ex.Message + ex.ModelId);
            } catch (InternalServerErrorException ex) {
                return StatusCode (500, ex.Message);
            } catch (Exception) {
                return StatusCode (400);
            }
        }
        
        [Consumes ("application/json")]
        [HttpPut]
        public IActionResult UpdateModel([FromBody] ModelDTO modelDTO){
            try{
                ModelMapper modelMapper = MapperFactory.CreateModelMapper();
                Entity entity = modelMapper.CreateEntity(modelDTO);
                UpdateModelCommand command = CommandFactory.CreateUpdateModelCommand ((Model) entity);
                command.Execute ();
                if(command.GetResult())
                    return Ok("La modificación fue realizada exitosamente");
                else
                    return StatusCode(400);
            } catch (RequiredAttributeException ex){
                return StatusCode (400, ex.Message);
            } catch (ModelNotFoundException ex){
                return StatusCode (404, ex.Message + ex.ModelId);
            } catch (BrandNotFoundException ex){
                return StatusCode (404, ex.Message + ex.BrandId);
            } catch(UniqueAttributeException ex){
                return StatusCode (500, ex.Message);
            } catch(InternalServerErrorException ex){
                return StatusCode(500, ex.Message);
            } catch(Exception){
                return StatusCode(400);
            }
      
        }
    }
}