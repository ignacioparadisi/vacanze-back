
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo8;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo8;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;

namespace vacanze_back.VacanzeApiTest.Grupo7
{
    [TestFixture]
    public class CruisersControllerTest
    {
        private CruiserController _cruiserController;
        CruiserDTO _cruiserDTO;
        LayoverDTO _layoverDTO;
        int _idABorrar;
        private List<int> _addedcruiserList;

        [SetUp]
        public void Setup()
        {
            _cruiserController = new CruiserController();
            _cruiserDTO = new CruiserDTO();
            _cruiserDTO.Name = "Corneria";
            _cruiserDTO.Status = true;
            _cruiserDTO.Capacity = 150;
            _cruiserDTO.LoadingShipCap = 20;
            _cruiserDTO.Model = "Cleveland";
            _cruiserDTO.Line = "Lynis";
            _cruiserDTO.Picture = "A nice one";
            _addedcruiserList = new List<int>();
        }

        [Test]

        public void PostCruiserTest()
        {
            var result = _cruiserController.PostCruiser(_cruiserDTO).Value;
            _addedcruiserList.Add(result.Id);
            Assert.AreEqual(_cruiserDTO.Name,result.Name);
        }

        [Test]

        public void PostCruiserTest_InvalidAttributeExeption_ReturnsBadRequest()
        {
            _cruiserDTO.Capacity = 0;
            Assert.IsInstanceOf<BadRequestObjectResult>(_cruiserController.PostCruiser(_cruiserDTO).Result);
        }
        
        [Test]

        public void GetCruisersTest()
        {
            Assert.IsInstanceOf<List<CruiserDTO>>(_cruiserController.Get().Value);
        }

        [Test]
        public void GetCruiserTest()
        {
            var addedCruiser = _cruiserController.PostCruiser(_cruiserDTO).Value;
            _addedcruiserList.Add(addedCruiser.Id);
            var result = _cruiserController.GetCruiser(addedCruiser.Id).Value;
            Assert.AreEqual(addedCruiser.Id,result.Id);
        }
        
        [Test]

        public void GetCruiserTest_CruiserNotFoundException_ReturnBadRequest()
        {
            Assert.IsInstanceOf<BadRequestObjectResult>(_cruiserController.GetCruiser(-1).Result);
        }

        [Test]

        public void PutCruiserTest()
        {
            var addedCruiser = _cruiserController.PostCruiser(_cruiserDTO).Value;
            _addedcruiserList.Add(addedCruiser.Id);
            _cruiserDTO.Id = addedCruiser.Id;
            _cruiserDTO.Name = "Updated";
            var result = _cruiserController.PutCruiser(_cruiserDTO).Value;
            Assert.AreEqual(_cruiserDTO.Name,result.Name);
        }
        
        [Test]

        public void PutRestaurantTest_InvalidAttributeException_ReturnsBadRequest()
        {
            _cruiserDTO.Capacity = -1;
            Assert.IsInstanceOf<BadRequestObjectResult>(_cruiserController.PutCruiser(_cruiserDTO).Result);
        }

        [Test]

        public void PutRestaurantTest_CruiserNotFoundException_ReturnBadRequest()
        {
            _cruiserDTO.Id = -1;
            Assert.IsInstanceOf<BadRequestObjectResult>(_cruiserController.PutCruiser(_cruiserDTO).Result);
        }
        
        [Test]
        public void DeleteRestaurantTestt()
        {
            var id = _cruiserController.PostCruiser(_cruiserDTO).Value.Id;
            Assert.IsInstanceOf<OkObjectResult>(_cruiserController.DeleteCruiser(id));
        }

        [Test, Order(1)]
        public void PostLayoverTest()
        {
            var result = _cruiserController.PostCruiser(_cruiserDTO).Value;
            _addedcruiserList.Add(result.Id);
            _layoverDTO = new LayoverDTO();
            _layoverDTO.CruiserId = result.Id;
            _layoverDTO.DepartureDate = "2019-12-2";
            _layoverDTO.ArrivalDate = "2019-12-6";
            _layoverDTO.Price = 250;
            _layoverDTO.LocDeparture = 1;
            _layoverDTO.LocArrival = 2;
            var layover = _cruiserController.PostLayover(_layoverDTO).Value;
            _layoverDTO.Id = layover.Id;
            _idABorrar = layover.Id;
            Assert.AreEqual(_layoverDTO.CruiserId,layover.CruiserId);
        }

        [Test, Order(2)]
        public void PostLayoverTest_Returns_InvalidAttributeException()
        {
            _layoverDTO.LocDeparture = -1;
            Assert.IsInstanceOf<BadRequestObjectResult>(_cruiserController.PostLayover(_layoverDTO).Result);
        }

        [Test, Order(3)]
        public void GetLayoversTest()
        {
            _layoverDTO.LocDeparture = 1;
            Assert.IsInstanceOf<List<LayoverDTO>>(_cruiserController.GetLayovers(_layoverDTO.CruiserId).Value);
        }

        [Test, Order(4)]
        public void DeleteLayoversTest()
        {
             var result = _cruiserController.PostCruiser(_cruiserDTO).Value;
            _addedcruiserList.Add(result.Id);
            _layoverDTO = new LayoverDTO();
            _layoverDTO.CruiserId = result.Id;
            _layoverDTO.DepartureDate = "2019-12-2";
            _layoverDTO.ArrivalDate = "2019-12-6";
            _layoverDTO.Price = 250;
            _layoverDTO.LocDeparture = 1;
            _layoverDTO.LocArrival = 2;
            var layover = _cruiserController.PostLayover(_layoverDTO).Value;
            Assert.IsInstanceOf<OkObjectResult>(_cruiserController.DeleteLayover(layover.Id));
        }



        [TearDown]
        public void TearDown()
        {
            foreach (var cruiserId in _addedcruiserList)
            {
                _cruiserController.DeleteCruiser(cruiserId);
            }
        }
    }
}