using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Persistence.Connection.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities;

namespace vacanze_back.VacanzeApiTest.Grupo13
{
    [TestFixture]
    public class ReservationRoomsTest
    {
        ReservationRoomConnection reservationRoomConnection;
        [SetUp]
        public void SetUp()
        {
            reservationRoomConnection = new ReservationRoomConnection();
        }

        [Test]
        public void GetReservationsRoomTest()
        {
            List<Entity> reservations = reservationRoomConnection.getRoomReservations();
            Assert.AreNotEqual(0, reservations.Count());
        }

    }

}
