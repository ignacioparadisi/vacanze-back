using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo2;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo2;
namespace vacanze_back.VacanzeApiTest.Grupo2
{
    [TestFixture]
    public class RolesTest
    {
        
        private RolesController _rolesController = new RolesController();
        
        [Test]
        public void GetRolesTest()
        {
            var roles = RoleRepository.GetRoles();
            Assert.AreEqual(5,roles.Count);
        }

        [Test]
        public void RequestRolesTest()
        {
            var result = _rolesController.GetRoles();
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void ValidateRoleTest()
        {
            var role = new Role(1, "Cliente");
            Assert.IsTrue(role.Validate());
        }

        [Test]
        public void ValidateRoleNotValidIdExceptionTest()
        {
            var role = new Role(-1, "Cliente");
            Assert.Throws<NotValidIdException>(() => { role.Validate(); });
        }
    }
}