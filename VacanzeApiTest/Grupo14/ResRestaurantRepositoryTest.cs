//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.AspNetCore.Mvc;
//using NUnit.Framework;
//using vacanze_back.VacanzeApi.Common.Entities.Grupo14;
//using vacanze_back.VacanzeApi.Services.Controllers.Grupo14;
//using vacanze_back.VacanzeApi.Persistence.Repository.Grupo14;

//using vacanze_back.VacanzeApi.Persistence.Repository.Grupo7;
//using vacanze_back.VacanzeApi.Common.Entities.Grupo7;

//using System;

//namespace vacanze_back.VacanzeApiTest.Grupo14
//{
//	[TestFixture]
//	public class ResRestaurantRepositoryTest
//	{
//		ResRestaurantController controller;
//		Restaurant_res res;
//		Restaurant_res secondRes;
//		List<Restaurant_res> resRestaurantList;
//		int addId;
//		int modifyId;
//		int deleteId;

//		Restaurant prueba;
//		int restaurantId;

//		[SetUp]
//		public void setUp(){

//			prueba = new Restaurant( "Yosemite", 
//                            200, 
//                            true, 
//                            4, 
//                            "Tsingy", 
//                            21, 
//                            "Wadsdworth Longfellow", 
//                            "A great picture of the Dead Sea could be here, you know?", 
//                            "Listen, strange women lying in ponds distributing swords is no basis for a system of government… You can’t expect to wield supreme power just ‘cause some watery tart threw a sword at you!",
//                            "021221546352", 
//                            1, 
//                            "As it turns out, Mount Kilimanjaro is not wi-fi enabled, so I had to spend two weeks in Tanzania talking to the people on my trip.");
			
//		}

//		[Test, Order(1)]
//		public void addReservationTest(){
//			restaurantId = RestaurantRepository.AddRestaurant(prueba);
			
//			res = new Restaurant_res(
//				"2019-07-28 00:00",
//				1,
//				"2019-05-30 00:17",
//				5,
//				restaurantId);
			
//			addId = ResRestaurantRepository.addReservation(res);
//			Assert.True(addId >0);
//		}

//		[Test, Order(2)]
//		public void updateResRestaurantTest(){
//			ResRestaurantRepository conec = new ResRestaurantRepository();
//			modifyId = conec.updateResRestaurant(1, addId);
//			Assert.AreEqual(addId, modifyId);
//		}

//		[Test, Order(3)]
//		public void getReservationNotPayTest(){
//			//El  ID del usuario es 5
//			resRestaurantList = ResRestaurantRepository.getReservationNotPay(5);
//			Assert.True( resRestaurantList.Count() >= 0);
//		}

//		[Test, Order(4)]
//		public void getResRestaurantTest(){
//			//El  ID del usuario es 5
//			resRestaurantList = ResRestaurantRepository.getResRestaurant(5);
//			Assert.True( resRestaurantList.Count() >= 0);
//		}

//		[Test, Order(5)]
//		public void deleteResRestaurantTest(){
//			ResRestaurantRepository conec = new ResRestaurantRepository();
//			deleteId = conec.deleteResRestaurant(addId);
//			Assert.AreEqual(addId, deleteId);
//		}
//	}
//}