using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo3;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo3;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo3;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo3;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo3;
using vacanze_back.VacanzeApi.LogicLayer.DTO;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo3;

using vacanze_back.VacanzeApi.Common.Exceptions.Grupo3;
namespace vacanze_back.VacanzeApiTest.Grupo3
{

         
  

    [TestFixture]
    public class FlightsControllerTest
    {
       
        private FlightsController controller;

        [SetUp]
        public void SetUp(){
           
            controller = new FlightsController();   

        }

        [Test, Order(1) ]
        public void GetTest()
        {
           GetFlightListCommand command=CommandFactory.getListFlightCommand();
            command.Execute();
            Assert.NotNull(command.GetResult()); 
        }


        [Test, Order(2) ]
        public void FindTest()
        {
            GetFindFlightIdCommand command= CommandFactory.GetFindFlightIdEntityCommand(1);
            command.Execute();
            Assert.NotNull(command.GetResult()); 
        }


        [Test, Order(3) ]
        public void GetByDateTest()
        {
           
            var result = controller.GetByDate("10-10-2020","10-15-2020");
            Assert.IsInstanceOf<ActionResult<IEnumerable<Entity>>>(result); 
        }


        [Test, Order(4) ]
        public void GetByLocationTest()
        {
           
            var result = controller.GetByLocation(1,2);
            Assert.IsInstanceOf<ActionResult<IEnumerable<Entity>>>(result); 
        }


        [Test, Order(5) ]
        public void GetOutboundFlightsTest()
        {
           
            var result = controller.GetOutboundFlights(1,2,"10-15-2020");
            Assert.IsInstanceOf<ActionResult<IEnumerable<Entity>>>(result); 
        }


        [Test, Order(6) ]
        public void GetRoundTripFlightsTest()
        {
           
            var result = controller.GetRoundTripFlights(1,2,"10-10-2020","10-15-2020");
            Assert.IsInstanceOf<ActionResult<IEnumerable<Entity>>>(result); 
        }


        [Test, Order(7) ]
        public void PostTest()
        {

            Flight testflight = new Flight();

            Location loc_departure = new Location();
            Location loc_arrival = new Location();
            Airplane airplane = new Airplane();

            loc_departure.Id = 1;
            loc_arrival.Id = 2;
            airplane.Id = 1;

            testflight.plane = airplane;
            testflight.price = 100;
            testflight.departure = "12-11-2020 01:00:00";
            testflight.arrival = "12-11-2020 02:00:00";
            testflight.loc_departure = loc_departure;
            testflight.loc_arrival = loc_arrival;
           
           var result = controller.Post(testflight);
    
            Assert.IsInstanceOf<OkObjectResult>(result.Result);  

        }


        [Test, Order(8) ]
        public void NullValuesPostTest()
        {

            Flight testflight = new Flight();

            testflight.plane = null;
            testflight.price = 1;
            testflight.departure = null;
            testflight.arrival = null;
            testflight.loc_departure = null;
            testflight.loc_arrival = null;
           

            var result = controller.Post(testflight);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result); 
        }


        [Test, Order(9) ]
        public void NonExistentPlanePostTest()
        {

            Flight testflight = new Flight();

            Location loc_departure = new Location();
            Location loc_arrival = new Location();
            Airplane airplane = new Airplane();

            loc_departure.Id = 1;
            loc_arrival.Id = 2;
            airplane.Id = 99999;

            testflight.plane = airplane;
            testflight.price = 100;
            testflight.departure = "12-11-2020 01:00:00";
            testflight.arrival = "12-11-2020 02:00:00";
            testflight.loc_departure = loc_departure;
            testflight.loc_arrival = loc_arrival;
           

            var result = controller.Post(testflight);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result); 
        }


        [Test, Order(10) ]
        public void SameDepartureAndArrivalPlacesPostTest()
        {

            Flight testflight = new Flight();

            Location loc_departure = new Location();
            Location loc_arrival = new Location();
            Airplane airplane = new Airplane();

            loc_departure.Id = 1;
            loc_arrival.Id = 1;
            airplane.Id = 1;

            testflight.plane = airplane;
            testflight.price = 100;
            testflight.departure = "12-11-2020 01:00:00";
            testflight.arrival = "12-11-2020 02:00:00";
            testflight.loc_departure = loc_departure;
            testflight.loc_arrival = loc_arrival;
           

            var result = controller.Post(testflight);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result); 
        }


        [Test, Order(11) ]
        public void InsufficientPlaneAutonomyPostTest()
        {

            Flight testflight = new Flight();

            Location loc_departure = new Location();
            Location loc_arrival = new Location();
            Airplane airplane = new Airplane();

            loc_departure.Id = 1;
            loc_arrival.Id = 2;
            airplane.Id = 1;

            testflight.plane = airplane;
            testflight.price = 100;
            testflight.departure = "12-11-2020 01:00:00";
            testflight.arrival = "12-20-2020 02:00:00";
            testflight.loc_departure = loc_departure;
            testflight.loc_arrival = loc_arrival;
           

            var result = controller.Post(testflight);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result); 
        }


        [Test, Order(12) ]
        public void DepartureBiggerThanArrivalPostTest()
        {

            Flight testflight = new Flight();

            Location loc_departure = new Location();
            Location loc_arrival = new Location();
            Airplane airplane = new Airplane();

            loc_departure.Id = 1;
            loc_arrival.Id = 2;
            airplane.Id = 1;

            testflight.plane = airplane;
            testflight.price = 100;
            testflight.departure = "12-22-2020 01:00:00";
            testflight.arrival = "12-20-2020 02:00:00";
            testflight.loc_departure = loc_departure;
            testflight.loc_arrival = loc_arrival;
           

            var result = controller.Post(testflight);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result); 
        }


        [Test, Order(13) ]
        public void DeleteTest()
        {
           
            Flight testflight = new Flight();

            Location loc_departure = new Location();
            Location loc_arrival = new Location();
            Airplane airplane = new Airplane();

            loc_departure.Id = 1;
            loc_arrival.Id = 2;
            airplane.Id = 1;

            testflight.plane = airplane;
            testflight.price = 100;
            testflight.departure = "12-11-2020";
            testflight.arrival = "12-21-2020";
            testflight.loc_departure = loc_departure;
            testflight.loc_arrival = loc_arrival;

            int id = FlightRepository.Add(testflight);

            var result = controller.Delete(id);
            Assert.IsInstanceOf<OkObjectResult>(result.Result); 
        }

        [Test, Order(14) ]
        public void NonExistentDeleteTest()
        {

            var result = controller.Delete(0);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result); 
        }

///////////////////////////////////////////////////////////////////////////////////////////////
        [Test, Order(15) ]
        public void PutTest()
        {

            Flight testflight = new Flight();

            Location loc_departure = new Location();
            Location loc_arrival = new Location();
            Airplane airplane = new Airplane();

            loc_departure.Id = 1;
            loc_arrival.Id = 2;
            airplane.Id = 1;

            testflight.plane = airplane;
            testflight.price = 100;
            testflight.departure = "12-11-2020 01:00:00";
            testflight.arrival = "12-11-2020 02:00:00";
            testflight.loc_departure = loc_departure;
            testflight.loc_arrival = loc_arrival;
           
            int id = FlightRepository.Add(testflight);
            testflight.Id = id;

            var result = controller.Put(testflight);
    
            Assert.IsInstanceOf<OkObjectResult>(result.Result);  

        }


        [Test, Order(16) ]
        public void NonExistentPutTest()
        {

            Flight testflight = new Flight();

            testflight.Id = 0;
            

            var result = controller.Put(testflight);
    
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);  

        }

        [Test, Order(17) ]
        public void NullValuesPutTest()
        {

            Flight testflight = new Flight();

            Location loc_departure = new Location();
            Location loc_arrival = new Location();
            Airplane airplane = new Airplane();

            loc_departure.Id = 1;
            loc_arrival.Id = 2;
            airplane.Id = 1;

            testflight.plane = airplane;
            testflight.price = 100;
            testflight.departure = "12-11-2020 01:00:00";
            testflight.arrival = "12-11-2020 02:00:00";
            testflight.loc_departure = loc_departure;
            testflight.loc_arrival = loc_arrival;

            int id = FlightRepository.Add(testflight);

            testflight.plane = null;
            testflight.price = 1;
            testflight.departure = null;
            testflight.arrival = null;
            testflight.loc_departure = null;
            testflight.loc_arrival = null;   
            testflight.Id = id;

            var result = controller.Put(testflight);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result); 
        }

 
        [Test, Order(18) ]
        public void NonExistentPlanePutTest()
        {

            Flight testflight = new Flight();

            Location loc_departure = new Location();
            Location loc_arrival = new Location();
            Airplane airplane = new Airplane();

            loc_departure.Id = 1;
            loc_arrival.Id = 2;
            airplane.Id = 1;

            testflight.plane = airplane;
            testflight.price = 100;
            testflight.departure = "12-11-2020 01:00:00";
            testflight.arrival = "12-11-2020 02:00:00";
            testflight.loc_departure = loc_departure;
            testflight.loc_arrival = loc_arrival;

            int id = FlightRepository.Add(testflight);

            airplane.Id = 99999;  
            testflight.plane = airplane;
            testflight.Id = id;

            var result = controller.Put(testflight);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);  
        }


        [Test, Order(19) ]
        public void SameDepartureAndArrivalPlacesPutTest()
        {

            Flight testflight = new Flight();

            Location loc_departure = new Location();
            Location loc_arrival = new Location();
            Airplane airplane = new Airplane();

            loc_departure.Id = 1;
            loc_arrival.Id = 2;
            airplane.Id = 1;

            testflight.plane = airplane;
            testflight.price = 100;
            testflight.departure = "12-11-2020 01:00:00";
            testflight.arrival = "12-11-2020 02:00:00";
            testflight.loc_departure = loc_departure;
            testflight.loc_arrival = loc_arrival;

            int id = FlightRepository.Add(testflight);

            loc_departure.Id = 1;
            loc_arrival.Id = 1;
            testflight.loc_departure = loc_departure;
            testflight.loc_arrival = loc_arrival;
            testflight.Id = id;

            var result = controller.Put(testflight);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result); 
        }


        [Test, Order(21) ]
        public void InsufficientPlaneAutonomyPutTest()
        {

            Flight testflight = new Flight();

            Location loc_departure = new Location();
            Location loc_arrival = new Location();
            Airplane airplane = new Airplane();

            loc_departure.Id = 1;
            loc_arrival.Id = 2;
            airplane.Id = 1;

            testflight.plane = airplane;
            testflight.price = 100;
            testflight.departure = "12-11-2020 01:00:00";
            testflight.arrival = "12-11-2020 02:00:00";
            testflight.loc_departure = loc_departure;
            testflight.loc_arrival = loc_arrival;

            int id = FlightRepository.Add(testflight);

            testflight.departure = "12-11-2020 01:00:00";
            testflight.arrival = "12-30-2020 02:00:00";
            testflight.Id = id;

            var result = controller.Put(testflight);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result); 
        }


        [Test, Order(22) ]
        public void DepartureBiggerThanArrivalPutTest()
        {

           Flight testflight = new Flight();

            Location loc_departure = new Location();
            Location loc_arrival = new Location();
            Airplane airplane = new Airplane();

            loc_departure.Id = 1;
            loc_arrival.Id = 2;
            airplane.Id = 1;

            testflight.plane = airplane;
            testflight.price = 100;
            testflight.departure = "12-11-2020 01:00:00";
            testflight.arrival = "12-11-2020 02:00:00";
            testflight.loc_departure = loc_departure;
            testflight.loc_arrival = loc_arrival;

            int id = FlightRepository.Add(testflight);

            testflight.departure = "12-13-2020 01:00:00";
            testflight.arrival = "12-12-2020 02:00:00";
            testflight.Id = id;

            var result = controller.Put(testflight);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result); 

        }


        
    }
}