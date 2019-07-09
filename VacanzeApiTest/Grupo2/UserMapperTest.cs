using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.LogicLayer.DTO;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo2;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo2;

namespace vacanze_back.VacanzeApiTest.Grupo2
{
    [TestFixture]
    public class UserMapperTest
    {
        private User _user;

        [SetUp]
        [Order(0)]
        public void Setup()
        {
            var roles = new List<Role>();
            roles.Add(new Role(1, "Cliente"));
            _user = new User(0, 23456789, "Pedro", "Perez",
                   "cliente1@vacanze.com", "12345678", roles);
        }

        [Test]
        [Order(1)]
        public void CreateDTOTest()
        {
            UserMapper userMapper = MapperFactory.createUserMapper();
            var result = userMapper.CreateDTO(_user);
            Assert.IsInstanceOf<UserDTO>(result);
        }
        [Test]
        [Order(2)]
        public void CreateEntityTest()
        {
            UserMapper userMapper = MapperFactory.createUserMapper();
            UserDTO userDTO = DTOFactory.CreateUserDTO(_user.Id, _user.DocumentId, _user.Email,
                _user.Lastname, _user.Name, _user.Password, _user.Roles);
            var result = userMapper.CreateEntity(userDTO);
            Assert.IsInstanceOf<User>(result);
        }
        [Test]
        [Order(3)]
        public void CreateDTOListTest()
        {
            UserMapper userMapper = MapperFactory.createUserMapper();
            List<User> entities = new List<User>();
            entities.Add(_user);
            entities.Add(_user);
            var result = userMapper.CreateDTOList(entities);
            Assert.IsInstanceOf<List<UserDTO>>(result);
        }
        [Test]
        [Order(4)]
        public void CreateEntityListTest()
        {
            UserMapper userMapper = MapperFactory.createUserMapper();
            UserDTO userDTO = DTOFactory.CreateUserDTO(_user.Id, _user.DocumentId, _user.Email,
                _user.Lastname, _user.Name, _user.Password, _user.Roles);
            List<UserDTO> dtos = new List<UserDTO>();
            dtos.Add(userDTO);
            dtos.Add(userDTO);
            var result = userMapper.CreateEntityList(dtos);
            Assert.IsInstanceOf<List<User>>(result);
        }
    }
}
