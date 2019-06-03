using System;
using System.Collections.Generic;
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

        private CruiserController _cruiserController;
        private List<int> _addedCruiserList;
        private Cruiser _cruiser;
        [SetUp]
        public void Setup()
        {
            _cruiser = new Cruiser("concordia", true, 100, 1000, "Model1", "Line1", "Picture.jpg");
            _cruiserController = new CruiserController();
            _addedCruiserList = new List<int>();
        }
        [TearDown]
        public void TearDown()
        {
            foreach (var cruiserId in _addedCruiserList) CruiserRepository.DeleteCruiser(cruiserId);
            _addedCruiserList.Clear();
        }
        [Test]
        public void PostCruisersTest_ReturnOkResult()
        {
            var addedcruiser = _cruiserController.PostCruiser(_cruiser);
            var Okresult = (OkObjectResult) addedcruiser.Result;
            var cruiserId = (Cruiser) Okresult.Value;
            _addedCruiserList.Add(cruiserId.Id);
            Assert.IsInstanceOf<OkObjectResult>(addedcruiser.Result);
        }
        [Test]
        public void PostCruiser_CruiserWithInvalidAttributes_BadRequestReturned_Test()
        {
            var cruiser = new Cruiser("concordia", true,0, 1000, "Model1", "Line1", "Picture.jpg");
            var addedcruiser = _cruiserController.PostCruiser(cruiser);
            Assert.IsInstanceOf<BadRequestObjectResult>(addedcruiser.Result);
        }
        [Test]
        public void GetCruisersTest_ReturnsOkResult()
        {
            var addedCruiser = CruiserRepository.AddCruiser(_cruiser);
            _addedCruiserList.Add(addedCruiser);
            ActionResult<IEnumerable<Cruiser>> cruisers = _cruiserController.GetCruisers();
            Assert.IsInstanceOf<OkObjectResult>(cruisers.Result);
        }
        [Test]
        public void GetCruiserTest_ReturnOkresult()
        {
            var addedCruiser = CruiserRepository.AddCruiser(_cruiser);
            _addedCruiserList.Add(addedCruiser);
            var getCruiser = _cruiserController.GetCruiser(addedCruiser);
            Assert.IsInstanceOf<OkObjectResult>(getCruiser.Result);
        }
        [Test]
        public void GetCruiser_CruiserNotFound_ReturnBadRequest()
        {
            var getCruiser = _cruiserController.GetCruiser(-1);
            Assert.IsInstanceOf<BadRequestObjectResult>(getCruiser.Result);
        }
        [Test]
        public void PutCruiser_CruiserWithInvalidAttributes_BadRequestReturned_Test()
        {
            var cruiser = new Cruiser("concordia", true,0, 1000, "Model1", "Line1", "Picture.jpg");
            var updatedcruiser = _cruiserController.PutCruiser(cruiser);
            Assert.IsInstanceOf<BadRequestObjectResult>(updatedcruiser.Result);
        }
        [Test]
        public void PutCruiser_ReturnOkResult()
        {
            var addedCruiserId = CruiserRepository.AddCruiser(_cruiser);
            _addedCruiserList.Add(addedCruiserId);
            _cruiser.Id = addedCruiserId;
            var updatedCruiser = _cruiserController.PutCruiser(_cruiser);
            Assert.IsInstanceOf<OkObjectResult>(updatedCruiser.Result);
        }
        [Test]
        public void PutCruiser_CruiserNotFound_BadRequestReturned_Test()
        {
            _cruiser.Id = -1;
            var updatedcruiser = _cruiserController.PutCruiser(_cruiser);
            Assert.IsInstanceOf<BadRequestObjectResult>(updatedcruiser.Result);
        }
        [Test]
        public void DeleteCruiser_CruiserNotFound_BadRequestReturned_Test()
        {
            var deletedCruiser = _cruiserController.DeleteCruiser(-1);
            Assert.IsInstanceOf<BadRequestObjectResult>(deletedCruiser.Result);
        }
        [Test]
        public void DeleteCruiser_returnOkResult()
        {
            var addedCruiserId = CruiserRepository.AddCruiser(_cruiser);
            var deletedCruiser = _cruiserController.DeleteCruiser(addedCruiserId);
            Assert.IsInstanceOf<OkObjectResult>(deletedCruiser.Result);
        }
        [Test]
        public void GetLayovers_ReturnsOkResult()
        {
            var addedCruiser = CruiserRepository.AddCruiser(_cruiser);
            var layover = new Layover(addedCruiser,"2019-01-01", "2019-01-02",2000,1,2);
            var addedlayover = CruiserRepository.AddLayover(layover);
            _addedCruiserList.Add(addedCruiser);
            var result = _cruiserController.GetLayovers(addedCruiser);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }
        [Test]
        public void PostLayover_ReturnsOkResult_Test()
        {
            var addedCruiser = CruiserRepository.AddCruiser(_cruiser);
            var layover = new Layover(addedCruiser,"2019-01-01", "2019-01-02",2000,1,2);
            var addedlayover = _cruiserController.PostLayover(layover);
            _addedCruiserList.Add(addedCruiser);
            Assert.IsInstanceOf<OkObjectResult>(addedlayover.Result);
        }
        [Test]
        public void PostLayover_CruiserNotFound_ReturnsBadrequest_Test()
        {
            var layover = new Layover(-1,"2019-01-01", "2019-01-02",2000,1,2);
            var addedlayover = _cruiserController.PostLayover(layover);
            Assert.IsInstanceOf<BadRequestObjectResult>(addedlayover.Result);
        }
        [Test]
        public void PostLayover_InvalidAttribute_ReturnsBadRequest_Test()
        {
            var layover = new Layover(2,"2019-01-01", "2019-01-02",2000,0,2);
            var addedlayover = _cruiserController.PostLayover(layover);
            Assert.IsInstanceOf<BadRequestObjectResult>(addedlayover.Result);
        }
        [Test]
        public void DeleteLayover_ReturnsOkResult_Test()
        {
            var addedCruiser = CruiserRepository.AddCruiser(_cruiser);
            _addedCruiserList.Add(addedCruiser);
            var layover = new Layover(addedCruiser,"2019-01-01", "2019-01-02",2000,1,2);
            var addedlayover = CruiserRepository.AddLayover(layover);
            var deletedlayover = _cruiserController.DeleteLayover(addedlayover.Id);
            Assert.IsInstanceOf<OkObjectResult>(deletedlayover.Result);
        }
        [Test]
        public void PostLayover_LayoverNotFound_ReturnsBadRequest_Test()
        {
            var deletedlayover = _cruiserController.DeleteLayover(-1);
            Assert.IsInstanceOf<BadRequestObjectResult>(deletedlayover.Result);
        }
        [Test]
        public void GetLayoversByLoc_ReturnsOkResult_Test()
        {
            var addedCruiser = CruiserRepository.AddCruiser(_cruiser);
            var layover = new Layover(addedCruiser,"2019-01-01", "2019-01-02",2000,1,2);
            var addedlayover = CruiserRepository.AddLayover(layover);
            _addedCruiserList.Add(addedCruiser);
            var result = _cruiserController.GetLayoverByLoc(layover.LocDeparture,layover.LocArrival);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }
        [Test]
        public void GetLayoversByLoc_LayoverNotFound_ReturnsBadRequest_Test()
        {
            var result = _cruiserController.GetLayoverByLoc(-1,-1);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
    }
}