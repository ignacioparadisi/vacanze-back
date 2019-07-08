using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo12;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo12;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo12;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo12;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo12;
namespace vacanze_back.VacanzeApiTest.Grupo12
{

    [TestFixture]
    public class FlyReservationControllerTests
    {
       
        private FlightReservationController controller;

        [SetUp]
        public void SetUp(){
           
            controller = new FlightReservationController();   

        }

        //Positivas
        [Test, Order(1) ]
        public void GetResByUserIdTest()
        {
           
            var result = controller.Get(1);
            Assert.IsInstanceOf<ActionResult<IEnumerable<Entity>>>(result); 
        }


        [Test, Order(2) ]
        public void GetLocationsIdsByNamesTest()
        {
           
            var result = controller.Get("Caracas","Madrid");
            Assert.IsInstanceOf<ActionResult<IEnumerable<int>>>(result); 
        }


        [Test, Order(3) ]
        public void DeleteResTest()
        {
           
            var result = controller.Delete(1);
            Assert.IsInstanceOf<ActionResult<IEnumerable<String>>>(result); 
        }


        [Test, Order(4) ]
        public void GetIResTest()
        {
           
            var result = controller.Get(1, 2, "2019-01-01",1);
            Assert.IsInstanceOf<ActionResult<IEnumerable<Entity>>>(result); 
        }


        [Test, Order(5) ]
        public void GetIVResTest()
        {
           
            var result = controller.Get(1, 2, "2019-01-01","2019-01-01",1);
            Assert.IsInstanceOf<ActionResult<IEnumerable<Entity>>>(result); 
        }


        [Test, Order(6) ]
        public void PostResTest()
        {


            FlightResDTO flightDTO = new FlightResDTO("","2019-7-6 23:00",10,1,1);

           
           var result = controller.Post(flightDTO);
    
            Assert.IsInstanceOf<ActionResult<IEnumerable<string>>>(result);  

        }


        //Negativas
       [Test, Order(7) ]
        public void GetResByUserIdExTest()
        {
            var result = controller.Get(-1);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result); 
        }

        [Test, Order(8) ]
        public void GetLocationsIdsByNamesExTest()
        {
            var result = controller.Get("petare","petare");
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result); 
        }

        [Test, Order(9) ]
        public void DeleteResExTest()
        {
            var result = controller.Delete(-1);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result); 
        }

        [Test, Order(10) ]
        public void GetIResFirstLocationExTest()
        {    
            var result = controller.Get(-1, 2, "2019-01-01",1);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result); 
        }

        [Test, Order(11) ]
        public void GetIResSecondLocationExTest()
        {    
            var result = controller.Get(1, -2, "2019-01-01",1);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result); 
        }


       [Test, Order(12) ]
        public void GetIVResFirstLocationExTest()
        {    
            var result = controller.Get(-1, 2, "2019-01-01","2019-01-01",1);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result); 
        }

        [Test, Order(13) ]
        public void GetIVResSecondLocationExTest()
        {    
            var result = controller.Get(1, -2, "2019-01-01","2019-01-01",1);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result); 
        }

        [Test, Order(14) ]
        public void PostResPassengersExTest()
        {
            FlightResDTO flightDTO = new FlightResDTO("","2019-7-6 23:00",0,1,1);
            var result = controller.Post(flightDTO);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);  
        }

        [Test, Order(15) ]
        public void PostResUserExTest()
        {
            FlightResDTO flightDTO = new FlightResDTO("","2019-7-6 23:00",1,-1,1);
            var result = controller.Post(flightDTO);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);  
        }

        [Test, Order(16) ]
        public void PostResFlyExTest()
        {
            FlightResDTO flightDTO = new FlightResDTO("","2019-7-6 23:00",0,1,-1);
            var result = controller.Post(flightDTO);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);  
        }
        
    }
}