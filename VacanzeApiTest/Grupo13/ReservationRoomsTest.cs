using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities;

namespace vacanze_back.VacanzeApiTest.Grupo13
{
    [TestFixture]
    public class ReservationRoomsTest
    {
        ReservationRoomRepository reservationRoomConnection;
        [SetUp]
        public void SetUp()
        {
            reservationRoomConnection = new ReservationRoomRepository();
        }

        [Test]
        public void GetReservationsRoomTest()
        {
            List<Entity> reservations = reservationRoomConnection.GetRoomReservations();
            Assert.AreNotEqual(0, reservations.Count());
        }

        //PIEDRERA
        [Test]
        public void GetReservationsRoomByIdTest()
        {
            ReservationRoomRepository repository = new ReservationRoomRepository();
            Assert.IsNotNull(repository.Find(11));
        }

        /*
        [Test]
        public void AddReservationsRoomTest()
        {
            List<Entity> reservations = reservationRoomConnection.GetRoomReservations();
          //  reservationRoomConnection.Add(reservation);
            List<Entity> reservationslater = reservationRoomConnection.GetRoomReservations();
            Assert.Greater(reservationslater.Count, reservations.Count);
        }
        */
    }

}
