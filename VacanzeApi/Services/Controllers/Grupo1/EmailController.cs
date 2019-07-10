using System;
using MimeKit;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo1;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo1;
using vacanze_back.VacanzeApi.LogicLayer.DTO;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo1;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo1;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo1
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpPost]

        //POST : /api/ApplicationUser/Email
        public ActionResult<LoginDTO> Recovery([FromBody] LoginDTO loginDTO)
        {
            Console.WriteLine("Antes del TRY, en el EmailController");
            LoginMapper LoginMapper = MapperFactory.createLoginMapper();
            Entity entity = LoginMapper.CreateEntity(loginDTO);
            RecoveryPasswordCommand command = CommandFactory.RecoveryPasswordCommand((Login) entity);
            command.Execute();
                
            Login objUser = command.GetResult();
            if (objUser != null){

                Console.WriteLine("Esta funcionando");
                //logica correo
                var message = new MimeMessage();
                //From Address
                message.From.Add(new MailboxAddress("Vacanze Administracion", "vacanzeucab@gmail.com"));
                //To Address
                message.To.Add(new MailboxAddress("Usuario", address: objUser.email));
                //Subject
                message.Subject = "Recuperacion De Contraseña : ";

                message.Body = new TextPart("plain"){
                   Text = "Su contraseña nueva:" + objUser.password
                };

                using (var client = new MailKit.Net.Smtp.SmtpClient()){
                    client.Connect("smtp.gmail.com", 587, true);
                    client.Authenticate("vacanzeucab1@gmail.com", "vacanze1234");
                    client.Send(message);
                    client.Disconnect(true);
                    client.Dispose();
                }
                LoginDTO ldto = LoginMapper.CreateDTO(objUser);
                return Ok(ldto);
            }
            else{
                return BadRequest(new { message = "Username or password is incorrect." });
            }
            
        }
    }
}