using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Persistence.Connection.Grupo2;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo2;

namespace vacanze_back.VacanzeApiTest.Grupo2
{
    [TestFixture]
    public class UsersTests
    {
        [Test]
        public void GetEmployeesFromDBTest()
        {
            var connection = new UserConnection();
            List<User> users = connection.GetEmployees();
            Assert.AreNotEqual(0, users.Count());
        }

        [Test]
        public void GetEmployeesResponseTest()
        {
            var controller = new UsersController();
            ActionResult<IEnumerable<User>> users = controller.GetEmployees();
            Assert.AreNotEqual(0, users.Value.Count());
        }
    }
}