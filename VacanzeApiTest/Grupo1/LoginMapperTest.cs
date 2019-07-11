using System.Collections.Generic;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo1;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo1;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo1;

namespace vacanze_back.VacanzeApiTest.Grupo1
{
    [TestFixture]
    public class LoginMapperTest{

        private LoginMapper _loginMapper;
        private LoginDTO _loginDto;
        private Login _loginEntity;
        private List<Login> _loginEntityList;
        private List<LoginDTO> _loginDtoList;

        [SetUp]
        public void SetUp(){
            _loginMapper = MapperFactory.createLoginMapper();
            
            //DTO
            _loginDto = new LoginDTO();
            _loginDto.email = "krlsanoja@gmail.com";
            _loginDto.password="madrid2014";
            _loginDto.id = 1;
            var rolesdto = new List<RoleDTO>();
            rolesdto.Add(new RoleDTO(1, "Cliente"));
            _loginDto.roles = rolesdto;
            _loginDtoList = new List<LoginDTO>();
            _loginDtoList.Add(_loginDto);
            
            //Entity 
            _loginEntity = new Login();
            _loginEntity.email = "krlsanoja@gmail.com";
            _loginEntity.password = "madrid2014";
            _loginEntity.Id = 1;
            var roles = new List<Role>();
            roles.Add(new Role(1, "Cliente"));
            _loginEntity.roles = roles;
            _loginEntityList = new List<Login>();
            _loginEntityList.Add(_loginEntity);

        }

        [Test]
        public void CreateLoginEntityTest(){
            var loginEntity = _loginMapper.CreateEntity(_loginDto);
            Assert.AreEqual(_loginDto.email, loginEntity.email);
        }
        
        [Test]
        public void CreateLoginDTOTest(){
            var loginDTO  = _loginMapper.CreateDTO(_loginEntity);
            Assert.AreEqual(_loginEntity.email, loginDTO.email);
        }

        [Test]
        public void CreateEntityListTest(){
            List<Login> loginList = new List<Login>();
            loginList = _loginMapper.CreateEntityList(_loginDtoList);
            Assert.AreEqual(_loginDtoList.Count,loginList.Count);
        }

        [Test]
        public void CreateDTOListTest(){
            List<LoginDTO> loginDTOList = new List<LoginDTO>();
            loginDTOList = _loginMapper.CreateDTOList(_loginEntityList);
            Assert.AreEqual(_loginEntityList.Count,loginDTOList.Count);
        }


        [TearDown]
        public void TearDown(){
            _loginDto = null;
            _loginMapper = null;
            _loginEntity = null;
            _loginEntityList = null;
            _loginDtoList = null;
        }
    }
}