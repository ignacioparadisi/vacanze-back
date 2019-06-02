using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo8;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo8;

namespace vacanze_back.VacanzeApiTest.Grupo8
{
    [TestFixture]
    public class CruiserControllerTests
    {
        private CruiserController CruiserController;
        private List<int> addedCruiserList;
        [SetUp]
        public void Setup()
        {
            CruiserController = new CruiserController();
        }
        [Test]
        public void PostCruisersTest_ReturnOkResult()
        {
            var cruiser = new Cruiser("concordia", true, 100, 1000, "Model1", "Line1", "Picture.jpg");
            var addedcruiser = CruiserController.PostCruiser(cruiser);
            Assert.IsInstanceOf<OkObjectResult>(addedcruiser.Result);
        }
        [Test]
        public void PostCruiser_CruiserWithInvalidAttributes_BadRequestReturned_Test()
        {
            var cruiser = new Cruiser("concordia", true,0, 1000, "Model1", "Line1", "Picture.jpg");
            var addedcruiser = CruiserController.PostCruiser(cruiser);
            Assert.IsInstanceOf<BadRequestObjectResult>(addedcruiser.Result);
        }
        [Test]
        public void GetCruisersTest_ReturnsOkResult()
        {    
            var cruiser = new Cruiser("concordia", true, 100, 1000, "Model1", "Line1", "Picture.jpg");
            var addedCruiser = CruiserRepository.AddCruiser(cruiser);
            ActionResult<IEnumerable<Cruiser>> cruisers = CruiserController.GetCruisers();
            Assert.IsInstanceOf<OkObjectResult>(cruisers.Result);
        }
        [Test]
        public void GetCruiserTest_ReturnOkresult()
        {
            var cruiser = new Cruiser("concordia", true, 100, 1000, "Model1", "Line1", "Picture.jpg");
            var addedCruiser = CruiserRepository.AddCruiser(cruiser);
            var getCruiser = CruiserController.GetCruiser(addedCruiser);
            Assert.IsInstanceOf<OkObjectResult>(getCruiser.Result);
        }
        [Test]
        public void GetCruiser_CruiserNotFound_ReturnBadRequest()
        {
            var getCruiser = CruiserController.GetCruiser(-1);
            Assert.IsInstanceOf<BadRequestObjectResult>(getCruiser.Result);
        }
        [Test]
        public void PutCruiser_CruiserWithInvalidAttributes_BadRequestReturned_Test()
        {
            var cruiser = new Cruiser("concordia", true,0, 1000, "Model1", "Line1", "Picture.jpg");
            var updatedcruiser = CruiserController.PutCruiser(cruiser);
            Assert.IsInstanceOf<BadRequestObjectResult>(updatedcruiser.Result);
        }
        [Test]
        public void PutCruiser_ReturnOkResult()
        {
            var cruiser = new Cruiser("concordia", true,100, 1000, "Model1", "Line1", "Picture.jpg");
            var addedCruiserId = CruiserRepository.AddCruiser(cruiser);
            cruiser.Id = addedCruiserId;
            var updatedCruiser = CruiserController.PutCruiser(cruiser);
            Console.WriteLine(1);
            Assert.IsInstanceOf<OkObjectResult>(updatedCruiser.Result);
        }
        [Test]
        public void PutCruiser_CruiserNotFound_BadRequestReturned_Test()
        {
            var cruiser = new Cruiser("concordia", true,100, 1000, "Model1", "Line1", "Picture.jpg");
            cruiser.Id = -1;
            var updatedcruiser = CruiserController.PutCruiser(cruiser);
            Assert.IsInstanceOf<BadRequestObjectResult>(updatedcruiser.Result);
        }
        [Test]
        public void PutCruiser_NullCruiser_BadRequestReturned_Test()
        {
            var updatedcruiser = CruiserController.PutCruiser(null);
            Assert.IsInstanceOf<BadRequestObjectResult>(updatedcruiser.Result);
        }
        [Test]
        public void DeleteCruiser_CruiserNotFound_BadRequestReturned_Test()
        {
            var deletedCruiser = CruiserController.DeleteCruiser(-1);
            Assert.IsInstanceOf<BadRequestObjectResult>(deletedCruiser.Result);
        }
        [Test]
        public void DeleteCruiser_returnOkResult()
        {
            var cruiser = new Cruiser("concordia", true,100, 1000, "Model1", "Line1", "Picture.jpg");
            var addedCruiserId = CruiserRepository.AddCruiser(cruiser);
            var deletedCruiser = CruiserController.DeleteCruiser(addedCruiserId);
            Assert.IsInstanceOf<OkObjectResult>(deletedCruiser.Result);
        }
        [Test]
        public void GetLayovers_ReturnsOkResult()
        {    
            var cruiser = new Cruiser("concordia", true, 100, 1000, "Model1", "Line1", "Picture.jpg");
            var addedCruiser = CruiserRepository.AddCruiser(cruiser);
            var layover = new Layover(addedCruiser,"2019-01-01", "2019-01-02",2000,1,2);
            var addedlayover = CruiserRepository.AddLayover(layover);
            var result = CruiserController.GetLayovers(addedCruiser);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }
        [Test]
        public void PostLayover_ReturnsOkResult_Test()
        {
            var cruiser = new Cruiser("concordia", true,100, 1000, "Model1", "Line1", "Picture.jpg");
            var addedCruiser = CruiserRepository.AddCruiser(cruiser);
            var layover = new Layover(addedCruiser,"2019-01-01", "2019-01-02",2000,1,2);
            var addedlayover = CruiserController.PostLayover(layover);
            Assert.IsInstanceOf<OkObjectResult>(addedlayover.Result);
        }
        [Test]
        public void PostLayover_CruiserNotFound_ReturnsBadrequest_Test()
        {
            var layover = new Layover(-1,"2019-01-01", "2019-01-02",2000,1,2);
            var addedlayover = CruiserController.PostLayover(layover);
            Assert.IsInstanceOf<BadRequestObjectResult>(addedlayover.Result);
        }
        [Test]
        public void PostLayover_InvalidAttribute_ReturnsBadRequest_Test()
        {
            var layover = new Layover(2,"2019-01-01", "2019-01-02",2000,0,2);
            var addedlayover = CruiserController.PostLayover(layover);
            Assert.IsInstanceOf<BadRequestObjectResult>(addedlayover.Result);
        }
        [Test]
        public void DeleteLayover_ReturnsOkResult_Test()
        {
            var cruiser = new Cruiser("concordia", true,100, 1000, "Model1", "Line1", "Picture.jpg");
            var addedCruiser = CruiserRepository.AddCruiser(cruiser);
            var layover = new Layover(addedCruiser,"2019-01-01", "2019-01-02",2000,1,2);
            var addedlayover = CruiserRepository.AddLayover(layover);
            var deletedlayover = CruiserController.DeleteLayover(addedlayover.Id);
            Assert.IsInstanceOf<OkObjectResult>(deletedlayover.Result);
        }
        [Test]
        public void PostLayover_LayoverNotFound_ReturnsBadRequest_Test()
        {
            var deletedlayover = CruiserController.DeleteLayover(-1);
            Assert.IsInstanceOf<BadRequestObjectResult>(deletedlayover.Result);
        }
    }
}