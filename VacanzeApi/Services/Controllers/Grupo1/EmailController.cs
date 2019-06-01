

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using MimeKit;
    using vacanze_back.VacanzeApi.Common.Entities.Grupo1;
    using vacanze_back.VacanzeApi.Persistence.Repository.Grupo1;

    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpPost]
        [Route("Email")]

        //POST : /api/ApplicationUser/Email
        public async Task<IActionResult> Recovery(Login Log)
        {
            try
            {
                EmailRepository msg = new EmailRepository();
                var objUser = msg.Recovery(Log.Email);
                 if (objUser != null)
                {
                    //logica correo
                    var message = new MimeMessage();
                    //From Address
                    message.From.Add(new MailboxAddress("Vacanze Administracion", "vacanzeucab@gmail.com"));
                    //To Address
                    message.To.Add(new MailboxAddress("Usuario", Log.Email));
                    //Subject
                    message.Subject = "Recuperacion De Contraseña : ";

                    message.Body = new TextPart("plain")
                    {
                        Text = "Su contraseña nueva:" + objUser
                    };
                    using (var client = new MailKit.Net.Smtp.SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, false);
                        client.Authenticate("vacanzeucab@gmail.com", "desarrollo1-");
                        client.Send(message);
                        client.Disconnect(true);
                        client.Dispose();
                    }
                    return Ok(Log.Email);
                }
                else
                {
                    return BadRequest(new { message = "Username or password is incorrect." });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}