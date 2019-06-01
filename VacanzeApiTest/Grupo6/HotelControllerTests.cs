using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo6;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApiTest.Grupo6
{
    [TestFixture]
    public class HotelControllerTests
    {
        private HotelsController hotelsController;
        [SetUp]
        public void Setup()
        {
            hotelsController = new HotelsController();
        }
        [Test]
        public void GetHotelsTest()
        {
            ActionResult<IEnumerable<Hotel>> hotels = hotelsController.Get();
            Assert.NotNull(hotels);
        }
        [Test]
        public void GetHotelsByLocationTest()
        {
            ActionResult<IEnumerable<Hotel>> hotels = hotelsController.Get(1);
            Assert.NotNull(hotels.Value);
        }
        [Test]
        public void GetHotelById()
        {
            ActionResult<Hotel> hotel = hotelsController.GetById(2);
            Assert.NotNull(hotel.Value);
        }
        [Test]
        public void GetNotExistingHotelById()
        {
            ActionResult<Hotel> hotel = hotelsController.GetById(1000);
            Assert.IsInstanceOf<NotFoundResult>(hotel.GetType());
        }
        [Test]
        public void GetHotelsEqualsLocationNullTest()
        {
            ActionResult<IEnumerable<Hotel>> hotels = hotelsController.Get();
            ActionResult<IEnumerable<Hotel>> _hotels = hotelsController.Get(-1);
            Assert.AreEqual(hotels.Value.ToString(), _hotels.Value.ToString());
        }
        

        
    }

}
