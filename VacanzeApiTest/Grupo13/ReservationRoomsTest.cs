using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;

namespace vacanze_back.VacanzeApiTest.Grupo13
{
    [TestFixture]
    public class ReservationRoomsTest
    {
        ReservationRoom reservation;
        ReservationRoomRepository reservationRoomConnection;
        [SetUp]
        public void SetUp()
        {
            reservationRoomConnection = new ReservationRoomRepository();
            DateTime time = new DateTime(1990, 04, 14);
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
            //Location location = new Location();
            //hotel.location = location;
            hotel.Id = 0;
            reservation.Hotel = hotel;
            reservation.Fk_user = 2;
            reservation.Id = 27;
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

 
        [Test]
        public void AddReservationsRoomTest()
        {
            List<Entity> reservations = reservationRoomConnection.GetRoomReservations();
            reservationRoomConnection.Add(reservation);
            List<Entity> reservationslater = reservationRoomConnection.GetRoomReservations();
            Assert.Greater(reservationslater.Count, reservations.Count);
        }

    }

}
