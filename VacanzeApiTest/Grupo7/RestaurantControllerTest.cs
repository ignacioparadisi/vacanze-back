using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo7;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo7;

namespace vacanze_back.VacanzeApiTest.Grupo7
{
    [TestFixture]
    public class RestaurantsControllerTest
    {
        private RestaurantsController restaurantsController;
        private int id;
        Restaurant restaurant;

        [SetUp]
        public void Setup()
        {
            restaurantsController = new RestaurantsController();
            restaurant = new Restaurant( "Yosemite", 
                            200, 
                            true, 
                            4, 
                            "Tsingy", 
                            21, 
                            "Wadsdworth Longfellow", 
                            "A great picture of the Dead Sea could be here, you know?", 
                            "Listen, strange women lying in ponds distributing swords is no basis for a system of government… You can’t expect to wield supreme power just ‘cause some watery tart threw a sword at you!",
                            "021221546352", 
                            1, 
                            "As it turns out, Mount Kilimanjaro is not wi-fi enabled, so I had to spend two weeks in Tanzania talking to the people on my trip.");
            id = RestaurantRepository.AddRestaurant(restaurant);
            restaurant.Id = id;
        }

//        [Test]
//        public void CreateRestaurantTest()
//        {
//            ActionResult<Restaurant> res = restaurantsController.PostRestaurant((restaurant);
//            RestaurantRepository.DeleteRestaurant(id+1);
//            Assert.NotNull(res);
//        }

        [Test]
        public void GetRestaurantsTest()
        {
            ActionResult<IEnumerable<Restaurant>> res = restaurantsController.Get();
            Assert.NotNull(res);
        }

        [Test]
        public void GetRestaurantTest()
        {
            Assert.IsInstanceOf<IActionResult>(restaurantsController.GetRestaurant(id));
        }

        [Test]
        public void GetRestaurantsByLocationTest()
        {
            ActionResult<IEnumerable<Restaurant>> res = restaurantsController.GetRestaurantByLocation(1);
            Assert.NotNull(res);
        }

//        [Test]
////        public void PutRestaurantTest()
////        {
////            ActionResult<Restaurant> res = restaurantsController.PutRestaurant(restaurant);
////            Assert.NotNull(res);
////        }

        [Test]
//        public void DeleteRestaurantTest()
//        {
//            ActionResult<int> res = restaurantsController.DeleteRestaurant(id);
//            id = 0;
//            Assert.NotNull(res);
//        }

        [TearDown]
        public void TearDown()
        {
            if (id != 0)
            {
                id = RestaurantRepository.DeleteRestaurant(id);
            }
                
        }       
    }
}