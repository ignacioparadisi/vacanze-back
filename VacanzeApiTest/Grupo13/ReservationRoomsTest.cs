using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Persistence.Repository;

namespace vacanze_back.VacanzeApiTest.Grupo13
{
    [TestFixture]
    public class ReservationRoomsTest
    {
        ReservationRoom reservation;
        ReservationRoomRepository _connection;
        DateTime time;
        private int id;

        [SetUp]
        public void SetUp()
        {
            _connection = new ReservationRoomRepository();
            time = new DateTime(1990, 04, 01);
            DateTime time2 = new DateTime(1990, 04, 01);
            
            Hotel hotel = new Hotel();
            hotel.IsActive = true;
            hotel.Name = "PruebaUnitaria";
            hotel.Phone = "04141234323";
            hotel.PricePerRoom = 13;
            hotel.RoomCapacity = 5;
            hotel.Stars = 5;
            hotel.AmountOfRooms = 30;
            hotel.AddressSpecification = "PU al lado de X";
            hotel.Picture = "PU";
            hotel.Website = "Pu Website";
            hotel.Location = LocationRepository.GetLocationById(1);
            hotel.Id = 1;
            reservation = new ReservationRoom(1, time, time2,hotel,1);
        }

        [Test,Order(1)]
        public void GetReservationsRoomTest()
        {
            List<Entity> reservations = _connection.GetRoomReservations();
            Assert.AreNotEqual(0, reservations.Count());
        }

        [Test, Order(2)]
        public void GetReservationsRoomByIdTest()
        {
            Assert.IsNotNull(_connection.Find(11));
        }

        [Test, Order(3)]
        public void GetAllByUserIdTest()
        {
            List<Entity> reservations = _connection.GetAllByUserId(1);
            Assert.AreNotEqual(0, reservations.Count());
        }


        [Test, Order(4)]
        public void AddReservationsRoomTest()
        {
            id = _connection.Add(reservation);
            Assert.True(id > 0);
        }

        /*
        [Test, Order(5)]
        public void UpdateTest()
        {
            DateTime date = new DateTime(1999, 04, 01);
            reservation.CheckIn = date;
            _connection.Update(reservation);
            ReservationAutomobile reservation2 = (ReservationAutomobile)_connection.Find(id);
            Assert.True(reservation2.CheckIn==date);

        }
*/

        [Test, Order(5)]
        public void DeleteReservationRoomTest()
        {
            _connection.Delete(reservation);
            Entity reservation2 = _connection.Find((int)reservation.Id);
            Assert.IsNull(reservation2);
        }

        [Test, Order(6)]
        public void AvailableRoomsTest()
        {
            id = ReservationRoomRepository.GetAvailableRoomReservations(1);
            Assert.IsNotNull(id);
        }

    }

}
