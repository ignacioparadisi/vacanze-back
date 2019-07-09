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
    public class ReservationAutomobileDAOTest
    {
        private Auto _vehicle;
        private Entity _user;
        private Payment _payment;
        private ReservationAutomobile _reservationAutomobile;

        [OneTimeSetUp]
        public void SetUp()
        {
            UserDAO daoUser = DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetUserDAO();
            List<Role> roles = new List<Role>();
            roles.Add(new Role(1, "Client"));
            _user = new User(0, 25964266, "Fernando", "Consalvo", "fercon@gmail.com", "123456789", roles);
            _user = daoUser.AddUser(_user);
            LocationDAO daoLocation = DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetLocationDAO();
            _vehicle = new Auto("Audi", "A3", 4, true, "afwdqe", 100, "dweoijewijd", daoLocation.GetLocations()[0].Id);
            _vehicle._id = 5;
        }

        [Test]
        public void TestAddAutomobileReservationOk()
        {
            IReservationAutomobileDAO dao = DAOFactory.GetFactory(DAOFactory.Type.Postgres)
                .GetReservationAutomobileDAO();
            DateTime checkOut = new DateTime(2019, 10, 20);
           _reservationAutomobile = EntityFactory.CreateReservationAutomobile(0, DateTime.Now, checkOut, _user.Id, _vehicle);
            _reservationAutomobile = dao.AddReservation(_reservationAutomobile);
            Assert.NotZero(_reservationAutomobile.Id);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            UserDAO userDao = DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetUserDAO();
            userDao.DeleteUserById(_user.Id);
        }
    }
}