using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo3;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo3;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo3;
namespace vacanze_back.VacanzeApiTest.Grupo3
{
    [TestFixture]
    public class FlightsTest
    {
        private Flight testflight;
        int id;

        [SetUp]
        public void SetUp(){
            testflight = new Flight();
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
        }

        [Test, Order(1) ]
        public void AddFlightTest()
        {
            id = FlightRepository.Add(testflight);
            Assert.True(id > 0);
        }

        [Test, Order(2)]
        public void GetFlightsTest()
        {
            List<Entity> flights = FlightRepository.Get();
            Assert.True(flights.Count() > 0);
        }

        [Test, Order(3)]
        public void FindFlightTest()
        {
            Entity flight = FlightRepository.Find(id);
            Assert.That(flight, Is.Not.Null);
        }

        [Test, Order(4)]
        public void UpdateFlightTest()
        {
            testflight.loc_arrival.Id = 6;
            testflight.Id = id;
            FlightRepository.Update(testflight);
            Flight flight = (Flight) FlightRepository.Find(id);
            Assert.True(flight.loc_arrival.Id == 6);
        }

        [Test, Order(5)]
        public void GetByDateFlightTest()
        {
            List<Entity> flights = FlightRepository.GetByDate("12-11-2020","12-30-2020");
            Assert.True(flights.Count() > 0);
        }
    }
}