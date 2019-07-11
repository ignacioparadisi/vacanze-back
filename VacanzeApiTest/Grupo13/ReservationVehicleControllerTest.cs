using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo13;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo13;

namespace vacanze_back.VacanzeApiTest.Grupo13
{
    public class ReservationVehicleControllerTest
    {
        private ReservationVehiclesController _controller;
        private ReservationVehicle _reservation;
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

        [SetUp]
        public void Setup()
        {
            _insertedReservations = new List<int>();
            
            DateTime checkin = new DateTime(2019,7,10);
            DateTime checkout = new DateTime(2019,7,12);
            
            _factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            _controller = new ReservationVehiclesController(null);
            
            CreateUser();

            _reservation = EntityFactory.CreateReservationVehicle(0, checkin, checkout, 1, _user.Id);
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var id in _insertedReservations)
            {
                _factory.GetReservationRoomDAO().Delete(id);
            }
            _factory.GetUserDAO().DeleteUserById(_user.Id);
        }
        
        [Test]
        public void AddSuccess()
        {
            var resroomMapper = MapperFactory.CreateReservationVehicleMapper();
            var result = _controller.Post(resroomMapper.CreateDTO(_reservation));
            var okObject = (OkObjectResult) result.Result;
            var idToDelete = ((ReservationVehicleDTO) okObject.Value).Id;
            _insertedReservations.Add(idToDelete);
            Assert.IsInstanceOf<OkObjectResult>(okObject);
        }

        [Test]
        public void AddBadRequest()
        {
            var resroomMapper = MapperFactory.CreateReservationVehicleMapper();
            _reservation.CheckOut = new DateTime();
            var result = _controller.Post(resroomMapper.CreateDTO(_reservation));
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void FindSuccess()
        {
            _reservation = _factory.GetReservationVehicleDAO().AddReservation(_reservation);
            _insertedReservations.Add(_reservation.Id);
            var result = _controller.Get(_reservation.Id);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void FindBadRequest()
        {
            var result = _controller.Get(0);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void DeleteSuccess()
        {
            _reservation = _factory.GetReservationVehicleDAO().AddReservation(_reservation);
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
            _reservation = _factory.GetReservationVehicleDAO().AddReservation(_reservation);
            _insertedReservations.Add(_reservation.Id);
            var date = DateTime.Now;
            _reservation.CheckIn = date;
            var mapper = MapperFactory.CreateReservationVehicleMapper();
            var result = _controller.Put(mapper.CreateDTO(_reservation));
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void GetReservationVehiclesForUser()
        {
            _reservation = _factory.GetReservationVehicleDAO().AddReservation(_reservation);
            _insertedReservations.Add(_reservation.Id);
            var result = _controller.GetAllByUserID(_user.Id);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void GetReservationRoomsForUserBadRequest()
        {
            var result = _controller.GetAllByUserID(-1);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
    }
}