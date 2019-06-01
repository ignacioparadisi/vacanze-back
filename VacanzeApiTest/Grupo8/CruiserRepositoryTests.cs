using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo8;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo8;

namespace vacanze_back.VacanzeApiTest.Grupo8
{
    [TestFixture]
    public class CruiserRepositoryTests
    {
        private Cruiser cruiser;

        [SetUp]
        public void Setup()
        {
            cruiser = new Cruiser("concordia", true, 100, 1000, "Model1", "Line1", "Picture.jpg");
        }

        [Test]
        public void GetCruisersTest()
        {
            var addedcruiser = CruiserRepository.AddCruiser(cruiser);
            List<Cruiser> cruisersList = CruiserRepository.GetCruisers();
            Assert.True(cruisersList.Count > 0);
        }
        
        [Test]
        public void AddCruiserTest()
        {
            var addedcruiser = CruiserRepository.AddCruiser(cruiser);
            Assert.AreNotEqual(0,  addedcruiser);
        }

        [Test]
        public void GetCruiserTest()
        {
            var cruiserId = CruiserRepository.AddCruiser(cruiser);
            var getCruiser = CruiserRepository.GetCruiser(cruiserId);
            Assert.AreEqual(cruiserId, getCruiser.Id);
        }

        [Test]
        public void GetCruiserNotFoundTest()
        {
            Assert.Throws<CruiserNotFoundException>(() => CruiserRepository.GetCruiser(-1));
        }

        [Test]
        public void DeleteCruiserTest()
        {
            var cruiserId = CruiserRepository.AddCruiser(cruiser);
            var deletedId = CruiserRepository.DeleteCruiser(cruiserId);
            Assert.AreEqual(cruiserId,deletedId);
        }

        [Test]
        public void DeletedCruiserFailedTest()
        {
            Assert.Throws<CruiserNotFoundException>(() => CruiserRepository.DeleteCruiser(-1));
        }

        [Test]
        public void ModifyCruiserTest()
        {
            var cruiserid = CruiserRepository.AddCruiser(cruiser);
            Cruiser toUpdateCruiser = new Cruiser(cruiserid,"Updatedcruiser",false,1,1,"updatedmodel","updatedline","updatedpicture");
            var updatedCruiser = CruiserRepository.UpdateCruiser(toUpdateCruiser);
            Assert.AreEqual(toUpdateCruiser,updatedCruiser);
        }
        [Test]
        public void ModifyCruiserNotFoundTest()
        {
            Cruiser toUpdateCruiser = new Cruiser(-1,"Updatedcruiser",false,1,1,"updatedmodel","updatedline","updatedpicture");
            Assert.Throws<CruiserNotFoundException>(() => CruiserRepository.UpdateCruiser(toUpdateCruiser));
        }

        [Test]
        public void ModifyCruiserNullParameterTest()
        {
            Assert.Throws<NullCruiserException>(() => CruiserRepository.UpdateCruiser(null));
        }
    }
}