using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo7;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo7;
using System;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo7;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo8;

namespace vacanze_back.VacanzeApiTest.Grupo7
{
	[TestFixture]
	public class PostgresRestaurantDaoTest
	{
		private PostgresRestaurantDAO _restaurantDao;
		private Restaurant _restaurant;
		private List<int> _addedRestaurantList;
		[SetUp]
		public void Setup()
		{
			_restaurant = new Restaurant( "Yosemite", 
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
			_addedRestaurantList = new List<int>();
			_restaurantDao = (PostgresRestaurantDAO) DAOFactory.GetFactory(DAOFactory.Type.Postgres).GetRestaurantDAO();
		}

		[Test]
		public void GetRestaurantTest()
		{
			var id = _restaurantDao.AddRestaurant(_restaurant);
			_addedRestaurantList.Add(id);
			var restaurant = _restaurantDao.GetRestaurant(id);
			Assert.AreEqual(id,restaurant.Id);
		}
		
		[Test]
		public void GetRestaurantTest_Returns_RestaurantNotFoundExeption()
		{
			Assert.Throws<RestaurantNotFoundExeption>(() => _restaurantDao.GetRestaurant(-1));
		}

		[Test]

		public void GetRestaurantsTest()
		{
			var id = _restaurantDao.AddRestaurant(_restaurant);
			_addedRestaurantList.Add(id);
			List<Restaurant> restaurantList = _restaurantDao.GetRestaurants();
			Assert.IsTrue(restaurantList.Count > 0);
		}
		
		
		[Test]
		public void GetRestaurantsByCityTest()
		{
			var id = _restaurantDao.AddRestaurant(_restaurant);
			_addedRestaurantList.Add(id);
			List<Restaurant> restaurantList = _restaurantDao.GetRestaurantsByCity(_restaurant.Location);
			Assert.IsTrue(restaurantList.Count > 0);
		}
		
		[Test]
		public void AddRestaurantTest()
		{
			var id = _restaurantDao.AddRestaurant(_restaurant);
			_addedRestaurantList.Add(id);
			var restaurant = _restaurantDao.GetRestaurant(id);
			Assert.AreEqual(id,restaurant.Id);
		}
		
		[Test]
		public void UpdateRestaurantTest()
		{
			var id = _restaurantDao.AddRestaurant(_restaurant);
			_addedRestaurantList.Add(id);
			var toUpdateRestaurant = EntityFactory.CreateRestaurant(id, "Updated", 200, false, 4, "Tsingy", 21, "frf",
				"rterte", "trhrthtr", "021221546352", 1, "yrty rtyrtks yrtyr");
			var updatedRestaurant = _restaurantDao.UpdateRestaurant(toUpdateRestaurant);
			var actualRestaurant = _restaurantDao.GetRestaurant(id);
			Assert.AreEqual(updatedRestaurant.Name,actualRestaurant.Name);
		}

		[Test]

		public void UpdateRestaurantTest_Returns_RestaurantNotFoundExeption()
		{
			_restaurant.Id = -1;
			Assert.Throws<RestaurantNotFoundExeption>(() => _restaurantDao.UpdateRestaurant(_restaurant));
		}
		
		[Test]

		public void DeleteRestaurantTest()
		{
			var id = _restaurantDao.AddRestaurant(_restaurant);
			_restaurantDao.DeleteRestaurant(id);
			Assert.Throws<RestaurantNotFoundExeption>(() => _restaurantDao.GetRestaurant(id));
		}
		
		[Test]

		public void DeleteRestaurantTest_RestaurantNotFoundExeption()
		{
			Assert.Throws<RestaurantNotFoundExeption>(() => _restaurantDao.GetRestaurant(-1));
		}
		
		[TearDown]

		public void TearDown()
		{
			foreach (var restaurantId in _addedRestaurantList)
			{
				_restaurantDao.DeleteRestaurant(restaurantId);
			}
		}
	}
}