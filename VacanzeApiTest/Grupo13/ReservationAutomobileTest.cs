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
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo5;

namespace vacanze_back.VacanzeApiTest.Grupo13
{
    [TestFixture]
    public class ReservationAutomobileTest
    {
        ReservationAutomobile reservation;
        ReservationAutomobileRepository _connection;
        DateTime time;
        private int id;
        Auto automobile = new Auto("Prueba", "Unitaria", 5, true, "licencia", 35, "mazda.jgp", 1);

        [SetUp]
        public void SetUp()
        {

            _connection = new ReservationAutomobileRepository();
            time = new DateTime(1990, 04, 14);
            DateTime time2 = new DateTime(1990, 04, 14);
            automobile.setId(7);
            reservation = new ReservationAutomobile(0, time, time2,automobile,2);
            reservation.Id = id;
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
            ReservationAutomobile reservation = (ReservationAutomobile) _connection.Find(68);
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
             id  = (int)_connection.AddReservation(reservation).Id;
             Assert.True(id > 0);
        }

        /*
        [Test, Order(5)]
        public void UpdateTest()
        {
            
            DateTime date = new DateTime(1991, 04, 14);
            ReservationAutomobile reservation2 = new ReservationAutomobile(reservation.Id, date, reservation.CheckOut, automobile, 2);
           // reservation.CheckIn = date;
           _connection.Update(reservation2);
           var reservation3 = (ReservationAutomobile)_connection.Find((int)reservation.Id);
           Assert.AreEqual(reservation2.CheckIn,date);
            
        }
    */
        

        [Test, Order(5)]
        public void DeleteReservationAutomobileTest()
        {
            _connection.Delete(reservation);
            Entity reservation2 = _connection.Find((int)reservation.Id);
            Assert.IsNull(reservation2);
        }



    }
}
