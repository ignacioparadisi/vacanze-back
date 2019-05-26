using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Persistence.Connection.Grupo2;

namespace vacanze_back.VacanzeApiTest.Grupo2
{
    [TestFixture]
    public class UsersTests
    {
        [Test]
        public void GetEmployeesTest()
        {
            var connection = new UserConnection();
            List<User> users = connection.GetEmployees();
            Assert.AreNotEqual(0, users.Count());
        }
    }
}