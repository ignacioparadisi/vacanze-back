using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo2;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo2;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo2;

namespace vacanze_back.VacanzeApiTest.Grupo2
{
    [TestFixture]
    public class UsersControllerTests
    {
        private UsersController _usersController;
        private User _user;
        private UserDTO _dto;
        private List<int> _insertedUsers;
        
        [SetUp]
        public void SetUp()
        {
            _usersController = new UsersController();
            _insertedUsers = new List<int>();
            var roles = new List<Role>();
            roles.Add(new Role(1,"Cliente"));
            _user = new User(0, 23456789, "Pedro", "Perez",
                "cliente1@vacanze.com", "12345678", roles);
        }
        
        [TearDown]
        public void TearDown()
        {
            foreach (var id in _insertedUsers)
            {
                UserRepository.DeleteUserById(id);
            }
            _insertedUsers.Clear();
        }
        
         [Test]
        public void GetEmployeesResponseTest()
        {
            var result = _usersController.GetEmployees();
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }
        
        [Test]
        public void PostUserTest()
        {
            var result = _usersController.Post(_dto);
            var okObject = (OkObjectResult) result.Result;
            var idToDelete = ((User) okObject.Value).Id;
            _insertedUsers.Add(idToDelete);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void PostUser_NotValidDocumentIdExceptionTest()
        {
            _user.DocumentId = 0;
            var result = _usersController.Post(_dto);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void PostUser_NotValidNameException_NullTest()
        {
            _user.Name = null;
            var result = _usersController.Post(_dto);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PostUser_NotValidNameException_EmptyTest()
        {
            _user.Name = "";
            var result = _usersController.Post(_dto);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PostUser_NotValidNameException_WhiteSpaceTest()
        {
            _user.Name = " ";
            var result = _usersController.Post(_dto);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PostUser_NotValidLastNameException_NullTest()
        {
            _user.Lastname = null;
            var result = _usersController.Post(_dto);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PostUser_NotValidLastNameException_EmptyTest()
        {
            _user.Lastname = "";
            var result = _usersController.Post(_dto);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PostUser_NotValidLastNameException_WhiteSpaceTest()
        {
            _user.Lastname = " ";
            var result = _usersController.Post(_dto);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PostUser_EmailRequiredException_NullTest()
        {
            _user.Email = null;
            var result = _usersController.Post(_dto);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PostUser_EmailRequiredException_EmptyTest()
        {
            _user.Email = "";
            var result = _usersController.Post(_dto);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PostUser_EmailRequiredException_WhiteSpaceTest()
        {
            _user.Email = " ";
            var result = _usersController.Post(_dto);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PostUser_NotValidEmailExceptionTest()
        {
            _user.Email = "email";
            var result = _usersController.Post(_dto);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PostUser_RoleRequiredException_NullTest()
        {
            _user.Roles = null;
            var result = _usersController.Post(_dto);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PostUser_RoleRequiredException_EmptyTest()
        {
            _user.Roles = new List<Role>();
            var result = _usersController.Post(_dto);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PostUser_NotValidRoleExceptionTest()
        {
            var roles = new List<Role>();
            roles.Add(new Role(-1, "Checkin"));
            _user.Roles = roles;
            var result = _usersController.Post(_dto);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void PostUser_AdminAndMoreRolesExceptionTest()
        {
            var roles = new List<Role>();
            roles.Add(new Role(Role.ADMIN, "Administrador"));
            roles.Add(new Role(Role.CLAIM, "Reclamo"));
            _user.Roles = roles;
            var result = _usersController.Post(_dto);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PostUser_VerifyEmailTest()
        {
            var user = UserRepository.AddUser(_user);
            _insertedUsers.Add(user.Id);
            var result = _usersController.Post(_dto);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void PostUser_PasswordRequiredException_NullTest()
        {
            _user.Password = null;
            var result = _usersController.Post(_dto);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PostUser_PasswordRequiredException_EmptyTest()
        {
            _user.Password = "";
            var result = _usersController.Post(_dto);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PostUser_PasswordRequiredException_WhiteSpaceTest()
        {
            _user.Password = "  ";
            var result = _usersController.Post(_dto);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void GetUserByIdRequestTest()
        {
            var user = UserRepository.AddUser(_user);
            var result = _usersController.Get(user.Id);
            _insertedUsers.Add(user.Id);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void GetUserById_UserNotFoundExceptionTest()
        {
            var result = _usersController.Get(0);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PutUserRequestTest()
        {
            var user = UserRepository.AddUser(_user);
            _user.Name = "Francisco";
            var result = _usersController.Put(user.Id, _user);
            _insertedUsers.Add(user.Id);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void PutUser_UserNotFoundExceptionTest()
        {
            var result = _usersController.Put(0, _user);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PutUser_NotValidDocumentIdExceptionTest()
        {
            _user.DocumentId = 0;
            var result = _usersController.Put(0, _user);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void PutUser_NotValidNameException_NullTest()
        {
            _user.Name = null;
            var result = _usersController.Put(0, _user);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PutUser_NotValidNameException_EmptyTest()
        {
            _user.Name = "";
            var result = _usersController.Put(0, _user);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PutUser_NotValidNameException_WhiteSpaceTest()
        {
            _user.Name = " ";
            var result = _usersController.Put(0, _user);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PutUser_NotValidLastNameException_NullTest()
        {
            _user.Lastname = null;
            var result = _usersController.Put(0, _user);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PutUser_NotValidLastNameException_EmptyTest()
        {
            _user.Lastname = "";
            var result = _usersController.Put(0, _user);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PutUser_NotValidLastNameException_WhiteSpaceTest()
        {
            _user.Lastname = " ";
            var result = _usersController.Put(0, _user);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PutUser_EmailRequiredException_NullTest()
        {
            _user.Email = null;
            var result = _usersController.Put(0, _user);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PutUser_EmailRequiredException_EmptyTest()
        {
            _user.Email = "";
            var result = _usersController.Put(0, _user);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PutUser_EmailRequiredException_WhiteSpaceTest()
        {
            _user.Email = " ";
            var result = _usersController.Put(0, _user);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PutUser_NotValidEmailExceptionTest()
        {
            _user.Email = "email";
            var result = _usersController.Put(0, _user);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PutUser_RoleRequiredException_NullTest()
        {
            _user.Roles = null;
            var result = _usersController.Put(0, _user);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PutUser_RoleRequiredException_EmptyTest()
        {
            _user.Roles = new List<Role>();
            var result = _usersController.Put(0, _user);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void PutUser_NotValidRoleExceptionTest()
        {
            var roles = new List<Role>();
            roles.Add(new Role(-1, "Checkin"));
            _user.Roles = roles;
            var result = _usersController.Put(0, _user);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void PutUser_AdminAndMoreRolesExceptionTest()
        {
            var roles = new List<Role>();
            roles.Add(new Role(Role.ADMIN, "Administrador"));
            roles.Add(new Role(Role.CLAIM, "Reclamo"));
            _user.Roles = roles;
            var result = _usersController.Put(0, _user);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void PutUser_VerifyEmailTest()
        {
            var user = UserRepository.AddUser(_user);
            _insertedUsers.Add(user.Id);
            _user.Id = 0;
            var result = _usersController.Put(0, _user);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void DeleteUserByIdRequestTest()
        {
            var user = UserRepository.AddUser(_user);
            var result = _usersController.Delete(user.Id);
            Assert.IsInstanceOf<OkResult>(result.Result);
        }

        [Test]
        public void DeleteUserById_UserNotFoundException()
        {
            var result = _usersController.Delete(0);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
    }
}