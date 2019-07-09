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

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo5 {

    [Produces ("application/json")]
    [Route ("api/[controller]")]
    [EnableCors ("MyPolicy")]
    [ApiController]
    public class BrandsController : ControllerBase {

        [Consumes ("application/json")]
        [HttpPost]
        public IActionResult AddBrand ([FromBody] BrandDTO brandDTO) {
            try {
                BrandMapper brandMapper = MapperFactory.CreateBrandMapper();
                Entity entity = brandMapper.CreateEntity(brandDTO);
                AddBrandCommand command = CommandFactory.CreateAddBrandCommand ((Brand) entity);
                command.Execute ();
                return Ok ("ok " + command.GetResult ());
            } catch(RequiredAttributeException ex){
                return StatusCode (400, ex.Message);
            } catch (UniqueAttributeException ex){
                return StatusCode (500, ex.Message);
            } catch (InternalServerErrorException ex) {
                return StatusCode (500, ex.Message);
            } catch (Exception) {
                return StatusCode (400);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Brand>> GetBrands(){
            List<Brand> brands = new List<Brand>();
            try {
                BrandMapper brandMapper = MapperFactory.CreateBrandMapper();
                GetBrandsCommand command = CommandFactory.CreateGetBrandsCommand();
                command.Execute ();
                return Ok (brandMapper.CreateDTOList(command.GetResult()));
            } catch(WithoutExistenceOfBrandsException ex){
                return StatusCode (404, ex.Message);
            } catch (InternalServerErrorException ex) {
                return StatusCode (500, ex.Message);
            } catch (Exception) {
                return StatusCode (400);
            }
        }

        [HttpGet("{brandId:int}")]
        public ActionResult<Brand> GetBrandById(int brandId){
            try {
                GetBrandByIdCommand command = CommandFactory.CreateGetBrandByIdCommand(brandId);
                command.Execute ();
                BrandMapper brandMapper = MapperFactory.CreateBrandMapper();
                return Ok (brandMapper.CreateDTO(command.GetResult()));
            } catch (BrandNotFoundException ex){
                return StatusCode(404,ex.Message + ex.BrandId);
            } catch (InternalServerErrorException ex) {
                return StatusCode (500, ex.Message);
            } catch (Exception) {
                return StatusCode (400);
            }
        }

        [Consumes ("application/json")]
        [HttpPut]
        public IActionResult UpdateBrand([FromBody] BrandDTO brandDTO){
            try{
                BrandMapper brandMapper = MapperFactory.CreateBrandMapper();
                Entity entity = brandMapper.CreateEntity(brandDTO);
                UpdateBrandCommand command = CommandFactory.CreateUpdateBrandCommand ((Brand) entity);
                command.Execute ();
                if(command.GetResult())
                    return Ok("La modificación fue realizada exitosamente");
                else
                    return StatusCode(400);
            } catch (RequiredAttributeException ex){
                return StatusCode (400, ex.Message);
            } catch (UniqueAttributeException ex){
                return StatusCode (500, ex.Message);
            } catch (BrandNotFoundException ex){
                return StatusCode (404, ex.Message + ex.BrandId);
            } catch(InternalServerErrorException ex){
                return StatusCode(500, ex.Message);
            } catch(Exception){
                return StatusCode(400);
            }
        }
    }
}