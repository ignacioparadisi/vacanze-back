using System.Linq;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo2;

namespace vacanze_back.VacanzeApiTest.Grupo2
{
    [TestFixture]
    public class UsersTests
    {
        [Test]
        public void GetEmployeesTest()
        {
            var connection = new UserConnection();
            var users = connection.GetEmployees();
            Assert.AreNotEqual(0, users.Count());
        }
    }
}