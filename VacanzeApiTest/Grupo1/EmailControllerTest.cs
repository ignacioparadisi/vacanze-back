using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo1;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo1;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo1;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo1;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo2;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;

namespace vacanze_back.VacanzeApiTest.Grupo1{
    [TestFixture]

    public class EmailControllerTest{
        private EmailController _emailController;
        LoginDTO _loginDto;

        UserDTO _userDto;
        private UsersController _userController;
        private int _userId;

        [SetUp]
        public void SetUp(){
            _emailController = new EmailController(null);
            _loginDto = new LoginDTO();

            //Creo un usuario para realizar las pruebas
            _userDto = new UserDTO();
            _userController = new UsersController();
            _userDto.Email = "krlsanoja@gmail.com";
            _userDto.DocumentId = 19986048;
            _userDto.Name = "Carlos";
            _userDto.Lastname = "Sanoja";
            _userDto.Password = "madrid2014";
            var roles = new List<Role>();
            roles.Add(new Role(1,"Cliente"));
            _userDto.Roles = roles;

        }

        [Test]
        public void ValidPostEmailRecoveryTest(){
            _loginDto.email = _userDto.Email;
            var result = _emailController.Recovery(_loginDto);
            Assert.NotNull(result);
        }

        [Test]
        public void InvalidadPostEmailRecoveryTest(){
            _loginDto.email = null;
            var result = _emailController.Recovery(_loginDto).Value;
            Assert.IsInstanceOf<BadRequestObjectResult>(_emailController.Recovery(_loginDto).Result);
        }

        [TearDown]
        public void TearDown(){
            var result = _userController.Delete(_userId);
            _emailController = null;
            _loginDto = null;
            _userDto = null;
            _userController = null;
            _userId = 0;
        }
    }
}