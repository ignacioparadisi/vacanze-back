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
    public class RoleMapperTest
    {
        public Role role;

        [SetUp]
        [Order(0)]
        public void Setup()
        {
            var roles = new List<Role>();
            roles.Add(new Role(1, "Cliente"));
            role = new Role(1, "Cliente");
        }

        [Test]
        [Order(1)]
        public void CreateDTOTest()
        {
            RoleMapper roleMapper = MapperFactory.createRoleMapper();
            var result = roleMapper.CreateDTO(role);
            Assert.IsInstanceOf<RoleDTO>(result);
        }
        [Test]
        [Order(2)]
        public void CreateEntityTest()
        {
            RoleMapper roleMapper = MapperFactory.createRoleMapper();
            RoleDTO roleDTO = DTOFactory.CreateRoleDTO(role.Id,role.Name);
            var result = roleMapper.CreateEntity(roleDTO);
            Assert.IsInstanceOf<Role>(result);
        }
        [Test]
        [Order(3)]
        public void CreateDTOListTest()
        {
            RoleMapper roleMapper = MapperFactory.createRoleMapper();
            List<Role> roles = new List<Role>();
            roles.Add(role);
            roles.Add(role);
            var result = roleMapper.CreateDTOList(roles);
            Assert.IsInstanceOf<List<RoleDTO>>(result);
        }
        [Test]
        [Order(4)]
        public void CreateEntityListTest()
        {
            RoleMapper roleMapper = MapperFactory.createRoleMapper();
            RoleDTO roleDTO = DTOFactory.CreateRoleDTO(role.Id, role.Name);
            List<RoleDTO> dtos = new List<RoleDTO>();
            dtos.Add(roleDTO);
            dtos.Add(roleDTO);
            var result = roleMapper.CreateEntityList(dtos);
            Assert.IsInstanceOf<List<Role>>(result);
        }
    }
}
