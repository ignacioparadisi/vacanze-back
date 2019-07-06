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
            } catch (InternalServerErrorException ex) {
                return StatusCode (500, ex.Message);
            } catch (Exception) {
                return StatusCode (400);
            }
        }
    }
}