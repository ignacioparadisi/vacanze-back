using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo8;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo8;

namespace vacanze_back.VacanzeApiTest.Grupo8
{
    [TestFixture]
    public class CruiserControllerTests
    {
        private CruiserController CruiserController;
        private Cruiser Cruiser; 
        [SetUp]
        public void Setup()
        {
            Cruiser = new Cruiser("concordia", true, 100, 1000, "Model1", "Line1", "Picture.jpg");
            CruiserController = new CruiserController();
        }
        [Test]
        public void GetCruisersTest()
        {
            ActionResult<IEnumerable<Cruiser>> cruisers = CruiserController.GetCruisers();
            Assert.NotNull(cruisers);
        }
        [Test]
        public void GetCruiserTest()
        {
            var addedCruiser = CruiserRepository.AddCruiser(Cruiser);
            var getCruiser = CruiserController.GetCruiser(addedCruiser);
            Assert.NotNull(getCruiser.Value);
        }
        [Test]
        public void GetCruiserFailedTest()
        {
            var getCruiser = CruiserController.GetCruiser(-1);
            Assert.IsNull(getCruiser.Value);
        }
        [Test]
        public void GetCruiserNullTest()
        {
            var getCruiser = CruiserController.GetCruiser(-1);
            Assert.IsNull(getCruiser.Value);
        }
    }
}