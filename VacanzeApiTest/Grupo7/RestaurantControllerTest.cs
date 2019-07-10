
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo7;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo7;

namespace vacanze_back.VacanzeApiTest.Grupo7
{
    [TestFixture]
    public class RestaurantsControllerTest
    {
        private RestaurantsController _restaurantsController;
        RestaurantDto _restaurantDto;
        private List<int> _addedRestaurantList;

        [SetUp]
        public void Setup()
        {
            _restaurantsController = new RestaurantsController(null);
            _restaurantDto = new RestaurantDto();
            _restaurantDto.Name = "Yosemite";
            _restaurantDto.Capacity = 200;
            _restaurantDto.IsActive = true;
            _restaurantDto.Qualify = 4;
            _restaurantDto.Specialty = "Tsingy";
            _restaurantDto.Price = 21;
            _restaurantDto.BusinessName = "Wadsdworth Longfellow";
            _restaurantDto.Picture = "A great picture of the Dead Sea could be here, you know?";
            _restaurantDto.Description =
                "Listen, strange women lying in ponds distributing swords is no basis for a system of government… You can’t expect to wield supreme power just ‘cause some watery tart threw a sword at you!";
            _restaurantDto.Phone = "021221546352";
            _restaurantDto.Location = 1;
            _restaurantDto.Address = "As it turns out, Mount Kilimanjaro is not wi-fi enabled, so I had to spend two weeks in Tanzania talking to the people on my trip.";
            _addedRestaurantList = new List<int>();
        }

        [Test]

        public void PostRestaurantTest()
        {
            var result = _restaurantsController.PostRestaurant(_restaurantDto).Value;
            _addedRestaurantList.Add(result.Id);
            Assert.AreEqual(_restaurantDto.Name,result.Name);
        }

        [Test]

        public void PostRestaurantTest_InvalidAttributeExeption_ReturnsBadRequest()
        {
            _restaurantDto.Capacity = 0;
            Assert.IsInstanceOf<BadRequestObjectResult>(_restaurantsController.PostRestaurant(_restaurantDto).Result);
        }
        
        [Test]

        public void GetRestaurantsTest()
        {
            Assert.IsInstanceOf<List<RestaurantDto>>(_restaurantsController.Get().Value);
        }

        [Test]
        public void GetRestaurantsByLocation()
        {
            var addedRestaurant = _restaurantsController.PostRestaurant(_restaurantDto).Value;
            _addedRestaurantList.Add(addedRestaurant.Id);
            Assert.IsInstanceOf<List<RestaurantDto>>(_restaurantsController.GetRestaurantByLocation(addedRestaurant.Location).Value);
        }

        [Test]
        public void GetRestaurantTest()
        {
            var addedRestaurant = _restaurantsController.PostRestaurant(_restaurantDto).Value;
            _addedRestaurantList.Add(addedRestaurant.Id);
            var result = _restaurantsController.GetRestaurant(addedRestaurant.Id).Value;
            Assert.AreEqual(addedRestaurant.Id,result.Id);
        }
        
        [Test]

        public void GetRestaurantTest_RestaurantNotFoundExeption_ReturnBadRequest()
        {
            Assert.IsInstanceOf<BadRequestObjectResult>(_restaurantsController.GetRestaurant(-1).Result);
        }

        [Test]

        public void PutRestaurantTest()
        {
            var addedRestaurant = _restaurantsController.PostRestaurant(_restaurantDto).Value;
            _addedRestaurantList.Add(addedRestaurant.Id);
            _restaurantDto.Id = addedRestaurant.Id;
            _restaurantDto.Name = "Updated";
            var result = _restaurantsController.PutRestaurant(_restaurantDto).Value;
            Assert.AreEqual(_restaurantDto.Name,result.Name);
        }
        
        [Test]

        public void PutRestaurantTest_InvalidAttributeExeption_ReturnsBadRequest()
        {
            _restaurantDto.Capacity = 0;
            Assert.IsInstanceOf<BadRequestObjectResult>(_restaurantsController.PutRestaurant(_restaurantDto).Result);
        }

        [Test]

        public void PutRestaurantTest_RestaurantNotFoundExeption_ReturnBadRequest()
        {
            _restaurantDto.Id = -1;
            Assert.IsInstanceOf<BadRequestObjectResult>(_restaurantsController.PutRestaurant(_restaurantDto).Result);
        }
        
        [Test]
        public void DeleteRestaurantTest_ReturnsOkResult()
        {
            var id = _restaurantsController.PostRestaurant(_restaurantDto).Value.Id;
            Assert.IsInstanceOf<OkObjectResult>(_restaurantsController.DeleteRestaurant(id));
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var restaurantId in _addedRestaurantList)
            {
                _restaurantsController.DeleteRestaurant(restaurantId);
            }
        }
    }
}