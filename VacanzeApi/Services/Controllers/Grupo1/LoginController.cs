using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo1;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo1;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo1;
using vacanze_back.VacanzeApi.LogicLayer.DTO;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo1;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo1;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo1
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class LoginController:ControllerBase
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        //POST : /api/Login
        public  ActionResult<LoginDTO> Login([FromBody] LoginDTO loginDTO)
        {
            try{
                LoginMapper LoginMapper = MapperFactory.createLoginMapper();
                Entity entity = LoginMapper.CreateEntity(loginDTO);
                GetUserCommand command = CommandFactory.loginGetUserCommand((Login)entity);
                command.Execute();

                Login answer = command.GetResult();
                DTO lDTO = LoginMapper.CreateDTO(answer);
                return Ok(lDTO);
            }
            catch(DatabaseException ex){
                _logger?.LogError(ex, "Database exception when trying to get a claim by id");
                return StatusCode(500, ex.Message);
            }
        }

    }
}
