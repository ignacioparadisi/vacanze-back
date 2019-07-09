using System;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo6;

namespace vacanze_back.VacanzeApiTest.Grupo13
{
    [TestFixture]
    public class ReservationRoomDAOTest
    {
        private ReservationRoom _reservation;
        private IReservationRoomDAO _rest_room_dao;
        private DAOFactory _factory_rest_room_dao;
        private Hotel _hotel;
        private HotelDAO _hotel_dao;
        private DAOFactory _factory_hotel_dao;
        private User _user;
        
        [SetUp]
        public void Setup()
        {
           _factory_rest_room_dao = new PostgresDAOFactory();
           _rest_room_dao = _factory_rest_room_dao.GetReservationRoomDAO();
           DateTime checkin = new DateTime(2019,7,10);
           DateTime checkout = new DateTime(2019,7,12);
           _reservation = EntityFactory.CreateReservationRoom(1, checkin, checkout);
           _reservation.Id = _rest_room_dao.Add(_reservation);
        }

        [TearDown]
        public void TearDown()
        {
            
            int del = _rest_room_dao.Delete(_reservation);
        }
        
        [Test]
        public void FindSuccess()
        {
            var reservationtest = _rest_room_dao.Find(_reservation.Id);
            Assert.NotNull(reservationtest);  
        }
    }
}