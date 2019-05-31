

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo1
{
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using vacanze_back.VacanzeApi.Common.Entities.Grupo1;
    using vacanze_back.VacanzeApi.Persistence.Repository.Grupo1;
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class LoginController:ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        //POST : /api/Login
        public async Task<IActionResult> Login(Login log)
        {
           
            LoginRepository lg = new LoginRepository();
            try
            {
                var objUser = lg.SessionLogin(log.Email, log.Password);
                
                if (objUser != null)
                {
                    Login login = new Login(objUser.Id,objUser.Roles, log.Email, log.Password);
                    return Ok(login);
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
