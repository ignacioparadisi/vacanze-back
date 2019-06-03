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

        [SetUp]
        public void SetUp()
        {
            _connection = new ReservationRoomRepository();
            time = new DateTime(1990, 04, 14);
            DateTime time2 = new DateTime(1990, 04, 14);
            ReservationRoom reservation = new ReservationRoom(0,time,time2);
            Hotel hotel = new Hotel();
            hotel.Id = 1;
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
            hotel.Id = 0;
            reservation.Hotel = hotel;
            reservation.Fk_user = 2;
            reservation.Id = 27;
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
            Assert.IsNotNull(_connection.Find(1));
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
            var reserv = _connection.Add(reservation);
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
