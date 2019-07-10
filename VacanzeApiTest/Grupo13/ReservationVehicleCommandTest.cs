using System;
using System.Collections.Generic;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo13;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo2;
using Command = vacanze_back.VacanzeApi.Common.Command;

namespace vacanze_back.VacanzeApiTest.Grupo13
{
    [TestFixture]
    public class ReservationVehicleCommandTest
    {
        private User _user;
        private ReservationVehicle _reservation;
        private DateTime _checkOut;

        [OneTimeSetUp]
        public void SetUpForTests()
        {
            UserDAO daoUser = DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetUserDAO();
            List<Role> roles = new List<Role>();
            roles.Add(new Role(1, "Client"));
            _user = EntityFactory.CreateUser(0, 25964266, "Fernando", "Consalvo", "fercon97@gmail.com", "123456789",
                roles);
            _user = daoUser.AddUser(_user);
            _checkOut = DateTime.Now;
            _checkOut = _checkOut.AddDays(7);
        }

        [Test]
        [Order(1)]
        public void TestAddReservationWithoutUserException()
        {
            _reservation = EntityFactory.CreateReservationVehicle(0, DateTime.Now, _checkOut, 5, 0);
            CommandResult<ReservationVehicle> command = CommandFactory.CreateAddReservationVehicleCommand(_reservation);
            Assert.Throws<ReservationHasNoUserException>(() => command.Execute());
        }

        [Test]
        [Order(2)]
        public void TestAddReservationWithoutVehicleException()
        {
            _reservation = EntityFactory.CreateReservationVehicle(0, DateTime.Now, _checkOut, 0, _user.Id);
            CommandResult<ReservationVehicle> command = CommandFactory.CreateAddReservationVehicleCommand(_reservation);
            Assert.Throws<ReservationHasNoVehicleException>((() => command.Execute()));
        }

        [Test]
        [Order(3)]
        public void TestAddReservationWithoutCheckinException()
        {
            _reservation = EntityFactory.CreateReservationVehicle(0, new DateTime(), _checkOut, 5, _user.Id);
            CommandResult<ReservationVehicle> command =
                CommandFactory.CreateAddReservationVehicleCommand(_reservation);
            Assert.Throws<ReservationHasNoCheckInException>((() => command.Execute()));
        }
        
        [Test]
        [Order(4)]
        public void TestAddReservationWithoutCheckOutException()
        {
            _reservation = EntityFactory.CreateReservationVehicle(0, DateTime.Now, new DateTime(), 5, _user.Id);
            CommandResult<ReservationVehicle> command =
                CommandFactory.CreateAddReservationVehicleCommand(_reservation);
            Assert.Throws<ReservationHasNoCheckOutException>(() => command.Execute());
        }

        [Test]
        [Order(5)]
        public void TestFindReservationByUserIdException()
        {
            CommandResult<List<ReservationVehicle>> command =
                CommandFactory.CreateGetReservationVehicleByUserCommand(_user.Id);
            Assert.Throws<UserDoesntHaveReservationsException>(() => command.Execute());
        }

        
        [Test]
        [Order(6)]
        public void TestAddReservationOk()
        {
            _reservation = EntityFactory.CreateReservationVehicle(0, DateTime.Now, _checkOut, 5, _user.Id);
            CommandResult<ReservationVehicle> command = CommandFactory.CreateAddReservationVehicleCommand(_reservation);
            command.Execute();
            _reservation = command.GetResult();
            Assert.NotZero(_reservation.Id);
        }
        
        [OneTimeTearDown]
        public void TearDownAfterTests()
        {
            UserDAO dao = DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetUserDAO();
            dao.DeleteUserById(_user.Id);
        }
    }
}