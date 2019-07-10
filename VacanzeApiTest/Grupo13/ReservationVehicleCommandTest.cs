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
        public void TestFindReservationByUserIdException()
        {
            CommandResult<List<ReservationVehicle>> command =
                CommandFactory.CreateGetReservationVehicleByUserCommand(_user.Id);
            Assert.Throws<UserDoesntHaveReservationsException>(() => command.Execute());
        }

        [Test]
        [Order(2)]
        public void TestAddReservationWithoutUserException()
        {
            _reservation = EntityFactory.CreateReservationVehicle(0, DateTime.Now, _checkOut, 5, 0);
            CommandResult<ReservationVehicle> command = CommandFactory.CreateAddReservationVehicleCommand(_reservation);
            Assert.Throws<ReservationHasNoUserException>(() => command.Execute());
        }

        [Test]
        [Order(3)]
        public void TestAddReservationWithoutVehicleException()
        {
            _reservation = EntityFactory.CreateReservationVehicle(0, DateTime.Now, _checkOut, 0, _user.Id);
            CommandResult<ReservationVehicle> command = CommandFactory.CreateAddReservationVehicleCommand(_reservation);
            Assert.Throws<ReservationHasNoVehicleException>((() => command.Execute()));
        }

        [Test]
        [Order(4)]
        public void TestAddReservationWithoutCheckinException()
        {
            _reservation = EntityFactory.CreateReservationVehicle(0, new DateTime(), _checkOut, 5, _user.Id);
            CommandResult<ReservationVehicle> command =
                CommandFactory.CreateAddReservationVehicleCommand(_reservation);
            Assert.Throws<ReservationHasNoCheckInException>((() => command.Execute()));
        }
        
        [Test]
        [Order(5)]
        public void TestAddReservationWithoutCheckOutException()
        {
            _reservation = EntityFactory.CreateReservationVehicle(0, DateTime.Now, new DateTime(), 5, _user.Id);
            CommandResult<ReservationVehicle> command =
                CommandFactory.CreateAddReservationVehicleCommand(_reservation);
            Assert.Throws<ReservationHasNoCheckOutException>(() => command.Execute());
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

        [Test]
        [Order(7)]
        public void TestFindReservationException()
        {
            CommandResult < ReservationVehicle > command = CommandFactory.CreateFindReservationVehicleCommand(0);
            Assert.Throws<VehicleReservationNotFoundException>(() => command.Execute());
        }

        [Test]
        [Order(8)]
        public void TestFindReservationOk()
        {
            CommandResult<ReservationVehicle> command =
                CommandFactory.CreateFindReservationVehicleCommand(_reservation.Id);
            command.Execute();
            var reservation = command.GetResult();
            Assert.AreEqual(_reservation.Id, reservation.Id);
        }

        [Test]
        [Order(9)]
        public void TestGetReservationsByUserOk()
        {
            CommandResult<List<ReservationVehicle>> command =
                CommandFactory.CreateGetReservationVehicleByUserCommand(_user.Id);
            command.Execute();
            List<ReservationVehicle> reservations = command.GetResult();
            Assert.AreEqual(_reservation.Id, reservations.ToArray()[0].Id);
        }

        [Test]
        [Order(10)]
        public void TestUpdateReservationOk()
        {
            
            _reservation.CheckIn = _reservation.CheckIn.AddDays(2);
            CommandResult<ReservationVehicle> command =
                CommandFactory.CreateUpdateReservationVehicleCommand(_reservation);
            command.Execute();
            var reservation = command.GetResult();
            Assert.AreEqual(_reservation.CheckIn, reservation.CheckIn);
        }
        
        [Test]
        [Order(11)]
        public void TestUpdateReservationWithoutCheckInException()
        {
            _reservation.CheckIn = new DateTime();
            CommandResult<ReservationVehicle> command = CommandFactory.CreateUpdateReservationVehicleCommand(_reservation);
            Assert.Throws<ReservationHasNoCheckInException>(() => command.Execute());
        }

        [Test]
        [Order(12)]
        public void TestUpdateReservationWithoutCheckOutException()
        {
            _reservation.CheckIn = DateTime.Now;
            _reservation.CheckOut = new DateTime();
            CommandResult<ReservationVehicle> command = CommandFactory.CreateUpdateReservationVehicleCommand(_reservation);
            Assert.Throws<ReservationHasNoCheckOutException>(() => command.Execute());
            
        }

        [Test]
        [Order(13)]
        public void TestDeleteReservationOk()
        {
            CommandResult<int> command = CommandFactory.CreateDeleteReservationVehicleCommand(_reservation.Id);
            command.Execute();
            Assert.AreEqual(_reservation.Id, command.GetResult());
        }
        
        [Test]
        [Order(14)]
        public void TestDeleteReservationException()
        {
            CommandResult<int> command = CommandFactory.CreateDeleteReservationVehicleCommand(_reservation.Id);
            Assert.Throws<VehicleReservationNotFoundException>(() => command.Execute());
        }
        
        [OneTimeTearDown]
        public void TearDownAfterTests()
        {
            UserDAO dao = DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetUserDAO();
            dao.DeleteUserById(_user.Id);
        }
    }
}