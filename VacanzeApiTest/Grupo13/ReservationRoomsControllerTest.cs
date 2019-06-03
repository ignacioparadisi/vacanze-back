using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo13;
using vacanze_back.VacanzeApi.Common.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities.Grupo13;
using vacanze_back.VacanzeApi.Persistence.Repository;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo13;

namespace vacanze_back.VacanzeApiTest.Grupo13
{
    [TestFixture]
    public class ReservationRoomsControllerTest
    {

        private ReservationRoomsController controller;
        private ReservationRoom reservation;

        [SetUp]
        public void SetUp()
        {

            controller = new ReservationRoomsController();
            DateTime time = new DateTime(1990, 04, 14);
            DateTime time2 = new DateTime(1990, 04, 14);
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
            hotel.Id = 0;
            reservation = new ReservationRoom(0, time, time2, hotel, 2);
        }

        [Test, Order(1)]
        public void GetAllByUserIDTest()
        {

            var result = controller.GetAllByUserID();
            Assert.IsInstanceOf<ActionResult<IEnumerable<Entity>>>(result);
        }

        [Test, Order(2)]
        public void GetFindTest()
        {

            var result = controller.Find(999);
            Assert.IsInstanceOf<ActionResult<Entity>>(result);
        }

        [Test, Order(3)]
        public void PostTest()
        {

            controller = new ReservationRoomsController();
            DateTime time = new DateTime(1990, 04, 14);
            DateTime time2 = new DateTime(1990, 04, 14);
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
            hotel.Id = 0;
            reservation = new ReservationRoom(0, time, time2, hotel, 2);

            var result = controller.Post(reservation);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);

        }

        [Test, Order(4)]
        public void DeleteTest()
        {
            controller = new ReservationRoomsController();
            DateTime time = new DateTime(1990, 04, 14);
            DateTime time2 = new DateTime(1990, 04, 14);
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
            hotel.Id = 0;
            reservation = new ReservationRoom(0, time, time2, hotel, 2);
            ReservationRoomRepository connection = new ReservationRoomRepository();
            int id = connection.Add(reservation);

            var result = controller.Delete(id);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }
    }
/*
    [Test, Order(4)]
    public void PutTest()
    {
        var res = controller.Put(reservation);
        Assert.IsInstanceOf<OkObjectResult>(res.Result);
    }*/

}
