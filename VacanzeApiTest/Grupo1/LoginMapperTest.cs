using System.Collections.Generic;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo1;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo1;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo1;

namespace vacanze_back.VacanzeApiTest.Grupo1
{
    [TestFixture]
    public class LoginMapperTest{

        private LoginMapper _loginMapper;
        private LoginDTO _loginDto;
        private Login _loginEntity;

        [SetUp]
        public void SetUp(){
            _loginMapper = MapperFactory.createLoginMapper();
            _loginDto = new LoginDTO();
            _loginDto.email = "krlsanoja@gmail.com";
            _loginEntity = new Login();
            _loginEntity.email = "krlsanoja@gmail.com";
        }

        [Test]
        public void CreateLoginEntityTest(){
            var loginEntity = _loginMapper.CreateEntity(_loginDto);
            Assert.AreEqual(_loginDto.email, loginEntity.email);
        }
        
        [Test]
        public void CreateLoginDTOTest(){
            var loginDTO = _loginMapper.CreateDTO(_loginEntity);
            Assert.AreEqual(_loginEntity.email, loginDTO.email);
        }

        [TearDown]
        public void TearDown(){
            _loginDto = null;
            _loginMapper = null;
            _loginEntity = null;
        }
    }
}