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
    public class UsersTests
    {
        private User UserTest;

        [SetUp]
        public void CreateUserTest()
        {
            var roles = new List<Role>();
            roles.Add(new Role(1,"Cliente"));
            UserTest = new User(0, 23456789, "Pedro", "Perez", 
                "cliente1@vacanze.com", "12345678", roles);
        }
        
        [Test]
        public void GetEmployeesFromDbTest()
        {
            List<User> users = UserRepository.GetEmployees();
            Assert.AreNotEqual(0, users.Count());
        }

        [Test]
        public void GetEmployeesResponseTest()
        {
            var controller = new UsersController();
            ActionResult<IEnumerable<User>> users = controller.GetEmployees();
            Assert.AreNotEqual(0, users.Value.Count());
        }

        [Test]
        public void NotValidDocumentIdExceptionTest()
        {
            var roles = new List<Role>();
            roles.Add(new Role(1, "Checkin"));
            var user = new User(0, 0, "Nombre", "Apellido", 
                "usuario@vacanza.com", "12345678", roles);
            Assert.Throws<NotValidDocumentIdException>(() => { user.Validate(); });
        }
        
        [Test]
        public void NameRequiredExceptionTest()
        {
            var roles = new List<Role>();
            roles.Add(new Role(1, "Checkin"));
            var user = new User(0, 12345678, null, "Apellido", 
                "usuario@vacanza.com", "12345678", roles);
            Assert.Throws<NameRequiredException>(() => { user.Validate(); });
        }
        
        [Test]
        public void LastnameRequiredExceptionTest()
        {
            var roles = new List<Role>();
            roles.Add(new Role(1, "Checkin"));
            var user = new User(0, 12345678, "Nombre", null, 
                "usuario@vacanza.com", "12345678", roles);
            Assert.Throws<LastnameRequiredException>(() => { user.Validate(); });
        }
        
        [Test]
        public void EmailRequiredExceptionTest()
        {
            var roles = new List<Role>();
            roles.Add(new Role(1, "Checkin"));
            var user = new User(0, 12345678, "Nombre", "Apellido", 
                null, "12345678", roles);
            Assert.Throws<EmailRequiredException>(() => { user.Validate(); });
        }
        
        [Test]
        public void NotValidEmailExceptionTest()
        {
            var roles = new List<Role>();
            roles.Add(new Role(1, "Checkin"));
            var user = new User(0, 12345678, "Nombre", "Apellido", 
                "correo", "12345678", roles);
            Assert.Throws<NotValidEmailException>(() => { user.Validate(); });
        }
        
        [Test]
        public void RoleRequiredExceptionTest()
        {
            var user = new User(12345678, "Nombre", "Apellido", 
                "usuario@vacanze.com", "12345678");
            Assert.Throws<RoleRequiredException>(() => { user.Validate(); });
        }
        
        [Test]
        public void NotValidRoleExceptionTest()
        {
            var roles = new List<Role>();
            roles.Add(new Role(-1, "Checkin"));
            var user = new User(0, 12345678, "Nombre", "Apellido", 
                "usuario@vacanza.com", "12345678", roles);
            Assert.Throws<NotValidIdException>(() => { user.Validate(); });
        }

        [Test]
        public void AddUserDbTest()
        {
            var user = UserRepository.AddUser(UserTest);
            Assert.True(user.Id > 0);
        }

        [Test]
        public void AddUserResponseTest()
        {
            var controller = new UsersController();
            ActionResult<User> user = controller.Post(UserTest);
            Assert.True(user.Value.Id > 0);
        }

        [Test]
        public void RepeatedEmailTest()
        {
            var user = UserRepository.AddUser(UserTest);
            Assert.Throws<RepeatedEmailException>(() => UserRepository.VerifyEmail("cliente1@vacanze.com"));
        }
        

        [TearDown]
        public void DeleteUserTest()
        {
            UserRepository.DeleteUserByEmail("cliente1@vacanze.com");
        }
        
    }
}