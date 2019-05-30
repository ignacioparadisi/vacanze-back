using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;

namespace vacanze_back.VacanzeApiTest.Grupo13
{
    [TestFixture]
    public class ReservationAutomobileTest
    {
        ReservationAutomobile reservation;

        ReservationAutomobileRepository _connection;
        [SetUp]
        public void SetUp()
        {
            _connection = new ReservationAutomobileRepository();
            DateTime time = new DateTime(1994, 04, 14);
            DateTime time2 = new DateTime(1995, 04, 14);
            reservation = new ReservationAutomobile(0, time, time2);
            Auto automobile = new Auto("Mazda", "X3", 5, true, "XYZ23D", 35, "mazda.jgp", 1);
            User user = new User(3, 99, "NameTest", "LastTest", "EmailTest");
            reservation.Automobile = automobile;
            reservation.User = user;
            //reservation.Automobile.Id = 1;
           // reservation.User.Id = 3;
        }

        [Test]
        public void GetReservationsAutomobileTest()
        {
            List<Entity> reservations = _connection.GetAutomobileReservations();
            Assert.AreNotEqual(0, reservations.Count());
        }

        [Test]
        public void FindReservationAutomobileTest()
        {
            ReservationAutomobile reservation = (ReservationAutomobile) _connection.Find(3);
            Assert.IsNotNull(reservation);
        }


        [Test]
        public void AddReservationsAutomobileTest()
        {
            List<Entity> reservations = _connection.GetAutomobileReservations();
            _connection.AddReservation(reservation);
            List<Entity> reservationslater = _connection.GetAutomobileReservations();
            Assert.Greater(reservationslater.Count, reservations.Count);
        }

    }
}
