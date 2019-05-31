using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo2;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo2;
namespace vacanze_back.VacanzeApiTest.Grupo2
{
    [TestFixture]
    public class RolesTest
    {
        [Test]
        public void GetRolesTest()
        {
            ActionResult<IEnumerable<Role>> roles = RoleRepository.GetRoles();
            Assert.AreEqual(5,roles.Value.Count());
        }
    }
}