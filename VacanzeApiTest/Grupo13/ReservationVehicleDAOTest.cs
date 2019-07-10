using System;
using System.Collections.Generic;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo11;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo2;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo5;

namespace vacanze_back.VacanzeApiTest.Grupo13
{
    [TestFixture]
    public class ReservationVehicleDAOTest
    {
        private User _user;
        private ReservationVehicle _reservationAutomobile;
        private IReservationVehicleDAO dao;

        [OneTimeSetUp]
        public void SetUp()
        {
            UserDAO daoUser = DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetUserDAO();
            List<Role> roles = new List<Role>();
            roles.Add(new Role(1, "Client"));
            _user = EntityFactory.CreateUser(0, 25964266, "Fernando", "Consalvo", "fercon@gmail.com", "123456789",
                roles);
            _user = daoUser.AddUser(_user);
            dao = DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetReservationVehicleDAO();
        }

        [Test]
        [Order(1)]
        public void TestAddVehicleReservationOk()
        {
            DateTime checkOut = new DateTime(2019, 11, 20);
            _reservationAutomobile =
                EntityFactory.CreateReservationVehicle(0, DateTime.Now, checkOut, 5, _user.Id);
            _reservationAutomobile = dao.AddReservation(_reservationAutomobile);
            Assert.NotZero(_reservationAutomobile.Id);
        }

        [Test]
        [Order(2)]
        public void TestFindVehicleOk()
        {
            Assert.AreEqual(_reservationAutomobile.Id, dao.Find(_reservationAutomobile.Id).Id);
        }

        [Test]
        [Order(3)]
        public void TestFindReservationVehicleByUserId()
        {
            Assert.IsNotEmpty(dao.GetAllByUserId(_user.Id));
        }

        [Test]
        [Order(4)]
        public void TestUpdateReservationVehicleOk()
        {
            _reservationAutomobile.CheckIn = new DateTime(2019, 10, 20);
            Assert.AreEqual(_reservationAutomobile.CheckIn, dao.Update(_reservationAutomobile).CheckIn);
        }

        [Test]
        [Order(5)]
        public void TestDeleteReservation()
        {
            dao.Delete(_reservationAutomobile.Id);
            Assert.AreEqual(0, dao.Find(_reservationAutomobile.Id).Id);
        }


        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            factory.GetUserDAO().DeleteUserById(_user.Id);
        }
    }
}