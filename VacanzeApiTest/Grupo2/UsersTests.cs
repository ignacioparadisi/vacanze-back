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
        private User _userTest;
        private int _id;

        [SetUp]
        public void SetUp()
        {
            var roles = new List<Role>();
            roles.Add(new Role(1,"Cliente"));
            _userTest = new User(0, 23456789, "Pedro", "Perez",
                "cliente1@vacanze.com", "12345678", roles);
            // UserRepository.AddUser(_userTest);
        }
        
        [Test]
        public void AddUserDbTest()
        {
            var user = UserRepository.AddUser(_userTest);
            Assert.True(user.Id > 0);
        }

        [Test]
        public void DeleteUserByIdTest()
        {
            var user = UserRepository.AddUser(_userTest);
            var id = UserRepository.DeleteUserById(user.Id);
            Assert.AreEqual(id, user.Id);
        }

        
        [Test]
        public void GetEmployeesFromDbTest()
        {
            var roles = new List<Role>();
            roles.Add(new Role(3, "Checkin"));
            _userTest.Roles = roles;
            UserRepository.AddUser(_userTest);
            List<User> users = UserRepository.GetEmployees();
            Assert.AreNotEqual(0, users.Count);
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
            roles.Add(new Role(2, "Checkin"));
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
        public void AddUserResponseTest()
        {
            var controller = new UsersController();
            ActionResult<User> user = controller.Post(_userTest);
            Assert.True(user.Value.Id > 0);
        }

        [Test]
        public void RepeatedEmailTest()
        {
            var user = UserRepository.AddUser(_userTest);
            Assert.Throws<RepeatedEmailException>(() => UserRepository.VerifyEmail("cliente1@vacanze.com"));
        }

        [Test]
        public void GetUserTest()
        {
            var controller = new UsersController();
            ActionResult<User> user = controller.Get(1);
            Assert.True((user.Value.Id == 1) && (user.Value.Roles.Count > 0));
        }

        [Test]
        public void UserNotFoundTest()
        {
            Assert.Throws<UserNotFoundException>(() => UserRepository.GetUserById(100));
        }

        [Test]
        public void ModifyUserDbTest()
        {
            var user = UserRepository.AddUser(_userTest);
            user.Name = "Francisco";
            long id = UserRepository.UpdateUser(user, user.Id);
            Assert.AreEqual(user.Id, id);
        }

        [Test]
        public void ModifyUserResponseTest()
        {
            var controller = new UsersController();
            ActionResult<User> user = controller.Post(_userTest);
            user.Value.Name = "Francisco";
            ActionResult<int> id = controller.Put(user.Value.Id, user.Value);
            Assert.AreEqual(user.Value.Id, id.Value);
        }

        [TearDown]
        public void TearDown()
        {
            UserRepository.DeleteUserByEmail("cliente1@vacanze.com");
        }
        
    }
}