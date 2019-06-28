using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Common.Exceptions;
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
                BrandMapper brandMapper = MapperFactory.createBrandMapper();
                Entity entity = brandMapper.CreateEntity(brandDTO);
                AddBrandCommand command = CommandFactory.createAddBrandCommand ((Brand) entity);
                command.Execute ();
                return Ok ("ok " + command.GetResult ());
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
                GetBrandsCommand command = CommandFactory.createGetBrandsCommand();
                command.Execute ();
                return Ok (command.GetResult ());
            } catch (InternalServerErrorException ex) {
                return StatusCode (500, ex.Message);
            } catch (Exception) {
                return StatusCode (400);
            }
        }
    }
}