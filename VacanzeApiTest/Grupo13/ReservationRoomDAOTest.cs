using System;
using System.Collections.Generic;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApiTest.Grupo13
{
    [TestFixture]
    public class ReservationRoomDAOTest
    {
        private ReservationRoom _reservation;
        private Hotel _hotel;
        private DAOFactory _factory;
        private User _user;
        private List<int> _insertedReservations;

        /// <summary>
        /// Crea el usuario en base de datos para usar este usuario en las pruebas
        /// </summary>
        private void CreateUser()
        {
            // TODO: Cambiar por el Factory de usuario
            var roles = new List<Role>();
            roles.Add(EntityFactory.CreateRole(1, "Cliente"));
            _user = EntityFactory.CreateUser(0, 23456789, "Pedro", "Perez",
                "cliente1@vacanze.com", "12345678", roles);
            _user = _factory.GetUserDAO().AddUser(_user);
        }

        /// <summary>
        /// Crea un hotel en la base de datos para usarlo en las pruebas
        /// </summary>
        private void CreateHotel()
        {
            _hotel = EntityFactory.CreateHotel(0,
                "Prueba",
                30,
                5,
                true,
                "Direccion",
                13,
                "www.google.com",
                "04141234567",
                "https://www.google.com/search?q=hotel&source=lnms&tbm=isch&sa=X&ved=0ahUKEwiE99f53abjAhXsmeAKHaa7COMQ_AUIECgB&biw=1280&bih=678&dpr=2#imgrc=_p5ca9DimO_r-M:",
                5, 1);
            
            _hotel.Id = _factory.GetHotelDAO().AddHotel(_hotel);
        }
        
        [SetUp]
        public void Setup()
        {
            _insertedReservations = new List<int>();
            
            DateTime checkin = new DateTime(2019,7,10);
            DateTime checkout = new DateTime(2019,7,12);
            
            _factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            
            CreateUser();
            CreateHotel();

            _reservation = EntityFactory.CreateReservationRoom(0, checkin, checkout, _hotel.Id, _user.Id);
        }

        [TearDown]
        public void TearDown()
        {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            
            foreach (var id in _insertedReservations)
            {
                factory.GetReservationRoomDAO().Delete(id);
            }
            factory.GetHotelDAO().DeleteHotel(_hotel.Id);
            factory.GetUserDAO().DeleteUserById(_user.Id);
        }

        [Test]
        public void CreateValidReservation()
        {
            DateTime checkin = new DateTime(2019,7,10);
            DateTime checkout = new DateTime(2019,7,12);
            ReservationRoom reservationRoom =
                EntityFactory.CreateReservationRoom(0, checkin, checkout, _hotel.Id, _user.Id);
            Assert.AreEqual(0, reservationRoom.Id);
        }

        [Test]
        public void CreateReservationNotValidId()
        {
            DateTime checkin = new DateTime(2019,7,10);
            DateTime checkout = new DateTime(2019,7,12);
            Assert.Throws<NotValidIdException>(() =>
            {
                EntityFactory.CreateReservationRoom(-1, checkin, checkout, _hotel.Id, _user.Id);
            });
        }
        
        [Test]
        public void CreateReservationNotValidHotelId()
        {
            DateTime checkin = new DateTime(2019,7,10);
            DateTime checkout = new DateTime(2019,7,12);
            Assert.Throws<NotValidIdException>(() =>
            {
                EntityFactory.CreateReservationRoom(0, checkin, checkout, -1, _user.Id);
            });
        }
        
        [Test]
        public void CreateReservationNotValidUserId()
        {
            DateTime checkin = new DateTime(2019,7,10);
            DateTime checkout = new DateTime(2019,7,12);
            Assert.Throws<NotValidIdException>(() =>
            {
                EntityFactory.CreateReservationRoom(0, checkin, checkout, _hotel.Id, -1);
            });
        }
        
        [Test]
        public void CreateReservationNotValidDatesId()
        {
            DateTime checkin = new DateTime(2019,7,12);
            DateTime checkout = new DateTime(2019,7,10);
            Assert.Throws<GeneralException>(() =>
            {
                EntityFactory.CreateReservationRoom(0, checkin, checkout, _hotel.Id, _user.Id);
            });
        }

        [Test]
        public void AddSuccess()
        {
            ReservationRoom reservationRoom = _factory.GetReservationRoomDAO().Add(_reservation);
            _insertedReservations.Add(reservationRoom.Id);
            Assert.True(reservationRoom.Id > 0);
        }
        
        [Test]
        public void FindSuccess()
        {
            _reservation = _factory.GetReservationRoomDAO().Add(_reservation);
            _insertedReservations.Add(_reservation.Id);
            ReservationRoom reservation = _factory.GetReservationRoomDAO().Find(_reservation.Id);
            Assert.AreEqual(reservation.Id, _reservation.Id);
        }

        [Test]
        public void GetAllByUserSuccess()
        {
            _reservation = _factory.GetReservationRoomDAO().Add(_reservation);
            _insertedReservations.Add(_reservation.Id);
            List<ReservationRoom> reservations = _factory.GetReservationRoomDAO().GetAllByUserId(_user.Id);
            Assert.True(reservations.Count > 0);
            Assert.AreEqual(reservations[0].Id, _reservation.Id);
        }

        [Test]
        public void DeleteSuccess()
        {
            _reservation = _factory.GetReservationRoomDAO().Add(_reservation);
            var id = _factory.GetReservationRoomDAO().Delete(_reservation.Id);
            Assert.AreEqual(_reservation.Id, id);
        }

        [Test]
        public void GetAvailableRoomReservationsSuccess()
        {
            var availableRooms = _factory.GetReservationRoomDAO().GetAvailableRoomReservations(_hotel.Id);
            Assert.True(availableRooms > 0);
        }
    }
}