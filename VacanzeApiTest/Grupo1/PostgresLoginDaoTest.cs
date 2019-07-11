using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo1;
using System;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo1;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo1;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo2;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;

namespace vacanze_back.VacanzeApiTest.Grupo1
{
	[TestFixture]
	public class PostgresLoginDaoTest{

        private PostgresLoginDAO _loginDao;
        private Login _login;


        UserDTO _userDto;
        private UsersController _userController;


        [SetUp]
        public void SetUp(){
            //Creo un usuario para realizar las pruebas
            _userDto = new UserDTO();
            _userController = new UsersController();
            _userDto.Email = "krlsanojaDAO@gmail.com";
            _userDto.DocumentId = 19986048;
            _userDto.Name = "Carlos";
            _userDto.Lastname = "Sanoja";
            _userDto.Password = "madrid2014";
            var roles = new List<Role>();
            roles.Add(new Role(1,"Cliente"));
            _userDto.Roles = roles;
            var userResult = _userController.Post(_userDto).Value;

            _login = new Login(_userDto.Id,_userDto.Email,_userDto.Password);
            _loginDao = (PostgresLoginDAO) DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetLoginDAO();
        }

        [Test]
        public void SessionLoginDAOTest(){
            _login = _loginDao.SessionLogin(_login.email, _login.password);
            Assert.AreEqual(_login.email, _userDto.Email);
        }

        [Test]
        public void RecoveryDAOTest(){
            _login = _loginDao.Recovery(_login.email);
            Assert.AreEqual(_login.email, _userDto.Email);
        }

        [Test]
        public void InvalidSessionLoginDAOTest(){

            Assert.Throws<LoginUserNotFoundException>(() => _loginDao.SessionLogin("",""));
        }

        [Test]
        public void InvalidRecoveryDAOTest(){

            Assert.Throws<PasswordRecoveryException>(() => _loginDao.Recovery(""));
        }

        [TearDown]
        public void TearDown(){
            var result = _userController.Delete(_login.Id);
            _userDto = null;
            _userController = null;
            _login = null;
            _loginDao = null;
        }
    }
}