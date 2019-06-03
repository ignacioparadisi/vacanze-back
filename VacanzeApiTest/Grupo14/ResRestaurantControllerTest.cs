using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo14;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo14;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo14;

using vacanze_back.VacanzeApi.Persistence.Repository.Grupo7;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo7;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;

namespace vacanze_back.VacanzeApiTest.Grupo14
{
    [TestFixture]
    public class ResRestaurantsControllerTest
    {
        private int addId;
        private int modifyId;
        private int deleteId;
        Restaurant_res reservation;
        reservationRestaurant rr;
        private ResRestaurantController controller;
     
        private RestaurantsController restaurantsController;
        Restaurant restaurant;
        int restaurantId;

        [SetUp]
        public void setUp(){
            controller = new ResRestaurantController();
            rr = new reservationRestaurant();
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
			
        }

        [Test]
        public void postResRestaurantTest(){

            restaurantId = RestaurantRepository.AddRestaurant(restaurant);
			
			rr.fecha_res = "2019-07-28 00:00";
			rr.cant_people= 1;
			rr.date= "2019-05-30 00:17";
			rr.user_id= 5;
			rr.rest_id= restaurantId;			

            ActionResult<int> addId = controller.Post(rr);
            Assert.NotNull(addId);
        }

        [Test]
        public void getResRestaurantTest(){

            ActionResult<IEnumerable<Restaurant_res>> res = controller.Get(5);
            Assert.NotNull(res);
        }

        [Test]
        public void getReservationNotPayTest(){

            ActionResult<IEnumerable<Restaurant_res>> res = controller.GetReservationNotPay(5);
            Assert.NotNull(res);
        }

        [Test]
        public void putResRestaurantTest(){
            rr.pay_id = 1;
            restaurantId = RestaurantRepository.AddRestaurant(restaurant);
            reservation = new Restaurant_res(
				"2019-07-28 00:00",
				1,
				"2019-05-30 00:17",
				5,
				restaurantId);
			
			addId = ResRestaurantRepository.addReservation(reservation);
            ActionResult<string> res = controller.Put(addId,rr);
            Assert.NotNull(res);
        }

        [Test]
        public void deleteResRestaurantTest(){
            restaurantId = RestaurantRepository.AddRestaurant(restaurant);
            reservation = new Restaurant_res(
				"2019-07-28 00:00",
				1,
				"2019-05-30 00:17",
				5,
				restaurantId);
			
			addId = ResRestaurantRepository.addReservation(reservation);
            ActionResult<string> res = controller.Delete(addId);
            Assert.NotNull(res);
        }
    }
}