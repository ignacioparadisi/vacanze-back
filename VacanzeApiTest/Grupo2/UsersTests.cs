using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo2;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo2;

namespace vacanze_back.VacanzeApiTest.Grupo2
{
    [TestFixture]
    public class UsersTests
    {
        private UsersController _usersController;
        private User _user;
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
        public void DeleteUserByIdTest()
        {
            var user = UserRepository.AddUser(_user);
            var id = UserRepository.DeleteUserById(user.Id);
            Assert.AreEqual(id, user.Id);
        }

        [Test]
        public void DeleteUserById_UserNotFoundException()
        {
            Assert.Throws<UserNotFoundException>(() => { UserRepository.DeleteUserById(0); });
        }
        
        [Test]
        public void NotValidDocumentIdExceptionTest()
        {
            _user.DocumentId = 0;
            Assert.Throws<NotValidDocumentIdException>(() => { _user.Validate(); });
        }
        
        [Test]
        public void NameRequiredException_NullTest()
        {
            _user.Name = null;
            Assert.Throws<NameRequiredException>(() => { _user.Validate(); });
        }
        
        [Test]
        public void NameRequiredException_EmptyTest()
        {
            _user.Name = "";
            Assert.Throws<NameRequiredException>(() => { _user.Validate(); });
        }
        
        [Test]
        public void NameRequiredException_WhiteSpaceTest()
        {
            _user.Name = " ";
            Assert.Throws<NameRequiredException>(() => { _user.Validate(); });
        }
        
        [Test]
        public void LastnameRequiredException_NullTest()
        {
            _user.Lastname = null;
            Assert.Throws<LastnameRequiredException>(() => { _user.Validate(); });
        }
        
        [Test]
        public void LastnameRequiredException_EmptyTest()
        {
            _user.Lastname = "";
            Assert.Throws<LastnameRequiredException>(() => { _user.Validate(); });
        }
        
        [Test]
        public void LastnameRequiredException_WhiteSpaceTest()
        {
            _user.Lastname = " ";
            Assert.Throws<LastnameRequiredException>(() => { _user.Validate(); });
        }
        
        [Test]
        public void EmailRequiredException_NullTest()
        {
            _user.Email = null;
            Assert.Throws<EmailRequiredException>(() => { _user.Validate(); });
        }
        
        [Test]
        public void EmailRequiredException_EmptyTest()
        {
            _user.Email = "";
            Assert.Throws<EmailRequiredException>(() => { _user.Validate(); });
        }
        
        [Test]
        public void EmailRequiredException_WhiteSpaceTest()
        {
            _user.Email = " ";
            Assert.Throws<EmailRequiredException>(() => { _user.Validate(); });
        }
        
        [Test]
        public void NotValidEmailExceptionTest()
        {
            _user.Email = "email";
            Assert.Throws<NotValidEmailException>(() => { _user.Validate(); });
        }
        
        [Test]
        public void RoleRequiredException_NullTest()
        {
            _user.Roles = null;
            Assert.Throws<RoleRequiredException>(() => { _user.Validate(); });
        }
        
        [Test]
        public void RoleRequiredException_EmptyTest()
        {
            _user.Roles = new List<Role>();
            Assert.Throws<RoleRequiredException>(() => { _user.Validate(); });
        }
        
        [Test]
        public void NotValidRoleExceptionTest()
        {
            var roles = new List<Role>();
            roles.Add(new Role(-1, "Checkin"));
            _user.Roles = roles;
            Assert.Throws<NotValidIdException>(() => { _user.Validate(); });
        }

        [Test]
        public void AdminAndMoreRolesExceptionTest()
        {
            var roles = new List<Role>();
            roles.Add(new Role(Role.ADMIN, "Administrador"));
            roles.Add(new Role(Role.CLAIM, "Reclamo"));
            _user.Roles = roles;
            Assert.Throws<AdminAndMoreRolesException>(() => { _user.Validate(); });
        }

        [Test]
        public void ValidateSuccessfullyTest()
        {
            Assert.IsTrue(_user.Validate());
        }

        [Test]
        public void PasswordRequiredException_NullTest()
        {
            _user.Password = null;
            Assert.Throws<PasswordRequiredException>(() => { _user.EncryptOrCreatePassword(); });
        }
        
        [Test]
        public void PasswordRequiredException_EmptyTest()
        {
            _user.Password = "";
            Assert.Throws<PasswordRequiredException>(() => { _user.EncryptOrCreatePassword(); });
        }
        
        [Test]
        public void PasswordRequiredException_WhiteSpaceTest()
        {
            _user.Password = "  ";
            Assert.Throws<PasswordRequiredException>(() => { _user.EncryptOrCreatePassword(); });
        }

        [Test]
        public void EncryptClientPasswordTest()
        {
            _user.EncryptOrCreatePassword();
            Assert.AreEqual("25d55ad283aa400af464c76d713c07ad", _user.Password);
        }

        [Test]
        public void EncryptEmployeePasswordTest()
        {
            var roles = new List<Role>();
            roles.Add(new Role(Role.CARRIER, "Cargador"));
            _user.Roles = roles;
            _user.EncryptOrCreatePassword();
            var userPassword = _user.Name.Trim().ToLower()[0] + _user.Lastname.Trim().ToLower()[0] + _user.DocumentId.ToString();
            var encrpytedPassword = Encryptor.Encrypt(userPassword);
            Assert.AreEqual(encrpytedPassword, _user.Password);
        }
        
        [Test]
        public void AddUserTest()
        {
            var user = UserRepository.AddUser(_user);
            _insertedUsers.Add(user.Id);
            Assert.True(user.Id > 0);
        }

        [Test]
        public void AddUser_NotValidDocumentIdExceptionTest()
        {
            _user.DocumentId = 0;
            Assert.Throws<NotValidDocumentIdException>(() => { UserRepository.AddUser(_user); });
        }

        [Test]
        public void AddUser_NotValidNameException_NullTest()
        {
            _user.Name = null;
            Assert.Throws<NameRequiredException>(() => { UserRepository.AddUser(_user); });
        }
        
        [Test]
        public void AddUser_NotValidNameException_EmptyTest()
        {
            _user.Name = "";
            Assert.Throws<NameRequiredException>(() => { UserRepository.AddUser(_user); });
        }
        
        [Test]
        public void AddUser_NotValidNameException_WhiteSpaceTest()
        {
            _user.Name = " ";
            Assert.Throws<NameRequiredException>(() => { UserRepository.AddUser(_user); });
        }
        
        [Test]
        public void AddUser_NotValidLastNameException_NullTest()
        {
            _user.Lastname = null;
            Assert.Throws<LastnameRequiredException>(() => { UserRepository.AddUser(_user); });
        }
        
        [Test]
        public void AddUser_NotValidLastNameException_EmptyTest()
        {
            _user.Lastname = "";
            Assert.Throws<LastnameRequiredException>(() => { UserRepository.AddUser(_user); });
        }
        
        [Test]
        public void AddUser_NotValidLastNameException_WhiteSpaceTest()
        {
            _user.Lastname = " ";
            Assert.Throws<LastnameRequiredException>(() => { UserRepository.AddUser(_user); });
        }
        
        [Test]
        public void AddUser_EmailRequiredException_NullTest()
        {
            _user.Email = null;
            Assert.Throws<EmailRequiredException>(() => { UserRepository.AddUser(_user); });
        }
        
        [Test]
        public void AddUser_EmailRequiredException_EmptyTest()
        {
            _user.Email = "";
            Assert.Throws<EmailRequiredException>(() => { UserRepository.AddUser(_user); });
        }
        
        [Test]
        public void AddUser_EmailRequiredException_WhiteSpaceTest()
        {
            _user.Email = " ";
            Assert.Throws<EmailRequiredException>(() => { UserRepository.AddUser(_user); });
        }
        
        [Test]
        public void AddUser_NotValidEmailExceptionTest()
        {
            _user.Email = "email";
            Assert.Throws<NotValidEmailException>(() => { UserRepository.AddUser(_user); });
        }
        
        [Test]
        public void AddUser_RoleRequiredException_NullTest()
        {
            _user.Roles = null;
            Assert.Throws<RoleRequiredException>(() => { UserRepository.AddUser(_user); });
        }
        
        [Test]
        public void AddUser_RoleRequiredException_EmptyTest()
        {
            _user.Roles = new List<Role>();
            Assert.Throws<RoleRequiredException>(() => { UserRepository.AddUser(_user); });
        }
        
        [Test]
        public void AddUser_NotValidRoleExceptionTest()
        {
            var roles = new List<Role>();
            roles.Add(new Role(-1, "Checkin"));
            _user.Roles = roles;
            Assert.Throws<NotValidIdException>(() => { UserRepository.AddUser(_user); });
        }

        [Test]
        public void AddUser_AdminAndMoreRolesExceptionTest()
        {
            var roles = new List<Role>();
            roles.Add(new Role(Role.ADMIN, "Administrador"));
            roles.Add(new Role(Role.CLAIM, "Reclamo"));
            _user.Roles = roles;
            Assert.Throws<AdminAndMoreRolesException>(() => { UserRepository.AddUser(_user); });
        }
        
        [Test]
        public void AddUser_VerifyEmailTest()
        {
            var user = UserRepository.AddUser(_user);
            _insertedUsers.Add(user.Id);
            Assert.Throws<RepeatedEmailException>(() => UserRepository.VerifyEmail(_user.Email));
        }

        [Test]
        public void AddUser_PasswordRequiredException_NullTest()
        {
            _user.Password = null;
            Assert.Throws<PasswordRequiredException>(() => { UserRepository.AddUser(_user); });
        }
        
        [Test]
        public void AddUser_PasswordRequiredException_EmptyTest()
        {
            _user.Password = "";
            Assert.Throws<PasswordRequiredException>(() => { UserRepository.AddUser(_user); });
        }
        
        [Test]
        public void AddUser_PasswordRequiredException_WhiteSpaceTest()
        {
            _user.Password = "  ";
            Assert.Throws<PasswordRequiredException>(() => { UserRepository.AddUser(_user); });
        }

        [Test]
        public void GetEmployeesTest()
        {
            var roles = new List<Role>();
            roles.Add(new Role(3, "Checkin"));
            _user.Roles = roles;
            var user = UserRepository.AddUser(_user);
            _insertedUsers.Add(user.Id);
            List<User> users = UserRepository.GetEmployees();
            Assert.AreNotEqual(0, users.Count);
        }

        [Test]
        public void GetUserByIdTest()
        {
            var user = UserRepository.AddUser(_user);
            _insertedUsers.Add(user.Id);
            var savedUser = UserRepository.GetUserById(user.Id);
            Assert.AreEqual(user, savedUser);
        }

        [Test]
        public void GetUserById_UserNotFoundExceptionTest()
        {
            Assert.Throws<UserNotFoundException>(() => { UserRepository.GetUserById(0); });
        }

        [Test]
        public void UpdateUserTest()
        { 
            var user = UserRepository.AddUser(_user);
            _insertedUsers.Add(user.Id);
            user.Name = "Francisco";
            int userId = UserRepository.UpdateUser(user, user.Id);
            Assert.AreEqual(user.Id, userId);
        }

        [Test]
        public void UpdateUser_UserNotFoundExceptionTest()
        {
            Assert.Throws<UserNotFoundException>(() => { UserRepository.UpdateUser(_user, 0); });
        }
        
        [Test]
        public void UpdateUser_NotValidDocumentIdExceptionTest()
        {
            _user.DocumentId = 0;
            Assert.Throws<NotValidDocumentIdException>(() => { UserRepository.UpdateUser(_user, _user.Id); });
        }

        [Test]
        public void UpdateUser_NotValidNameException_NullTest()
        {
            _user.Name = null;
            Assert.Throws<NameRequiredException>(() => { UserRepository.UpdateUser(_user, _user.Id); });
        }
        
        [Test]
        public void UpdateUser_NotValidNameException_EmptyTest()
        {
            _user.Name = "";
            Assert.Throws<NameRequiredException>(() => { UserRepository.UpdateUser(_user, _user.Id); });
        }
        
        [Test]
        public void UpdateUser_NotValidNameException_WhiteSpaceTest()
        {
            _user.Name = " ";
            Assert.Throws<NameRequiredException>(() => { UserRepository.UpdateUser(_user, _user.Id); });
        }
        
        [Test]
        public void UpdateUser_NotValidLastNameException_NullTest()
        {
            _user.Lastname = null;
            Assert.Throws<LastnameRequiredException>(() => { UserRepository.UpdateUser(_user, _user.Id); });
        }
        
        [Test]
        public void UpdateUser_NotValidLastNameException_EmptyTest()
        {
            _user.Lastname = "";
            Assert.Throws<LastnameRequiredException>(() => { UserRepository.UpdateUser(_user, _user.Id); });
        }
        
        [Test]
        public void UpdateUser_NotValidLastNameException_WhiteSpaceTest()
        {
            _user.Lastname = " ";
            Assert.Throws<LastnameRequiredException>(() => { UserRepository.UpdateUser(_user, _user.Id); });
        }
        
        [Test]
        public void UpdateUser_EmailRequiredException_NullTest()
        {
            _user.Email = null;
            Assert.Throws<EmailRequiredException>(() => { UserRepository.UpdateUser(_user, _user.Id); });
        }
        
        [Test]
        public void UpdateUser_EmailRequiredException_EmptyTest()
        {
            _user.Email = "";
            Assert.Throws<EmailRequiredException>(() => { UserRepository.UpdateUser(_user, _user.Id); });
        }
        
        [Test]
        public void UpdateUser_EmailRequiredException_WhiteSpaceTest()
        {
            _user.Email = " ";
            Assert.Throws<EmailRequiredException>(() => { UserRepository.UpdateUser(_user, _user.Id); });
        }
        
        [Test]
        public void UpdateUser_NotValidEmailExceptionTest()
        {
            _user.Email = "email";
            Assert.Throws<NotValidEmailException>(() => { UserRepository.UpdateUser(_user, _user.Id); });
        }
        
        [Test]
        public void UpdateUser_RoleRequiredException_NullTest()
        {
            _user.Roles = null;
            Assert.Throws<RoleRequiredException>(() => { UserRepository.UpdateUser(_user, _user.Id); });
        }
        
        [Test]
        public void UpdateUser_RoleRequiredException_EmptyTest()
        {
            _user.Roles = new List<Role>();
            Assert.Throws<RoleRequiredException>(() => { UserRepository.UpdateUser(_user, _user.Id); });
        }
        
        [Test]
        public void UpdateUser_NotValidRoleExceptionTest()
        {
            var roles = new List<Role>();
            roles.Add(new Role(-1, "Checkin"));
            _user.Roles = roles;
            Assert.Throws<NotValidIdException>(() => { UserRepository.UpdateUser(_user, _user.Id); });
        }

        [Test]
        public void UpdateUser_AdminAndMoreRolesExceptionTest()
        {
            var roles = new List<Role>();
            roles.Add(new Role(Role.ADMIN, "Administrador"));
            roles.Add(new Role(Role.CLAIM, "Reclamo"));
            _user.Roles = roles;
            Assert.Throws<AdminAndMoreRolesException>(() => { UserRepository.UpdateUser(_user, _user.Id); });
        }
        
        [Test]
        public void UpdateUser_VerifyEmailTest()
        {
            var user = UserRepository.AddUser(_user);
            _insertedUsers.Add(user.Id);
            _user.Id = 0;
            Assert.Throws<RepeatedEmailException>(() => UserRepository.VerifyEmail(_user.Email, _user.Id));
        }

//        [Test]
//        public void GetEmployeesResponseTest()
//        {
//            var controller = new UsersController();
//            ActionResult<IEnumerable<User>> users = controller.GetEmployees();
//            Assert.AreNotEqual(0, users.Value.Count());
//        }
//
//        [Test]
//        public void AddUserResponseTest()
//        {
//            var controller = new UsersController();
//            ActionResult<User> user = controller.Post(_user);
//            Assert.True(user.Value.Id > 0);
//        }
//
//        [Test]
//        public void GetUserTest()
//        {
//            var controller = new UsersController();
//            ActionResult<User> user = controller.Get(1);
//            Assert.True((user.Value.Id == 1) && (user.Value.Roles.Count > 0));
//        }
//
//        [Test]
//        public void UserNotFoundTest()
//        {
//            Assert.Throws<UserNotFoundException>(() => UserRepository.GetUserById(100));
//        }
//
//        [Test]
//        public void ModifyUserDbTest()
//        {
//            var user = UserRepository.AddUser(_user);
//            user.Name = "Francisco";
//            long id = UserRepository.UpdateUser(user, user.Id);
//            Assert.AreEqual(user.Id, id);
//        }
//
//        [Test]
//        public void ModifyUserResponseTest()
//        {
//            var controller = new UsersController();
//            ActionResult<User> user = controller.Post(_user);
//            user.Value.Name = "Francisco";
//            ActionResult<int> id = controller.Put(user.Value.Id, user.Value);
//            Assert.AreEqual(user.Value.Id, id.Value);
//        }

    }
}