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
        DateTime time;

        [SetUp]
        public void SetUp()
        {
            _connection = new ReservationAutomobileRepository();
            time = new DateTime(1990, 04, 14);
            DateTime time2 = new DateTime(1990, 04, 14);
            reservation = new ReservationAutomobile(0, time, time2);

            Auto automobile = new Auto("Prueba", "Unitaria", 5, true, "licencia", 35, "mazda.jgp", 1);
            automobile.setId(1);
            reservation.Automobile = automobile;
            reservation.Fk_user = 2;
            reservation.Id = 27;
        }

        [Test, Order(1)]
        public void GetReservationsAutomobileTest()
        {
            List<Entity> reservations = _connection.GetAutomobileReservations();
            Assert.AreNotEqual(0, reservations.Count());
        }

        [Test, Order(2)]
        public void FindReservationAutomobileTest()
        {
            ReservationAutomobile reservation = (ReservationAutomobile) _connection.Find(1);
            Assert.IsNotNull(reservation);
        }

        [Test, Order(3)]
        public void GetAllByUserIdTest()
        {
            List<Entity> reservations = _connection.GetAllByUserId(1);
            Assert.AreNotEqual(0, reservations.Count());
        }

        [Test, Order(4)]
        public void AddReservationsAutomobileTest()
        {
            var reserv= _connection.AddReservation(reservation);
            Assert.IsNotNull(reserv);
        }

        [Test, Order(5)]
        public void UpdateTest()
        {
            reservation.CheckIn = new DateTime(1991, 04, 14);
            _connection.Update(reservation);
            var reservation2 = (ReservationAutomobile)_connection.Find(1);
            Assert.AreNotEqual(reservation2.CheckIn, time);
            
        }
        

        [Test, Order(6)]
        public void DeleteReservationAutomobileTest()
        {
            _connection.Delete(reservation);
            Entity reservation2 = _connection.Find((int)reservation.Id);
            Assert.IsNull(reservation2);
        }

    }
}
