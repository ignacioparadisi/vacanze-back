using System;
using System.Collections.Generic;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo8;

namespace vacanze_back.VacanzeApiTest.Grupo8
{
    [TestFixture]
    public class CruiserRepositoryTests
    {
        private Cruiser _cruiser;
        private List<int> _addedCruiserList;
        [SetUp]
        public void Setup()
        {
            _cruiser = new Cruiser("concordia", true, 100, 1000, "Model1", "Line1", "Picture.jpg");
            _addedCruiserList = new List<int>();
        }
        [TearDown]
        public void TearDown()
        {
            foreach (var cruiserId in _addedCruiserList) CruiserRepository.DeleteCruiser(cruiserId);
            _addedCruiserList.Clear();
        }
        [Test]
        public void GetCruisersTest()
        {
            var addedCruiserId = CruiserRepository.AddCruiser(_cruiser);
            _addedCruiserList.Add(addedCruiserId);
            List<Cruiser> cruisersList = CruiserRepository.GetCruisers();
            Assert.True(cruisersList.Count > 0);
        }
        
        [Test]
        public void AddCruiserTest()
        {
            var addedCruiserId = CruiserRepository.AddCruiser(_cruiser);
            _addedCruiserList.Add(addedCruiserId);
            Assert.AreNotEqual(0,  addedCruiserId);
        }

        [Test]
        public void GetCruiserTest()
        {
            var addedCruiserId = CruiserRepository.AddCruiser(_cruiser);
            _addedCruiserList.Add(addedCruiserId);
            var getCruiser = CruiserRepository.GetCruiser(addedCruiserId);
            Assert.AreEqual(addedCruiserId, getCruiser.Id);
        }

        [Test]
        public void GetCruiserNotFoundTest()
        {
            Assert.Throws<CruiserNotFoundException>(() => CruiserRepository.GetCruiser(-1));
        }

        [Test]
        public void DeleteCruiserTest()
        {
            var cruiserId = CruiserRepository.AddCruiser(_cruiser);
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
            var addedCruiserId = CruiserRepository.AddCruiser(_cruiser);
            _addedCruiserList.Add(addedCruiserId);
            Cruiser toUpdateCruiser = new Cruiser(addedCruiserId,"Updatedcruiser",false,1,1,"updatedmodel","updatedline","updatedpicture");
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

        [Test]
        public void AddLayoverTest()
        {
            var addedCruiserId = CruiserRepository.AddCruiser(_cruiser);
            _addedCruiserList.Add(addedCruiserId);
            var layover = new Layover(addedCruiserId,Convert.ToString("2019-01-01"), Convert.ToString("2019-01-02"),2000,1,2);
            var addedLayover = CruiserRepository.AddLayover(layover);
            addedLayover.Id = 0;
            Assert.AreEqual(layover,addedLayover);
        }
        [Test]
        public void AddlayoverCruiserNotFoundTest()
        {
            var layover = new Layover(-1,Convert.ToString("2019-01-01"), Convert.ToString("2019-01-02"),2000,1,2);
            Assert.Throws<CruiserNotFoundException>(() => CruiserRepository.AddLayover(layover));
        }
        [Test]
        public void DeleteLayoverTest()
        {
            var addedCruiserId = CruiserRepository.AddCruiser(_cruiser);
            _addedCruiserList.Add(addedCruiserId);
            var layover = new Layover(addedCruiserId,Convert.ToString("2019-01-01"), Convert.ToString("2019-01-02"),2000,1,2);
            var addedLayover = CruiserRepository.AddLayover(layover);
            var deletedId = CruiserRepository.DeleteLayover(addedLayover.Id);
            Assert.AreEqual(addedLayover.Id,deletedId);
        }

        [Test]
        public void DeletedLayoverFailedTest()
        {
            Assert.Throws<LayoverNotFoundException>(() => CruiserRepository.DeleteLayover(-1));
        }

        [Test]

        public void GetLayoversTest()
        {
            List<Layover> layoversList= new List<Layover>();
            var addedCruiserId = CruiserRepository.AddCruiser(_cruiser);
            _addedCruiserList.Add(addedCruiserId);
            var layover1 = new Layover(addedCruiserId,Convert.ToString("2019-01-01"), Convert.ToString("2019-01-02"),2000,1,2);
            var layover2 = new Layover(addedCruiserId,Convert.ToString("2019-01-01"), Convert.ToString("2019-01-02"),2000,1,2);
            var addedLayover1 = CruiserRepository.AddLayover(layover1);
            var addedLayover2 = CruiserRepository.AddLayover(layover2);
            layoversList.Add(addedLayover1);
            layoversList.Add(addedLayover2);
            List<Layover> getLayoverList = CruiserRepository.GetLayovers(addedCruiserId);
            Assert.AreEqual(layoversList.Count,getLayoverList.Count);
        }
        [Test]

        public void GetLayoversByLocationTest()
        {
            List<Layover> layoversList= new List<Layover>();
            var addedCruiserId = CruiserRepository.AddCruiser(_cruiser);
            _addedCruiserList.Add(addedCruiserId);
            var layover = new Layover(addedCruiserId,Convert.ToString("2019-01-01"), Convert.ToString("2019-01-02"),2000,50,10);
            var addedLayover = CruiserRepository.AddLayover(layover);
            layoversList.Add(addedLayover);
            List<Layover> getLayoverList = CruiserRepository.GetLayoversForRes(layover.LocDeparture,layover.LocArrival);
            Assert.Greater(getLayoverList.Count,0);
        }
        [Test]

        public void GetLayoversByLocation_LayoverNotfound_Test()
        {
            Assert.Throws<LayoverNotFoundException>(() => CruiserRepository.GetLayoversForRes(-1, -1));
        }
    }
}