using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo13;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;

namespace vacanze_back.VacanzeApiTest.Grupo13
{
    [TestFixture]
    public class ReservationRoomsControllerTest
    {

        private ReservationRoomsController _controller;
        private ReservationRoom _reservation;
        private Hotel _hotel;
        private User _user;
        private List<int> _insertedReservations;
        private DAOFactory _factory;

        private void CreateUser()
        {
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
            _controller = new ReservationRoomsController(null);
            
            CreateUser();
            CreateHotel();

            _reservation = EntityFactory.CreateReservationRoom(0, checkin, checkout, _hotel.Id, _user.Id);
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var id in _insertedReservations)
            {
                _factory.GetReservationRoomDAO().Delete(id);
            }
            _factory.GetHotelDAO().DeleteHotel(_hotel.Id);
            _factory.GetUserDAO().DeleteUserById(_user.Id);
        }
        
        [Test]
        public void AddSuccess()
        {
            var resroomMapper = MapperFactory.CreateReservationRoomMapper();
            var result = _controller.Post(resroomMapper.CreateDTO(_reservation));
            var okObject = (OkObjectResult) result.Result;
            var idToDelete = ((ReservationRoomDTO) okObject.Value).Id;
            _insertedReservations.Add(idToDelete);
            Assert.IsInstanceOf<OkObjectResult>(okObject);
        }

        [Test]
        public void AddBadRequest()
        {
            var resroomMapper = MapperFactory.CreateReservationRoomMapper();
            _reservation.CheckOut = DateTime.Now;
            _reservation.CheckIn = DateTime.Now;
            var result = _controller.Post(resroomMapper.CreateDTO(_reservation));
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void FindSuccess()
        {
            _reservation = _factory.GetReservationRoomDAO().Add(_reservation);
            _insertedReservations.Add(_reservation.Id);
            var result = _controller.Find(_reservation.Id);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void FindBadRequest()
        {
            var result = _controller.Find(0);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void DeleteSuccess()
        {
            _reservation = _factory.GetReservationRoomDAO().Add(_reservation);
            var result = _controller.Delete(_reservation.Id);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void DeleteBadRequest()
        {
            var result = _controller.Delete(0);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result); 
        }

        [Test]
        public void UpdateSuccess()
        {
            _reservation = _factory.GetReservationRoomDAO().Add(_reservation);
            _insertedReservations.Add(_reservation.Id);
            var date = DateTime.Now;
            _reservation.CheckIn = date;
            var mapper = MapperFactory.CreateReservationRoomMapper();
            var result = _controller.Put(mapper.CreateDTO(_reservation));
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void GetReservationRoomsForUser()
        {
            _reservation = _factory.GetReservationRoomDAO().Add(_reservation);
            _insertedReservations.Add(_reservation.Id);
            var result = _controller.GetReservationRoomsForUser(_user.Id);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void GetReservationRoomsForUserBadRequest()
        {
            var result = _controller.GetReservationRoomsForUser(-1);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
    }
}
