using NUnit.Framework;
using vacanze_back.VacanzeApi.Services.Controllers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace vacanze_back.VacanzeApiTest.Grupo6
{
    [TestFixture]
    public class LocationContollerTest
    {
        
        private LocationsController  _locationController;

        [SetUp]
        public void Setup()
        {
            _locationController = new LocationsController(null);
        }
        [Test]
        public void Get_LocationTest()
        {
            var result = _locationController.Get();
            Assert.NotNull(result.Value);
        }
        [Test]
        public void GetCountries_LocationTest()
        {
            var result = _locationController.GetCountries();
            Assert.NotNull(result.Value);
        }
        [Test]
        public void GetCitiesByCountry_LocationTest()
        {
            var result = _locationController.GetCitiesByCountry(77);
            Assert.NotNull(result.Value);
        }
        [Test]
        public void GetCitiesByCountry_LocationNOTFOUNDTest()
        {
            var result = _locationController.GetCitiesByCountry(7447);
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }
    }
}