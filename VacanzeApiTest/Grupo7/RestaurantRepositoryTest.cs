using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo7;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo7;
using System;

namespace vacanze_back.VacanzeApiTest.Grupo7
{
	[TestFixture]
	public class RestaurantRepositoryTest
	{
		RestaurantsController controller;
		Restaurant res;
        Restaurant secondRes;
        List<Restaurant> restaurantList;
		int id;
		[SetUp]
		public void setup()
		{
			controller = new RestaurantsController();
			res = new Restaurant( "Yosemite", 
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
            
            secondRes = new Restaurant( "Crater Lake", 
                            102, 
                            false, 
                            5, 
                            "Dover", 
                            21, 
                            "Sir Edmund Hillary", 
                            "It is not the mountain we conquer but ourselves. A picture of Mout Everest may lay here, you know?", 
                            "If facts don’t fit the theory, change the facts.",
                            "021221565489", 
                            3, 
                            "Not all who wander are lost. You will get here.");

		}

        [Test, Order(1)]
		public void AddRestaurantTest()
		{ 
			res.Id = RestaurantRepository.AddRestaurant(res);
            id = res.Id;
			Assert.True( res.Id > 0);
		}

        [Test, Order(2)]
		public void UpdateRestaurantTest()
		{   
            secondRes.Id = id;
			secondRes = RestaurantRepository.UpdateRestaurant(secondRes);
			Assert.AreNotEqual( secondRes.Name, res.Name);
		}

        [Test, Order(3)]
		public void GetRestaurantByIDTest()
		{   
			secondRes = RestaurantRepository.GetRestaurant(id);
			Assert.AreEqual( secondRes.Id, id);
		}

        [Test, Order(4)]
		public void GetAllRestaurantTest()
		{   
            restaurantList = RestaurantRepository.GetRestaurants();
			Assert.True( restaurantList.Count() > 0);
		}

		[Test, Order(5)]
		public void GetRestaurantByCityTest()
		{ 
			restaurantList = RestaurantRepository.GetRestaurantsByCity(3);
			Assert.True( restaurantList.Count() > 0);
		}

        [Test, Order(6)]
		public void DeleteRestaurantTest()
		{   
            var deletedid = RestaurantRepository.DeleteRestaurant(id);
			Assert.AreEqual( deletedid, id);
		}

		
	} 
	
		
	
}