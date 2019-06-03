using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Newtonsoft.Json;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.Repository;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo5;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo5;
using vacanze_back.VacanzeApiTest.Grupo6;

namespace vacanze_back.VacanzeApiTest.Grupo5
{
    [TestFixture]
    public class AutoTest
    {
        private Auto auto;
        private string make;
        private string model;
        private int cap;
        private bool status;
        private string license;
        private int price;
        private int location;

        [SetUp]
        public void Setup()
        {
            /*var*/ location = 1;//LocationRepository.GetLocationById(HotelTestSetup.LOCATION_ID);
            make = "Toyota";
            model = "Corolla";
            cap = 3;
            status = true;
            license = "ab12c3f";
            price = 50;
            auto = new Auto(make,model,cap,status,license,price,location) ; 
        }
        [Test]
        public void AddTest()
        {
            var result= ConnectAuto.Agregar(auto);
            Assert.AreEqual(1, result);
        }
    }
}