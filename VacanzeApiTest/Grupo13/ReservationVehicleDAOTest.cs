using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Internal;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo11;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo2;
using vacanze_back.VacanzeApi.Persistence.DAO.Locations;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo5;


namespace vacanze_back.VacanzeApiTest.Grupo13
{
    [TestFixture]
    public class ReservationVehicleDAOTest
    {
        private User _user;
        private Payment _payment;
        private ReservationVehicle _reservationAutomobile;

        [OneTimeSetUp]
        public void SetUp()
        {
            UserDAO daoUser = DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetUserDAO();
            List<Role> roles = new List<Role>();
            roles.Add(new Role(1, "Client"));
            _user = EntityFactory.CreateUser(0, 25964266, "Fernando", "Consalvo", "fercon@gmail.com", "123456789",
                roles);
            _user = daoUser.AddUser(_user);

        }

        [Test]
        [Order(1)]
        public void TestAddVehicleReservationOk()
        {
            IReservationVehicleDAO dao = DAOFactory.GetFactory(DAOFactory.Type.Postgres)
                .GetReservationVehicleDAO();
            DateTime checkOut = new DateTime(2019, 10, 20);
            _reservationAutomobile =
                EntityFactory.CreateReservationVehicle(0, DateTime.Now, checkOut, _user.Id, 5);
            _reservationAutomobile = dao.AddReservation(_reservationAutomobile);
            Assert.NotZero(_reservationAutomobile.Id);
        }

        [Test]
        [Order(2)]
        public void TestFindVehicleOk()
        {
            var dao = DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetReservationVehicleDAO();
            Assert.AreEqual(_reservationAutomobile.Id, dao.Find(_reservationAutomobile.Id).Id);
        }

        [Test]
        [Order(3)]
        public void TestFindReservationVehicleByUserId()
        {
            var dao = DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetReservationVehicleDAO();
            Assert.IsNotEmpty(dao.GetAllByUserId(_user.Id));
        }

        [Test]
        [Order(4)]
        public void TestDeleteReservation()
        {
            var dao = DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetReservationVehicleDAO();
            dao.Delete(_reservationAutomobile.Id);
            Assert.AreEqual(0, dao.Find(_reservationAutomobile.Id).Id);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            UserDAO userDao = DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetUserDAO();
            userDao.DeleteUserById(_user.Id);
        }
    }
}