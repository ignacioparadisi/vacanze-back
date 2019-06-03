using System.Collections.Generic;
using NUnit.Framework;
using System.Data;
using System;
using vacanze_back.VacanzeApi.Persistence.Repository;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo10;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo10;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo10;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo10;

namespace vacanze_back.VacanzeApiTest.Grupo10
{   
    [TestFixture]
    public class TravelControllerTest{
       
       [SetUp]
        public void Setup(){
            _TravelsControllers = new TravelsControllers();
        }

        [Test]
         public void getTravelsTest_Success(){
            int _userID = 3;
            var result = _TraveksController.getTravels();
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
         }

        [Test]
         public void getTravelsTest_WithoutExistenceOfTravelsException(){
            int _userID = 5;
            var result = _TravelsController.getTravels(_userID);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result); 
         }

         [Test]
         public void getTravelsTest_UserNotFound(){
            int _userID = -3;
            var result = _TravelsController.getTravels(_userID);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result); 
         }


         [Test]
         public void getTravelsTest_InternalServerErrorException(){
            string _userID = "_UserID";
            var result = _TravelsController.getTravels(_userID);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result); 
         }

         [Test]
         public void getTravelsTest_Exception(){
            var result = _TravelsController.getTravels();
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result); 
         }

         [Test]
         public void GetReservationsByTravelAndLocationTest_Success(){
            int _travelID = 3;
            int _locattionID = 3;
            string _Type = "";

            var result = _TravelsController.GetReservationsByTravelAndLocation(_travelID, _locattionID, _Type);
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
         }

         [Test]
         public void GetReservationsByTravelAndLocationTest_InvalidReservationTypeException(){
            int _travelID = 1;
            int _locattionID = 37;
            string _Type = "hotel";

            var result = _TravelsController.GetReservationsByTravelAndLocation(_travelID, _locattionID, _Type);
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
         }

         [Test]
         public void GetReservationsByTravelAndLocationTest_WithoutTravelReservationsException(){
            int _travelID = -1;
            int _locattionID = 37;
            string _Type = "hotel";

            var result = _TravelsController.GetReservationsByTravelAndLocation(_travelID, _locattionID, _Type);
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
         }

         [Test]
         public void GetReservationsByTravelAndLocationTest_InternalServerErrorException(){
            int _travelID = -1;
            int _locattionID = -1;
            string _Type = "";

            var result = _TravelsController.GetReservationsByTravelAndLocation(_travelID, _locattionID, _Type);
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
         }

        [Test]
         public void GetReservationsByTravelAndLocationTest_Exception(){

            var result = _TravelsController.GetReservationsByTravelAndLocation();
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
         }
    }
}