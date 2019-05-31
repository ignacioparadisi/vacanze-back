using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo8;

namespace vacanze_back.VacanzeApiTest.Grupo8
{
    [TestFixture]
    public class CruiserControllerTests
    {
        private CruiserController cruiserController;
        [SetUp]
        public void Setup()
        {
            cruiserController = new CruiserController();
        }
        [Test]
        public void GetCruisersTest()
        {
            ActionResult<IEnumerable<Cruiser>> cruisers = cruiserController.GetCruisers();
            Assert.NotNull(cruisers);
        }
        [Test]
        public void GetCruiserTest()
        {
            var cruiser = cruiserController.GetCruiser(25);
            Assert.NotNull(cruiser.Value);
        }
        [Test]
        public void GetCruiserFailedTest()
        {
            var cruiser = cruiserController.GetCruiser(-1);
            Assert.IsNull(cruiser.Value);
        }
        [Test]
        public void GetCruiserNullTest()
        {
            var cruiser = cruiserController.GetCruiser(-1);
            Assert.IsNull(cruiser.Value);
        }
    }
}